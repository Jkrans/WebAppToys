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

        public async Task<IActionResult> OnPostAsync(int? id, string status = "")
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
                await SendNotificationEmails(ListingObj.User_Id, BidsObj.User_Id, ListingObj.Id, BidsObj.Id);


                TempData["ListingName"] = ListingObj.Name;
                TempData["TradeName"] = TradeObj.Name;
                TempData["BidPrice"] = BidsObj.Price.ToString("0.00");


            }
            else if (status == "BidDeclined")
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

        /// <summary>
        /// Retrieves the emails of non-winning bidders and updates their bid status to 'Bid Declined'.
        /// </summary>
        /// <param name="listingId">The ID of the listing.</param>
        /// <param name="winningBidId">The ID of the winning bid.</param>
        /// <returns>A Task representing the asynchronous operation, with a Dictionary containing the non-winning bidder IDs and their respective email addresses.</returns>
        private async Task<Dictionary<int, string>> GetNonWinningBiddersEmailsAsync(int listingId, int winningBidId)
        {
            var nonWinningBiddersEmails = new Dictionary<int, string>();
            var bids = _unitOfWork.Bids.List(b => b.Listing_Id == listingId).ToList();

            foreach (var bid in bids)
            {
                if (bid.Id != winningBidId)
                {
                    var user = await _userManager.FindByIdAsync(bid.User_Id);

                    if (user != null)
                    {
                        nonWinningBiddersEmails[bid.Id] = user.Email;
                    }

                    UpdateBidStatus(bid.Id, Status.BidDeclined);
                }
            }

            return nonWinningBiddersEmails;
        }


        /// <summary>
        /// Sends notification emails to the listing user, winning bidder, and non-winning bidders.
        /// </summary>
        /// <param name="listingUserId">The ID of the user who created the listing.</param>
        /// <param name="bidderUserId">The ID of the user who submitted the winning bid.</param>
        /// <param name="listingId">The ID of the listing.</param>
        /// <param name="winningBidId">The ID of the winning bid.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        private async Task SendNotificationEmails(string listingUserId, string bidderUserId, int listingId, int winningBidId)
        {
            var listingUser = await _userManager.FindByIdAsync(listingUserId);
            var bidderUser = await _userManager.FindByIdAsync(bidderUserId);
            var nonWinningBiddersEmails = await GetNonWinningBiddersEmailsAsync(listingId, winningBidId);

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

            foreach (var email in nonWinningBiddersEmails.Values)
            {
                if (email != null)
                {
                    await _emailSender.SendEmailAsync(
                        email,
                        "Swap Update",
                        $"<h2>Unfortunately, your bid on {ListingObj.Name} has not been accepted. Please check the website for more trading opportunities. Thank you for using Toy Swap Hub!</h2>"
                    );
                }
            }

        }

        /// <summary>
        /// Updates the status of a bid with the specified bidId and status.
        /// </summary>
        /// <param name="bidId">The ID of the bid to update.</param>
        /// <param name="status">The new status to assign to the bid.</param>
        private void UpdateBidStatus(int bidId, string status)
        {
            var bid = _unitOfWork.Bids.GetByID(bidId);
            if (bid != null)
            {
                bid.Status = status;
                _unitOfWork.Bids.Update(bid);
                _unitOfWork.Commit();
            }
        }


    }
}
