﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FurnitureShop.Models
{
	public class Product
	{
		[HiddenInput(DisplayValue = false)]
		[Key]
		public int ProductId { get; set; }

		[Required(ErrorMessage = "Please specify a product name")]
		public string Name { get; set; }

		[DataType(DataType.MultilineText)]
		[Required(ErrorMessage = "Please enter a description")]
		public string Description { get; set; }

		[Required]
		[Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
		public decimal Price { get; set; }

		public virtual Category Category { get; set; }

		public int CategoryId { get; set; }

        public virtual List<ProductSubCategory> SubCategories { get; set; }

		[Required(ErrorMessage = "Please select a product image")]
		public string ImageSrc { get; set; }

		public byte[] ImageData { get; set; }

		[HiddenInput(DisplayValue = false)]
		public string ImageMimeType { get; set; }

	}
}