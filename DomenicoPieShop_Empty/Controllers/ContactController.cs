using Microsoft.AspNetCore.Mvc;

namespace DomenicoPieShop_Empty.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
