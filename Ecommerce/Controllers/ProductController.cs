using Ecommerce.BLL;
using Ecommerce.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            var products=productRepository.Egger_Loading_Products_With_Her_Gategories();
            return View(products);
        }


        // show customers who by the product 
        public IActionResult CustomersWhoByThisProduct(int productId)
        {
            var product = productRepository.GetById(productId);
            var customers = productRepository.CustomersWhoByThisProduct(product);
            return View(customers);  
        }


        // create category
        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<Category> categories = productRepository.GetAllGategories();
            SelectList selectListItems = new SelectList(categories, "CategoryId", "CategoryName");
            ViewBag.Categories = selectListItems;
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                productRepository.Add(product);
                TempData["Message"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            TempData["Message"] = "Product dont created ";
            return View(product);
        }



        // update category
        [HttpGet]
        public IActionResult Update(int id)
        {

            if (id == 0) return BadRequest();

            Product product = productRepository.GetById(id);
            if (product == null) return NotFound();

            IEnumerable<Category> categories = productRepository.GetAllGategories();
            SelectList selectListItems = new SelectList(categories, "CategoryId", "CategoryName");
            ViewBag.Categories = selectListItems;
             
            return View(product);
        }

        [HttpPost]
        public IActionResult Update(Product product)
        {
            if (ModelState.IsValid)// ModelState is a my model (the class) and IsValid is a property
            {
                productRepository.Update(product);
                TempData["Message"] = "Category updated successfully";
                //return RedirectToAction("Index");
                return RedirectToAction(nameof(Index));// another way to write the above line
            }
            return View(product);
        }



        // delete category
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            Product product = productRepository.GetById(id);
            if (product == null) return NotFound();

            IEnumerable<Category> categories = productRepository.GetAllGategories();
            SelectList selectListItems = new SelectList(categories, "CategoryId", "CategoryName");
            ViewBag.Categories = selectListItems;

            return View(product);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteCategories(int id)
        {
            Product product = productRepository.GetById(id);
            if (product == null) return NotFound();
            productRepository.Delete(product);
            TempData["Message"] = "Category deleted successfully";
            return RedirectToAction(nameof(Index));
        }


    }
}
