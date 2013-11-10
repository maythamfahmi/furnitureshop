using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FurnitureShop.Models
{
	public class UserRole
	{
		[HiddenInput(DisplayValue = false)]
		[Key]
		public int UserRoleId { get; set; }

		[Required(ErrorMessage="Please enter a role name")]
		public string Name { get; set; }
	}
}