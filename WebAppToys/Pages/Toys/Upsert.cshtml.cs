using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppToys.Pages.Toys
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public Toy ToyObj { get; set; }

        public UpsertModel(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public IActionResult OnGet(int? id)
        {
            ToyObj = new Toy();
            if (id != 0) // edit
            {
                ToyObj = _unitOfWork.Toy.Get(u => u.Id == id);
                if (ToyObj == null)
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
            if (ToyObj.Id == 0)
            {
                _unitOfWork.Toy.Add(ToyObj);
            }

            else
            {
                _unitOfWork.Toy.Update(ToyObj);
            }

            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }

    }

}
