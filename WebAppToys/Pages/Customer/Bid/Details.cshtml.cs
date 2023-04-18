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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace WebAppToys.Pages.Customer
{
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public DetailsModel(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _emailSender = emailSender;
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

            TempData["UserId"] = BidsObj.User_Id;

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


            if (status == "BidAccepted")
            {
                BidsObj.Status = Status.BidAccepted;
                await SendNotificationEmails(ListingObj.User_Id, BidsObj.User_Id);

                TempData["ListingName"] = ListingObj.Name;
                TempData["TradeName"] = TradeObj.Name;
                TempData["BidPrice"] = BidsObj.Price.ToString("0.00");


            }
            else if(status == "BidDeclined")
            {
                BidsObj.Status = Status.BidDeclined;
                

                _unitOfWork.Bids.Update(BidsObj);
                _unitOfWork.Commit();
                return RedirectToPage("./Index");

            }
            _unitOfWork.Bids.Update(BidsObj);
            _unitOfWork.Commit();

            return RedirectToPage("./accepted");
        }

        private async Task SendNotificationEmails(string listingUserId, string bidderUserId)
        {
            var listingUser = await _userManager.FindByIdAsync(listingUserId);
                var bidderUser = await _userManager.FindByIdAsync(bidderUserId);

                
                if (listingUser != null && listingUser.Email != null)
                {
                    await _emailSender.SendEmailAsync(
                        listingUser.Email,
                        "Swap Accepted!",
                        $"<h2>Please contact {bidderUser.Email} to coordinate your swap in person! Thank you for using Toy Swap Hub!</h2>"
                    );
                }

                if (bidderUser != null && bidderUser.Email != null)
                {
                    await _emailSender.SendEmailAsync(
                        bidderUser.Email,
                        "Swap Accepted!",
                        $"<h2>Please contact {listingUser.Email} to coordinate your swap in person! Thank you for using Toy Swap Hub!</h2>"
                    );
                }

        }

    }
}
