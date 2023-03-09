using System;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models
{
    public class Listing
    {
        [Key]
        public int Id { get; set; } // primary key
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public string? Status { get; set; }
        public int Category_Id { get; set; } // foreign key to category
        public string User_Id { get; set; } // foreign key to user
    }
}

