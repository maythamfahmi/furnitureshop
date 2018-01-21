using System.ComponentModel.DataAnnotations;
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