using DomenicoPieShop_Empty.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DomenicoPieShop_Empty.Pages
{
    public class CheckoutPageModel : PageModel
    {
        public Order Order { get; set; }

        private readonly IOrderRepository orderRepository;
        private readonly IShoppingCart shoppingCart;

        public CheckoutPageModel(IOrderRepository orderRepository, IShoppingCart shoppingCart)
        {
            this.orderRepository = orderRepository;
            this.shoppingCart = shoppingCart;
        }

        public void OnGet()
        {
        }

        public ActionResult OnPost()
        {
            if(!ModelState.IsValid) { return Page(); }

            var items = shoppingCart.GetShoppingCartItems();
            shoppingCart.ShoppingCartItems = items;
            if (shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "empty");

            }
            if (ModelState.IsValid)
            {

                orderRepository.CreateOrder(Order);
                shoppingCart.ClearCart();
                return RedirectToPage("CheckoutCompletePage");
            }

            return Page();


        }
    }
}
