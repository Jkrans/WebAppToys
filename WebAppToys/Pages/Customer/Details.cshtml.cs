using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Models;

namespace WebAppToys.Pages.Customer
{
    public class DetailsModel : PageModel
    {
        private readonly IdentityDbContext _context;

        public DetailsModel(IdentityDbContext context)
        {
            _context = context;
        }

      public Bids Bids { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Bids == null)
            {
                return NotFound();
            }

            var bids = await _context.Bids.FirstOrDefaultAsync(m => m.Id == id);
            if (bids == null)
            {
                return NotFound();
            }
            else 
            {
                Bids = bids;
            }
            return Page();
        }
    }
}
