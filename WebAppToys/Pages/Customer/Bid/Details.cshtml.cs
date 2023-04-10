using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Models;
using ApplicationCore.Interfaces;
using Infrastructure.Utility;

namespace WebAppToys.Pages.Customer
{
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Bids BidsObj { get; set; } = default!;
        public Listing ListingObj { get; set; } = default!;
        public Listing TradeObj { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _unitOfWork.Bids == null)
            {
                return NotFound();
            }

            var bids = _unitOfWork.Bids.Get(m => m.Id == id);
            if (bids == null)
            {
                return NotFound();
            }
            else
            {
                BidsObj = bids;
            }

            var listing = _unitOfWork.Listing.Get(l => l.Id == BidsObj.Listing_Id);
            if (listing == null)
            {
                return NotFound();
            }
            else
            {
                ListingObj = listing;
            }
            

            var trade = _unitOfWork.Listing.Get(l => l.Id == BidsObj.Trade_Id);
            if (trade == null)
            {
                return NotFound();
            }
            else
            {
                TradeObj = trade;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id,string status = "")
        {
            if (id == null || _unitOfWork.Bids == null)
            {
                return NotFound();
            }

            var bids = _unitOfWork.Bids.Get(m => m.Id == id);
            if (bids == null)
            {
                return NotFound();
            }
            else
            {
                BidsObj = bids;
            }

            if (status == "BidAccepted")
            {
                BidsObj.Status = Status.BidAccepted;
            }
            _unitOfWork.Bids.Update(BidsObj);
            _unitOfWork.Commit();

            return RedirectToPage("./accepted");
        }
    }
}
