using DomenicoPieShop_Empty.Models;
using Microsoft.AspNetCore.Mvc;

namespace DomenicoPieShop_Empty.ViewModels
{
    public class ShoppingCartViewModdel
    {
        public ShoppingCartViewModdel(IShoppingCart shoppingCart, decimal shoppingCartTotal)
        {
            ShoppingCart = shoppingCart;
            ShoppingCartTotal = shoppingCartTotal;
        }

        public IShoppingCart ShoppingCart { get; }
        public decimal ShoppingCartTotal { get;  }
       
    }
}
