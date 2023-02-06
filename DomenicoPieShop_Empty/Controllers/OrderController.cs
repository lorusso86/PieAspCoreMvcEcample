using DomenicoPieShop_Empty.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace DomenicoPieShop_Empty.Controllers
{
    [Authorize] //FUNZIONA SOLO SE SEI LOGGATO
    public class OrderController : Controller
    {
        private readonly IOrderRepository orderRepository;
        private readonly IShoppingCart shoppingCart;
        
        public OrderController(IOrderRepository orderRepository, IShoppingCart shoppingCart)
        {
            this.orderRepository = orderRepository;
            this.shoppingCart = shoppingCart;
        }
        [HttpGet]//non serve, se nn specificato è messa di default
        public IActionResult Checkout()//in caso di form by GET
        {
            return View();

        }
        [HttpPost]
        public IActionResult Checkout(Order order)//in caso di form by POST
        {
            var items = shoppingCart.GetShoppingCartItems();
            shoppingCart.ShoppingCartItems = items;
            if (shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "empty");

            }
            if (ModelState.IsValid)
            {

                orderRepository.CreateOrder(order);
                shoppingCart.ClearCart();
                return RedirectToAction("CheckoutComplete");
            }

            return View(order);
        }
        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "OK";
            return View();
        }
    }
}
