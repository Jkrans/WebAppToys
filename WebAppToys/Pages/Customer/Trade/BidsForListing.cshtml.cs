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
        public Listing listingObj { get; set; }
        public Dictionary<int, Listing> TradeListings { get; set; } = new Dictionary<int, Listing>();

        public IActionResult OnGet(int listingId)
        {
            ListingId = listingId;
            var bids = _unitOfWork.Bids.List(b => b.Listing_Id == listingId).ToList();

            if (bids == null)
            {
                return NotFound();
            }
            else
            {
                Bids = bids;
            }

            foreach (var bid in Bids)
            {
                var tradeListing = _unitOfWork.Listing.Get(m => m.Id == bid.Trade_Id);
                if (tradeListing != null)
                {
                    TradeListings[bid.Trade_Id ?? 0] = tradeListing;
                }
            }

            return Page();
        }
    }
}
