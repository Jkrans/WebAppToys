using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Models;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Utility;

namespace _WebAppToys.Pages.Customer
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditModel(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }


        [BindProperty]
        public Bids BidsObj { get; set; } = default!;

        public Listing ListingObj { get; set; }

        public SelectList UserListings { get; set; }
        private string userId;

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            if (TempData["UserId"] != null)
            {
                userId = TempData["UserId"].ToString();                
            }
            
            if (id == null || _unitOfWork.Bids == null)
            {
                return NotFound();
            }

            var bids = _unitOfWork.Bids.GetFirstOrDefault(m => m.Id == id);
            if (bids == null)
            {
                return NotFound();
            }
            BidsObj = bids;

            

            // Get the user
            //var user = await _userManager.GetUserAsync(HttpContext.User);
            //if (user == null)
            //{
            //    return NotFound();
            //}

            // Get user listings
            var listings = _unitOfWork.Listing.GetAll(m => m.User_Id == userId);
            if (listings == null)
            {
                Console.WriteLine($"No listings found for user {userId}.");
                return NotFound();
            }

            // Create SelectList for UserListings
            UserListings = new SelectList(listings, "Id", "Name");

            return Page();
        }

        
        public async Task<IActionResult> OnPostAsync(string status = "")
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Checks what button was used to select edit and updates status
            if (status == "BidCountered")
            {
                BidsObj.Status = Status.BidCountered;
            }
            else if (status == "BidSubmitted")
            {
                BidsObj.Status = Status.BidSubmitted;
            }

            _unitOfWork.Bids.Update(BidsObj);


            try
            {
                _unitOfWork.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BidsExists(BidsObj.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            

            return RedirectToPage("./Details", new { Id = BidsObj.Id });

        }

        private bool BidsExists(int id)
        {
            return _unitOfWork.Bids.GetFirstOrDefault(e => e.Id == id) != null;
        }

    }
}
