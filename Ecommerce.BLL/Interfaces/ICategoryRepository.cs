using Ecommerce.DAL;

namespace Ecommerce.BLL
{
    public interface ICategoryRepository:IGenericRepository<Category>
    {
        IEnumerable<Product> GetCategoryProducts(int id);
        
    }

}
