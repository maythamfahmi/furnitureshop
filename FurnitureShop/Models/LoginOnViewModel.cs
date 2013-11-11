using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Models
{
	public class LoginOnViewModel
	{
		[Required]
		public string userName { get; set; }

		[Required]
		public string Password { get; set; }
	}
}