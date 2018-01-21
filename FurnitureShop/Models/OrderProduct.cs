using System.ComponentModel.DataAnnotations;
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

        public string OProdcutName { get; set; }

        public int OProdcutQty { get; set; }

        public int OProdcutPrice { get; set; }

    }
}