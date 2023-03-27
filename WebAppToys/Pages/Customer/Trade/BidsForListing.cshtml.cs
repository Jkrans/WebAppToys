using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _WebAppToys.Pages.Customer.Trade
{
	public class BidsForListingModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public BidsForListingModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int ListingId { get; set; }
        public List<Bids> Bids { get; set; }

        public IActionResult OnGet(int listingId)
        {
            ListingId = listingId;
            Bids = _unitOfWork.Bids.List(b => b.Listing_Id == listingId).ToList();

            if (Bids == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
