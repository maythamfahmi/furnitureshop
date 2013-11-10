using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
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