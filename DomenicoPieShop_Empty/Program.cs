using DomenicoPieShop_Empty.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DomenicosPieShopDbContextConnection") 
    ?? throw new InvalidOperationException("Connection string 'DomenicosPieShopDbContextConnection' not found.");



//builder.Services.AddScoped<ICategoryRepository, MockCategoryRepository>();
//builder.Services.AddScoped<IPieRepository, MockPieRepository>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();


builder.Services.AddScoped<IShoppingCart, ShoppingCart>(sp => ShoppingCart.GetCart(sp));
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {

        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; //ignora i cicli nella serializzazione
        //in questo caso l'ggetto pie ha riferimenti a category e category ha riferimenti a Pie...loop...infinito se nn mettiamo questo atrributo
    });

builder.Services.AddRazorPages();
/*builder.Services.AddControllers();*///controllers per creare un api, ma abbiamo gi� messo AddControllersWithViews e
                                  //pu� bastare. se nn ci fosse stato e dovevamo creare api occorreva metterlo

builder.Services.AddDbContext<DomenicosPieShopDbContext>(options => 
    options.UseSqlServer(connectionString));       

//utilizza i servizi di identity

//specifica il dbcontext
builder.Services.AddDefaultIdentity<IdentityUser>()/*(options => /*l'account deve essere confermato options.SignIn.RequireConfirmedAccount = true)*/
    .AddEntityFrameworkStores<DomenicosPieShopDbContext>();

builder.Services.AddServerSideBlazor();

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
//app.MapDefaultControllerRoute();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

/*app.MapControllers();*///abbiamo gi� MapControllerRoute, se nn ci fosse stato dovevamo metterlo
app.MapRazorPages();//framework come mvc ma nn usa i controller
DbInitializer.Seed(app);

app.MapBlazorHub();
app.MapFallbackToPage("/app/{*catchcall}", "/App/Index");  //tutto cio� che arriva su app/qualcosa che nn esiste ...reindirizza su app/Index
app.Run();
