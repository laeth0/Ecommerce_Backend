using Ecommerce.DAL;


namespace Ecommerce.BLL
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        IEnumerable<Product> GetCustomerProducts(Customer customer);
    }
}
