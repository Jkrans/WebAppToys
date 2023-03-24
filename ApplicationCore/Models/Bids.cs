using System;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models
{
    public class Bids
    {
        [Key]
        public int Id { get; set; }
   
        [Required]
        public string User_Id { get; set; } // foreign key to user

        [Required]
        public int Listing_Id { get; set; } // foreign key to listing

        public float Price { get; set; } // bid price

        [Required]
        public int Trade_Id { get; set; } // foreign key to trade

        [Required]
        public string Status { get; set; } // status of the bid (e.g. accepted, rejected, pending)
    }

}



