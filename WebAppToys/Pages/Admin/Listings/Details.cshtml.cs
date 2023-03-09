using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppToys.Pages.Admin.Listings
{
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Listing ListingObj { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _unitOfWork.Listing == null)
            {
                return NotFound();
            }

            var listingObj = _unitOfWork.Listing.Get(m => m.Id == id);
            if (listingObj == null)
            {
                return NotFound();
            }
            else
            {
                ListingObj = listingObj;
            }

            return Page();
        }
    }
}
