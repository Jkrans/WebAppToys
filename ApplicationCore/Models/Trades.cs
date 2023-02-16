using System;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models
{
    public class Trades
    {
        [Key]
        public int Id { get; set; } // primary key
        [Required]
        public string Name { get; set; }
        public string Value { get; set; }
        [Required]
        public string Condition { get; set; }

    }
}

