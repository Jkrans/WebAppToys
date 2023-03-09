using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Models;

namespace _WebAppToys.Pages.Customer
{
    public class DeleteModel : PageModel
    {
        private readonly IdentityDbContext _context;

        public DeleteModel(IdentityDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Bids == null)
            {
                return NotFound();
            }
            var bids = await _context.Bids.FindAsync(id);

            if (bids != null)
            {
                Bids = bids;
                _context.Bids.Remove(Bids);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
