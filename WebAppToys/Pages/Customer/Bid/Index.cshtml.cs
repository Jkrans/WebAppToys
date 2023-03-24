using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Models;
using ApplicationCore.Interfaces;

namespace _WebAppToys.Pages.Customer
{
    public class IndexModel : PageModel
    {
        //private readonly IUnitOfWork _unitOfWork;

        //public IndexModel(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}

        //public Bids BidsObj { get;set; } = default!;

        //public async Task<IActionResult> OnGetAsync(int? id)
        //{
        //    if (id == null || _unitOfWork.Bids == null)
        //    {
        //        return NotFound();
        //    }

        //    var bidsObj = _unitOfWork.Bids.Get(m => m.Id == id);
        //    if (bidsObj == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        BidsObj = bidsObj;
        //    }

        //    return Page();
        //}
    }
}
