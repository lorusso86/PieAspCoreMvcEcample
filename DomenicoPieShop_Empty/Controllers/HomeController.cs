using DomenicoPieShop_Empty.Models;
using DomenicoPieShop_Empty.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DomenicoPieShop_Empty.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPieRepository repository;

        public HomeController(IPieRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            var model = new HomeViewModel(repository.PiesOfTheWeek);
            return View(model);
        }
    }
}
