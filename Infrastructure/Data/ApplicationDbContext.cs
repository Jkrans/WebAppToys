using ApplicationCore.Models;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        // Create a database table based on the model from ApplicationCore.Models
        public DbSet<Category> Category { get; set; }
        public DbSet<Listing> Listing { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Bids> Bids { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed the rolesin the database
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "4521cd22-d23f-40b8-b781-2902e32d6344", Name = SD.AdminRole, NormalizedName = SD.AdminRole.ToUpper() });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "4521cd22-d23f-40b8-b781-2902e32d6345", Name = SD.UserRole, NormalizedName = SD.UserRole.ToUpper() });
            

            // hasher to hash the password before seeding the user to the db
            var hasher = new PasswordHasher<ApplicationUser>();

            // Seeding the User to AspNetUsers table
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationCore.Models.ApplicationUser
                {
                    Id = "b9c8bcd4-2819-4584-bdfb-9d1b03056026", // guid Primary Key
                    UserName = "Admin@Admin.com",
                    Email = "Admin@Admin.com",
                    NormalizedEmail = "ADMIN@ADMIN.COM",
                    FirstName = "Admin",
                    LastName = "Admin",
                    NormalizedUserName = "ADMIN@ADMIN.COM",
                    PasswordHash = hasher.HashPassword(null, "Pa$$w0rd")

                }
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            // Seed the Default Admin User as a Manager in AspNetUserRoles
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "4521cd22-d23f-40b8-b781-2902e32d6344",
                    UserId = "b9c8bcd4-2819-4584-bdfb-9d1b03056026"
                }
            );
        }

    }
}

