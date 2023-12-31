using AutoMapper;
using Ecommerce.BLL;
using Ecommerce.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.PL
{
    [Area("Admin")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        //Show all categories
        public async Task<IActionResult> Index()
        {
            var categories =await unitOfWork.CategoryRepository.GetAll();
            if (categories == null) return BadRequest();
            var mappedCategories = mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(categories);
            return View(mappedCategories);
        }

        //Show category products
        public async Task<IActionResult> categoryProducts(int CategoryId)
        {
            Category category = await unitOfWork.CategoryRepository.GetById(CategoryId);
            IEnumerable<Product> products = category.Products;
            if (products == null) return BadRequest();
            var mappedProducts = mapper.Map< IEnumerable<Product> , IEnumerable<ProductViewModel> >(products);
            var CategoryName = (await unitOfWork.CategoryRepository.GetById(CategoryId)).CategoryName;
            ViewBag.CategoryName = CategoryName;
            return View(mappedProducts);
        }

        // create category
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel categoryVM)
        {
            if (ModelState.IsValid)
            {
                var mappedCategory = mapper.Map<CategoryViewModel, Category>(categoryVM);
                await unitOfWork.CategoryRepository.Add(mappedCategory);
                await unitOfWork.Save();
                TempData["Message"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            TempData["Message"] = "Category dont created";
            return View(categoryVM);
        }



        // update category
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            if (id == 0) return BadRequest();

            Category category =await unitOfWork.CategoryRepository.GetById(id);
            if (category == null) return NotFound();
            var mappedCategory = mapper.Map<Category, CategoryViewModel>(category);
            return View(mappedCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryViewModel categoryVM)
        {
            if (ModelState.IsValid)// ModelState is a my model (the class) and IsValid is a property
            {
                Category mappedCategory = mapper.Map<CategoryViewModel, Category>(categoryVM);
                unitOfWork.CategoryRepository.Update(mappedCategory);
                await unitOfWork.Save();
                TempData["Message"] = "Category updated successfully";
                //return RedirectToAction("Index");
                return RedirectToAction(nameof(Index));// another way to write the above line
            }
            return View(categoryVM);
        }



        // delete category
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) return BadRequest();
            Category category =await unitOfWork.CategoryRepository.GetById(id);
            if (category == null) return NotFound();
            var mappedCategory = mapper.Map<Category, CategoryViewModel>(category);
            return View(mappedCategory);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteCategories(int id)
        {
            Category category =await unitOfWork.CategoryRepository.GetById(id);
            if (category == null) return NotFound();
            unitOfWork.CategoryRepository.Delete(category);
            await unitOfWork.Save();
            TempData["Message"] = "Category deleted successfully";
            return RedirectToAction(nameof(Index));
        }


    }
}
