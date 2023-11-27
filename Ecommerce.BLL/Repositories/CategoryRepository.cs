using Ecommerce.DAL;

namespace Ecommerce.BLL
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly EcommerceDbContext dbContext;
        public CategoryRepository(EcommerceDbContext context) : base(context)
        {
            dbContext = context;
        }

        public IEnumerable<Product> GetCategoryProducts(int id)
        {
            return dbContext.Products.Where(p => p.CategoryId == id).ToList();
        }
    }
}
