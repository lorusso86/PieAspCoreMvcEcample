using DomenicoPieShop_Empty.Models;
using DomenicoPieShop_Empty.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DomenicoPieShop_Empty.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository pieRepository;
        private readonly ICategoryRepository categoryRepository;
        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            this.pieRepository = pieRepository;
            this.categoryRepository = categoryRepository;
        }
        //public IActionResult List()
        //{
        //    var viewModel = new PieListViewModel(pieRepository.AllPies, "All Pies");
        //    return View(viewModel);
        //}
        public ViewResult List(string category)
        {
            IEnumerable<Pie> pies;
            string? currentCategory;

            if (string.IsNullOrEmpty(category))
            {
                pies = pieRepository.AllPies.OrderBy(p => p.PieId);
                currentCategory = "All pies";
            }
            else
            {
                pies = pieRepository.AllPies.Where(p => p.Category.CategoryName == category)
                    .OrderBy(p => p.PieId);
                currentCategory = categoryRepository.AllCategories.FirstOrDefault(c => c.CategoryName == category)?.CategoryName;
            }

            return View(new PieListViewModel(pies, currentCategory));
        }

        public IActionResult Details(int id)
        {

            var pie =  pieRepository.GetPieById(id);
            if (pie == null)
                return NotFound();

            return View(pie); 
        }
        public IActionResult Search()
        { return View(); }
    }
}
