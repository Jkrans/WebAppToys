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
    public class IndexModel : PageModel
    {
        private readonly IdentityDbContext _context;

        public IndexModel(IdentityDbContext context)
        {
            _context = context;
        }

        public IList<Bids> Bids { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Bids != null)
            {
                Bids = await _context.Bids.ToListAsync();
            }
        }
    }
}
