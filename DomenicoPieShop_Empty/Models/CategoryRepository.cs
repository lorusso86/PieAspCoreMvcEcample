namespace DomenicoPieShop_Empty.Models
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly DomenicosPieShopDbContext domenicosPieShopDbContext;

        public CategoryRepository(DomenicosPieShopDbContext domenicosPieShopDbContext)
        {
            this.domenicosPieShopDbContext = domenicosPieShopDbContext;
        }

        public IEnumerable<Category> AllCategories
        {
            get
            {
                return domenicosPieShopDbContext.Categories.OrderBy(c => c.CategoryName);
            }
        }
    }
}
