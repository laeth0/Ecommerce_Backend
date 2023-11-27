using Ecommerce.BLL;
using Ecommerce.DAL;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.PL.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;
        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        //Show all products
        [HttpGet]
        public IActionResult Index()
        {
            var products=productRepository.GetAll();
            return View(products);
        }


        // show customers who by the product 
        public IActionResult CustomersWhoByThisProduct(int productId)
        {
            var product = productRepository.GetById(productId);
            var customers = productRepository.CustomersWhoByThisProduct(product);
            return View(customers);  
        }



    }
}
