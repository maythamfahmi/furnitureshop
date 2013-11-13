using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FurnitureShop.Models
{
	public class Category
	{
		[HiddenInput(DisplayValue = false)]
		[Key]
		public int CategoryId { get; set; }

		[Required(ErrorMessage="Please enter a category name")]
		public string Name { get; set; }

		public virtual List<SubCategory> SubCategories { get; set; }
	}
}