using Ecommerce.BLL;
using Ecommerce.DAL;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<EcommerceDbContext>(options=>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //builder.Services.AddScoped<IProductRepository, ProductRepository>();
            //builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            //builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWrok));

            //services of lifetime for dependency injection
            //AddScoped => the object will be created when the request is created and when the request is finished the object will be destroyed
            //AddSingleton => the will still in memory until the application is running and when the application is closed the object will be destroyed
            //AddTransient => 

            builder.Services.AddAutoMapper(m=> m.AddProfile<ProductProfile>() );
            builder.Services.AddAutoMapper(m=> m.AddProfile<CategoryProfile>() );
            builder.Services.AddAutoMapper(m=> m.AddProfile<CustomerProfile>() );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute( name: "default",
                pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");
            
            app.Run();
        }
    }
}