using DomenicoPieShop_Empty.Models;
using DomenicoPieShop_Empty.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DomenicoPieShop_Empty.Components
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly IShoppingCart shoppingCart;

        public ShoppingCartSummary(IShoppingCart shoppingCart)
        {
            this.shoppingCart = shoppingCart;
        }
        public IViewComponentResult Invoke()//sempre invoke deve essere
        {
            var items = shoppingCart.GetShoppingCartItems();
            shoppingCart.ShoppingCartItems = items;

            var model = new ShoppingCartViewModdel(shoppingCart, shoppingCart.GetShoppingCartTotal());

            return View(model);

        }
    }
}
