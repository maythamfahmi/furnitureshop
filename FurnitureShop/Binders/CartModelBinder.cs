using FurnitureShop.Models;
using System.Web.Mvc;

namespace FurnitureShop.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string SessionKey = "Cart";
        public object BindModel(ControllerContext controllerContext,
        ModelBindingContext bindingContext)
        {
            // get the Cart from the session
            Cart cart = (Cart)controllerContext.HttpContext.Session[SessionKey];
            // create the Cart if there wasn't one in the session data
            if (cart == null)
            {
                cart = new Cart();
                controllerContext.HttpContext.Session[SessionKey] = cart;
            }
            // return the cart
            return cart;
        }
    }
}
