using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FurnitureShop.Models
{
	public class ProductSubCategory
	{
		[HiddenInput(DisplayValue = false)]
		[Key]
		public int ProductSubCategoryId { get; set; }

		[Required]
		public int ProductId { get; set; }

		public virtual Product Product { get; set; }
		
		[Required]
		public int SubCategoryId { get; set; }

		public virtual SubCategory SubCategory { get; set; }

	}
}