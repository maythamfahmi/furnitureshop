using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FurnitureShop.Models
{
    public class OrderProduct
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int OrderProdcutId { get; set; }

        public int OrderId { get; set; }

        public int OProdcutId { get; set; }

        public int OProdcutName { get; set; }

        public int OProdcutQty { get; set; }

        public int OProdcutPrice { get; set; }

    }
}