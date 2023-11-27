using Ecommerce.DAL;
namespace Ecommerce.BLL
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        public IEnumerable<Customer> CustomersWhoByThisProduct(Product product);

        public IEnumerable<Category> GetAllGategories();


        public IEnumerable<Product> Egger_Loading_Products_With_Her_Gategories();

    }
}
