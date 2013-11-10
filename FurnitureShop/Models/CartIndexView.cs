using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnitureShop.Models
{
    public class CartIndexView
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}