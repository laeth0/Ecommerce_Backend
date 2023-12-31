

namespace Ecommerce.BLL
{
    public interface IUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get; set; }
        public IProductRepository ProductRepository { get; set; }

        public Task<int> Save();

    }
}
