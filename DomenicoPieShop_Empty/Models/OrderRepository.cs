namespace DomenicoPieShop_Empty.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DomenicosPieShopDbContext domenicoPieSHopDbContext;
        private readonly IShoppingCart _shoppingCart;

        public OrderRepository(DomenicosPieShopDbContext bethanysPieShopDbContext, IShoppingCart shoppingCart)
        {
            domenicoPieSHopDbContext = bethanysPieShopDbContext;
            _shoppingCart = shoppingCart;
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            List<ShoppingCartItem>? shoppingCartItems = _shoppingCart.ShoppingCartItems;
            order.OrderTotal = _shoppingCart.GetShoppingCartTotal();

            order.OrderDetails = new List<OrderDetail>();

            //adding the order with its details

            foreach (ShoppingCartItem? shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = shoppingCartItem.Amount,
                    PieId = shoppingCartItem.Pie.PieId,
                    Price = shoppingCartItem.Pie.Price
                };

                order.OrderDetails.Add(orderDetail);
            }

            domenicoPieSHopDbContext.Orders.Add(order);

            domenicoPieSHopDbContext.SaveChanges();
        }
    }

}
