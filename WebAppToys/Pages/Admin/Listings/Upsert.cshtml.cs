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

namespace WebAppToys.Pages.Admin.Listings
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;


        [BindProperty]
        public Listing ListingObj { get; set; }

        public IEnumerable<SelectListItem> CategoryList { get; set; }

        public UpsertModel(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
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

        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            ListingObj.Status = "Listed";

            // Set user ID to current user ID
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                ListingObj.User_Id = user.Id.ToString();
            }

            // if new
            if (ListingObj.Id == 0)
            {
                _unitOfWork.Listing.Add(ListingObj);
            }

            else
            {
                _unitOfWork.Listing.Update(ListingObj);
            }

            


            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }

    }

}
