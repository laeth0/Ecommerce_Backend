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

        public async Task<T> GetById(int id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            if (typeof(T) == typeof(Product))
            {
                return (IEnumerable<T>) await dbContext.Products.Include(p => p.Category).ToListAsync();
            }
           return await dbContext.Set<T>().ToListAsync();
        }


        public async Task Add(T entity)
        {
           await dbContext.Set<T>().AddAsync(entity);
         
        }

        public void Delete(T entity)
        {
            dbContext.Set<T>().Remove(entity);// => there is no async method for remove
        }


        public void Update(T entity)
        {
            dbContext.Set<T>().Update(entity);
        }

    
    }
}
