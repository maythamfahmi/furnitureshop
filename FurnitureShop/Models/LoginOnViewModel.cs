using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Models
{
	public class LoginOnViewModel
	{
		[Required]
		public string UserName { get; set; }

		[Required]
		public string Password { get; set; }
	}
}