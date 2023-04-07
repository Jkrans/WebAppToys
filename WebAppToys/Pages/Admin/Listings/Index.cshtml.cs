using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ApplicationCore.Models;
using ApplicationCore.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Claims;

namespace WebAppToys.Pages.Admin.Listings
{
    public class IndexModel : PageModel
    {

        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Listing> ListingObj { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool ShowMyListings { get; set; }

        public void OnGet()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (ShowMyListings) // if true
            {
                ListingObj = _unitOfWork.Listing.GetAll(listing => listing.ExpirationDate > DateTime.Now && listing.User_Id == userId);
            }
            else
            {
                ListingObj = _unitOfWork.Listing.GetAll(listing => listing.ExpirationDate > DateTime.Now);
            }
        }

    }
}
