using System;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models
{
	public class Toy
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public string Description { get; set; }
	}
}

