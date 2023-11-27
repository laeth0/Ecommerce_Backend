

namespace Ecommerce.BLL
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T GetById(int id);

        int Add(T entity);

        int Update(T entity);

        int Delete(T entity);

        
    }
}
