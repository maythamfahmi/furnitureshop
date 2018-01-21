using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FurnitureShop.Models
{
	public class SubCategory
	{
		[HiddenInput(DisplayValue = false)]
		[Key]
		public int SubCategoryId { get; set; }

		[Required(ErrorMessage="Please enter a subcategory name")]
		public string Name { get; set; }

		public int? CategoryId { get; set; } //this is nullable
		public virtual Category Category { get; set; }
	}
}