using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FurnitureShop.Models
{
    public class Order
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int OrderId { get; set; }

        public int UserId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ShippingDate { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DeliveryDate { get; set; }

        [Required]
        public int OrderDeliveryId { get; set; }
        public virtual OrderDelivery Orderdelivery { get; set; }

        public string TrackingNumber { get; set; }

        [DataType(DataType.MultilineText)]
        public string Note { get; set; }

    }
}