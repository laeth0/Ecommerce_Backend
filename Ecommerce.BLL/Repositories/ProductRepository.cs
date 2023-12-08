using Ecommerce.DAL;
using Microsoft.EntityFrameworkCore;


namespace Ecommerce.BLL
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly EcommerceDbContext dbContext;
        public ProductRepository(EcommerceDbContext context):base(context)
        {
            dbContext = context;
        }

        public IEnumerable<Customer> CustomersWhoByThisProduct(Product product)
        {
            var customers = product.Customers;
            return customers;
        }
    
    }

}
