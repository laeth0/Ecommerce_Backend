using Ecommerce.DAL;


namespace Ecommerce.BLL
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly EcommerceDbContext dbContext;
        public CustomerRepository(EcommerceDbContext context) : base(context)
        {
            dbContext = context;
        }


     
    }
}
