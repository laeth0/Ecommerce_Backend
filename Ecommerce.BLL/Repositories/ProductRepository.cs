using Ecommerce.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public IEnumerable<Product> Egger_Loading_Products_With_Her_Gategories()
        {
            return dbContext.Products.Include(p=> p.Category).ToList();
        }

        public IEnumerable<Category> GetAllGategories()
        {
            return dbContext.Categories.ToList();
        }
    }

}
