using Ecommerce.DAL;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.BLL
{


    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly EcommerceDbContext dbContext;
        public GenericRepository(EcommerceDbContext context)
        {
            dbContext = context;
        }

        public T GetById(int id)
        {
            return dbContext.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Product))
            {
                return (IEnumerable<T>) dbContext.Products.Include(p => p.Category).ToList();
            }
           return dbContext.Set<T>().ToList();
        }


        public void Add(T entity)
        {
            dbContext.Set<T>().Add(entity);
         
        }

        public void Delete(T entity)
        {
            dbContext.Set<T>().Remove(entity);
        }


        public void Update(T entity)
        {
            dbContext.Set<T>().Update(entity);
        }

    
    }
}
