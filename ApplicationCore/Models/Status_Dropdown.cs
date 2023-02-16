using System;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models
{
    public class Status_Dropdown
    {
        [Key]
        public int Id { get; set; } // primary key
        [Required]
        public string Value { get; set; }
    }
}

