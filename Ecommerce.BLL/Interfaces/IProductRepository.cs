using Ecommerce.DAL;
namespace Ecommerce.BLL
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        public IEnumerable<Customer> CustomersWhoByThisProduct(Product product);


    }
}
