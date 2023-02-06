using DomenicoPieShop_Empty.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DomenicoPieShop_Empty.Controllers.Api
{
    [Route("api/[controller]")]//api hanno il route basato sugli attributi e
                               //non sul nome dei metodi con i controllers delle view
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IPieRepository pieRepository;

        public SearchController(IPieRepository pieRepository)
        {
            this.pieRepository = pieRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var allpies = pieRepository.AllPies;
            return Ok(allpies);//converte in automatico in json



        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (!pieRepository.AllPies.Any(p => p.PieId == id))
                return NotFound();

            return Ok(pieRepository.AllPies.Where(p => p.PieId == id));
        }
        [HttpPost]
        public IActionResult  SearchPies([FromBody]string searchQuery)
        {
            IEnumerable<Pie> pies = new List<Pie>();

          if(!string.IsNullOrEmpty(searchQuery))
            {
                pies = pieRepository.SearchPies(searchQuery);

            }

            return new JsonResult(pies);//vale come l'ok(pies) e contiene anche l'esitlo della richiesto
            //errore 200, 404,400...


        }

    }
}
