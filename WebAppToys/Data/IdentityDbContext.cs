using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Models;

    public class IdentityDbContext : DbContext
    {
        public IdentityDbContext (DbContextOptions<IdentityDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationCore.Models.Bids> Bids { get; set; } = default!;
    }
