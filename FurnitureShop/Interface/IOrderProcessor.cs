using FurnitureShop.Models;

namespace FurnitureShop.Interface
{
    public interface IOrderProcessor
    {
        // process order to email or what ever
        void ProcessOrder(Cart cart, ShippingDetails shippingDetails);
    }
}