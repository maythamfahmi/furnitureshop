using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FurnitureShop.Models
{
    public class OrderDelivery
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int OrderDeliveryId { get; set; }

	[Required]
        public string Methode { get; set; }

    }
}