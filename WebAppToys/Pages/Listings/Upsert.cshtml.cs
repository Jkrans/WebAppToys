using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppToys.Pages.Listings
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public Listing ListingObj { get; set; }

        public UpsertModel(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

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

            return Page(); //assume in insert mode
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
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
