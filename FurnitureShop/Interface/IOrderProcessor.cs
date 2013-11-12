using FurnitureShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FurnitureShop.Interface
{
    public interface IOrderProcessor
    {
        void ProcessOrder(Cart cart, ShippingDetails shippingDetails);

    }
}