using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FurnitureShop.Models
{
	public class User
	{
		[HiddenInput(DisplayValue = false)]
		[Key]
		public int UserId { get; set; }

		[Required(ErrorMessage="Please create a user name")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Please enter an e-mail address")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$",
            ErrorMessage = "Invalid email address.")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Please create password")]
        //[RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?!.*(.)\1\1)[a-zA-Z0-9@]{6,12}$",
        //    ErrorMessage = "Complex password should have blbl.")]
		[MinLength(2,ErrorMessage="Please specify a longer password")]
        [DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Please create your firstname")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Please create your lastname")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Please create phonenumber")]
        [RegularExpression(@"\d{8}",
            ErrorMessage = "Invalid phone number.")]
		public string Phone { get; set; }

		public string ImageSrc { get; set; }

		public int UserRoleId { get; set; }
		public virtual UserRole UserRole { get; set; }

		public virtual List<Address> Address { get; set; }
	}
}