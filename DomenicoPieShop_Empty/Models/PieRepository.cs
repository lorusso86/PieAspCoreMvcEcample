using Microsoft.EntityFrameworkCore;

namespace DomenicoPieShop_Empty.Models
{
    public class PieRepository : IPieRepository
    {
        private readonly DomenicosPieShopDbContext domenicosPieShopDbContext;

        public PieRepository(DomenicosPieShopDbContext domenicosPieShopDbContext)
        {
            this.domenicosPieShopDbContext = domenicosPieShopDbContext;
        }

        public IEnumerable<Pie> AllPies
        {
            get
            {
                return domenicosPieShopDbContext.Pies.Include(c => c.Category);
            }
        }

        public IEnumerable<Pie> PiesOfTheWeek
        {
            get
            {
                return domenicosPieShopDbContext.Pies.Include(c => c.Category).Where( c=> c.IsPieOfTheWeek);
            }
        }
        public Pie? GetPieById(int pieId)
        {
            return domenicosPieShopDbContext.Pies.Where(p => p.PieId == pieId).FirstOrDefault();
        }

        public IEnumerable<Pie> SearchPies(string searchQuery)
        {
            return domenicosPieShopDbContext.Pies.Where(p => p.Name.Contains(searchQuery));
        }
    }
}
