using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ApplicationCore.Models;

namespace _WebAppToys.Pages.Customer
{
    public class CreateModel : PageModel
    {
        private readonly IdentityDbContext _context;

        public CreateModel(IdentityDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Bids Bids { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Bids == null || Bids == null)
            {
                return Page();
            }

            _context.Bids.Add(Bids);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
