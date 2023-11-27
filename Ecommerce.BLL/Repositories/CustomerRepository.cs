using Ecommerce.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BLL.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly EcommerceDbContext dbContext;
        public CustomerRepository(EcommerceDbContext context) : base(context)
        {
            dbContext = context;
        }


        public IEnumerable<Product> GetCustomerProducts(Customer customer)
        {   
            return customer.Products.ToList();
        }
    }
}
