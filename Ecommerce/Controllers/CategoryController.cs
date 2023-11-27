using Ecommerce.BLL;
using Ecommerce.DAL;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.PL.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        //Show all categories
        public IActionResult Index()
        {
            var categories = categoryRepository.GetAll();
            return View(categories);
        }

        //Show category products
        public IActionResult categoryProducts(int id)
        {
            var products = categoryRepository.GetCategoryProducts(id);
            return View(products);
        }

        // create category
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                categoryRepository.Add(category);
                TempData["Message"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            TempData["Message"] = "Category  dont created ";
            return View(category);
        }



        // update category
        [HttpGet]
        public IActionResult Update(int id)
        {
            if(id==0) return BadRequest();
             
            Category category = categoryRepository.GetById(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        public IActionResult Update(Category model)
        {
            if (ModelState.IsValid)// ModelState is a my model (the class) and IsValid is a property
            {
                categoryRepository.Update(model);
                TempData["Message"] = "Category updated successfully";
                //return RedirectToAction("Index");
                return RedirectToAction(nameof(Index));// another way to write the above line
            }
            return View(model);
        }



        // delete category
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            Category category = categoryRepository.GetById(id);
            if (category == null)  return NotFound();
            return View(category);
        }


        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteCategories(int id)
        {
            Category category = categoryRepository.GetById(id);
            if (category == null)  return NotFound();
            categoryRepository.Delete(category);
            TempData["Message"] = "Category deleted successfully";
            return RedirectToAction(nameof(Index));
        }


    }
}
