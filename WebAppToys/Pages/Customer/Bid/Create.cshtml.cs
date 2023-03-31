using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ApplicationCore.Models;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace _WebAppToys.Pages.Customer
{
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        [BindProperty]
        public Bids BidsObj { get; set; } = new Bids();
        public Listing ListingObj { get; set; }

        public SelectList UserListings { get; set; }


        public async Task<IActionResult> OnGetAsync(int? listingId)
        {
            if (listingId == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return NotFound();
            }

            BidsObj.User_Id = user.Id.ToString();
            BidsObj.Listing_Id = listingId.Value;
            BidsObj.Status = "Submitted";

            // Get the Listing object using the listingId
            ListingObj = _unitOfWork.Listing.Get(m => m.Id == listingId.Value);

            if (ListingObj == null)
            {
                return NotFound();
            }


            if (_unitOfWork.Listing == null)
            {
                throw new InvalidOperationException("UnitOfWork.Listing is null.");
            }

            var listings = _unitOfWork.Listing.GetAll(m => m.User_Id == user.Id);
            if (listings == null)
            {
                Console.WriteLine($"No listings found for user {user.Id}.");
                return NotFound();
            }

            Console.WriteLine($"Listings found for user {user.Id}: {listings.Count()}");

            UserListings = new SelectList(listings, "Id", "Name");

            return Page();
        }





        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _unitOfWork.Bids == null || BidsObj == null)
            {
                return Page();
            }


            


            _unitOfWork.Bids.Add(BidsObj);
            _unitOfWork.Commit();

            return RedirectToPage("/Customer/Bid/Details", new {Id = BidsObj.Id});
        }
    }
}
