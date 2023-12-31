using Ecommerce.DAL;


namespace Ecommerce.BLL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public ICategoryRepository CategoryRepository { get; set; }
        public IProductRepository ProductRepository { get; set; }

        public readonly EcommerceDbContext dbContext;
        public UnitOfWork(EcommerceDbContext dbContext)
        {
            CategoryRepository = new CategoryRepository(dbContext);
            ProductRepository = new ProductRepository(dbContext);
            this.dbContext = dbContext;
        }

        public async Task<int> Save() => await dbContext.SaveChangesAsync();

        public void Dispose() => dbContext.Dispose(); //this method is called when the object is destroyed by the garbage collector. his job is to clean up any unmanaged resources that the object is holding onto.
        
    }
}
