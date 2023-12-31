using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Ecommerce.DAL
{    //  DbContext
    public class EcommerceDbContext:IdentityDbContext<ApplicationUser>
    {
        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // this is for IdentityDbContext
        }
        
     

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }



    }
}
