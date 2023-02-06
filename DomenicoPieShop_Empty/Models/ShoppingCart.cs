using Microsoft.EntityFrameworkCore;

namespace DomenicoPieShop_Empty.Models;

public class ShoppingCart : IShoppingCart
{
    private readonly DomenicosPieShopDbContext domenicosPieShopDbContext;

    public string? ShoppingCartId { get; set; }

    public List<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;

    private ShoppingCart(DomenicosPieShopDbContext domenicosPieShopDbContext)
    {
        this.domenicosPieShopDbContext = domenicosPieShopDbContext;
    }

    public static ShoppingCart GetCart(IServiceProvider services)
    {
        ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

        DomenicosPieShopDbContext context = services.GetService<DomenicosPieShopDbContext>() ?? throw new Exception("Error initializing");

        string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();

        session?.SetString("CartId", cartId);

        return new ShoppingCart(context) { ShoppingCartId = cartId };
    }

    public void AddToCart(Pie pie)
    {
        var shoppingCartItem =
                domenicosPieShopDbContext.ShoppingCartItems.SingleOrDefault(
                    s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);

        if (shoppingCartItem == null)
        {
            shoppingCartItem = new ShoppingCartItem
            {
                ShoppingCartId = ShoppingCartId,
                Pie = pie,
                Amount = 1
            };

            domenicosPieShopDbContext.ShoppingCartItems.Add(shoppingCartItem);
        }
        else
        {
            shoppingCartItem.Amount++;
        }
        domenicosPieShopDbContext.SaveChanges();
    }

    public int RemoveFromCart(Pie pie)
    {
        var shoppingCartItem =
                domenicosPieShopDbContext.ShoppingCartItems.SingleOrDefault(
                    s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);

        var localAmount = 0;

        if (shoppingCartItem != null)
        {
            if (shoppingCartItem.Amount > 1)
            {
                shoppingCartItem.Amount--;
                localAmount = shoppingCartItem.Amount;
            }
            else
            {
                domenicosPieShopDbContext.ShoppingCartItems.Remove(shoppingCartItem);
            }
        }

        domenicosPieShopDbContext.SaveChanges();

        return localAmount;
    }

    public List<ShoppingCartItem> GetShoppingCartItems()
    {
        return ShoppingCartItems ??=
                   domenicosPieShopDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                       .Include(s => s.Pie)
                       .ToList();
    }

    public void ClearCart()
    {
        var cartItems = domenicosPieShopDbContext
            .ShoppingCartItems
            .Where(cart => cart.ShoppingCartId == ShoppingCartId);

        domenicosPieShopDbContext.ShoppingCartItems.RemoveRange(cartItems);

        domenicosPieShopDbContext.SaveChanges();
    }

    public decimal GetShoppingCartTotal()
    {
        var total = domenicosPieShopDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
            .Select(c => c.Pie.Price * c.Amount).Sum();
        return total;
    }
}