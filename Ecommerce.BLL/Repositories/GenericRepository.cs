using Ecommerce.BLL;
using Ecommerce.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
           return dbContext.Set<T>().ToList();
        }

        public int Add(T entity)
        {
            dbContext.Set<T>().Add(entity);
            return dbContext.SaveChanges();  
         
        }

        public int Delete(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            return dbContext.SaveChanges();
        }


        public int Update(T entity)
        {
            dbContext.Set<T>().Update(entity);
            return dbContext.SaveChanges();
        }
    }
}
