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

       

    
    }
}
