using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FurnitureShop.Models
{
	public class Address
	{
		[HiddenInput(DisplayValue = false)]
		[Key]
		public int AddressId { get; set; }

		[HiddenInput(DisplayValue=false)]
		public int UserId { get; set; }

		[Required(ErrorMessage = "Please enter at least one address line")]
		public string AddressLine1 { get; set; }
		public string AddressLine2 { get; set; }
		public string AddressLine3 { get; set; }

		[Required(ErrorMessage="Please enter your postal code")]
		public string Postal { get; set; }

		[Required(ErrorMessage="Please enter the city")]
		public string City { get; set; }

		[Required(ErrorMessage="Please enter the country")]
		public string Country { get; set; }
	}
}