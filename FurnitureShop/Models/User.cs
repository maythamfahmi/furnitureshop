using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

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
		public string Email { get; set; }

		[Required(ErrorMessage = "Please create password")]
		[MinLength(2,ErrorMessage="Please specify a longer password")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Please create your firstname")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Please create your lastname")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Please create phonenumber")]
		public string Phone { get; set; }

		public string ImageSrc { get; set; }

		public int UserRoleId { get; set; }
		public virtual UserRole UserRole { get; set; }

		public virtual List<Address> Address { get; set; }
	}
}