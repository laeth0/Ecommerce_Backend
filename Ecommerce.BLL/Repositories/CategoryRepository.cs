using Ecommerce.DAL;
using Microsoft.EntityFrameworkCore;


namespace Ecommerce.BLL
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly EcommerceDbContext dbContext;
        public CategoryRepository(EcommerceDbContext context) : base(context)
        {
            dbContext = context;
        }

        public IEnumerable<Product> GetCategoryProducts(int Category_id)
        {
            return dbContext.Categories.FirstOrDefault(c => c.CategoryId == Category_id).Products;

        }

    
    }
}
