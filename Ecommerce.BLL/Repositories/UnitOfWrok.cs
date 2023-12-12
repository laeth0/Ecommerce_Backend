using Ecommerce.DAL;


namespace Ecommerce.BLL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public ICategoryRepository CategoryRepository { get; set; }
        public IProductRepository ProductRepository { get; set; }
        public ICustomerRepository CustomerRepository { get; set; }

        public readonly EcommerceDbContext dbContext;
        public UnitOfWork(EcommerceDbContext dbContext)
        {
            CategoryRepository = new CategoryRepository(dbContext);
            ProductRepository = new ProductRepository(dbContext);
            CustomerRepository = new CustomerRepository(dbContext);
            this.dbContext = dbContext;
        }

        public int Save() => dbContext.SaveChanges();

        public void Dispose() => dbContext.Dispose(); //this method is called when the object is destroyed by the garbage collector. his job is to clean up any unmanaged resources that the object is holding onto.
        
    }
}
