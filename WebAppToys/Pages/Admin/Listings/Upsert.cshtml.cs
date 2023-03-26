using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;


namespace WebAppToys.Pages.Admin.Listings
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;


        [BindProperty]
        public Listing ListingObj { get; set; }

        public IEnumerable<SelectListItem> CategoryList { get; set; }

        private readonly IWebHostEnvironment _webHostEnvironment;

        public UpsertModel(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult OnGet(int? id)
        {
            ListingObj = new Listing();
            if (id != 0) // edit
            {
                ListingObj = _unitOfWork.Listing.Get(u => u.Id == id);
                if (ListingObj == null)
                {
                    return NotFound();
                }
            }
            
            if (id != null) //edit mode and will track changes (true)
            {
                ListingObj = _unitOfWork.Listing.Get(u => u.Id == id, true); //keeps track of changes

                var categories = _unitOfWork.Category.List();
                

                CategoryList = categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
                
            }

            if (ListingObj == null)
            {
                ListingObj = new();
            }

            return Page(); //assume in insert mode
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            // get the path to for wwwroot
            string webRootPath = _webHostEnvironment.WebRootPath;

            // pulls files from form request
            var files = HttpContext.Request.Form.Files;

            ListingObj.Status = "Listed";
            ListingObj.ExpirationDate = DateTime.Now.AddDays(7);

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                ListingObj.User_Id = user.Id.ToString();
            }

            // Create new ListingObj
            if (ListingObj.Id == 0)
            {
                // was there an image submitted with the form
                if (files.Count > 0)
                {
                    // create a unique identifier for the image name
                    string fileName = Guid.NewGuid().ToString();

                    // create variable to hold path to images/ListingObjs subfolder
                    var uploads = Path.Combine(webRootPath, @"images\");

                    // get and preserve file extension type
                    var extension = Path.GetExtension(files[0].FileName);

                    // create the complete full path string
                    var fullPath = uploads + fileName + extension;

                    // upload file binary
                    using var fileStream = System.IO.File.Create(fullPath);
                    files[0].CopyTo(fileStream);

                    // associate image to the ListingObj object
                    ListingObj.Image = @"\images\" + fileName + extension;
                }

                // add the new menu item to the database
                _unitOfWork.Listing.Add(ListingObj);
            }

            // Update to existing ListingObj
            else
            {
                // Get the original ListingObj form the database (since tracking and binding is on)
                var objFromDb = _unitOfWork.Listing.Get(m => m.Id == id, true);

                // was there an image submitted with the form
                if (files.Count > 0)
                {
                    // create a unique identifier for the image name
                    string fileName = Guid.NewGuid().ToString();

                    // create variable to hold path to images/ListingObjs subfolder
                    var uploads = Path.Combine(webRootPath, @"images\");

                    // get and preserve file extension type
                    var extension = Path.GetExtension(files[0].FileName);

                    // if item in db already has an image
                    var imagePath = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\'));

                    //if the image exists then physically delete the image
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }

                    // create the complete full path string
                    var fullPath = uploads + fileName + extension;

                    // upload file binary
                    using var fileStream = System.IO.File.Create(fullPath);
                    files[0].CopyTo(fileStream);

                    // associate image to the ListingObj object
                    ListingObj.Image = @"\images\" + fileName + extension;
                }

                // no image uploded with form
                else
                {
                    // add image from the existing item to the item we're updating
                    ListingObj.Image = objFromDb.Image;

                }

                // update the existing item
                _unitOfWork.Listing.Update(ListingObj);

                // save changes to database
                _unitOfWork.Commit();
            }

            // redirect to ListingObjs page
            return RedirectToPage("./Index");

        }

    }

}
