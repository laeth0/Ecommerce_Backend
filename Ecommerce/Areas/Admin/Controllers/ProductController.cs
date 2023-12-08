using AutoMapper;
using Ecommerce.BLL;
using Ecommerce.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce.PL
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        //Show all products
        [HttpGet]
        public IActionResult Index()
        {
            var products = unitOfWork.ProductRepository.GetAll();
            if (products == null) return BadRequest();
            var mappedProducts = mapper.Map< IEnumerable<Product>, IEnumerable<ProductViewModel> >(products);
            return View(mappedProducts);
        }


        // show customers who by the product 
        public IActionResult CustomersWhoByThisProduct(int productId)
        {
            var product = unitOfWork.ProductRepository.GetById(productId);
            if (product == null) return NotFound();
            var customers = unitOfWork.ProductRepository.CustomersWhoByThisProduct(product);
            if (customers == null) return NotFound();
            var mappedCustomers = mapper.Map< IEnumerable<Customer>, IEnumerable<CustomerViewModel> >(customers);
            return View(mappedCustomers);
        }


        // create category
        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<Category> categories = unitOfWork.CategoryRepository.GetAll();
            SelectList selectListItems = new SelectList(categories, "CategoryId", "CategoryName");
            // this is another way to write the above line
            //IEnumerable<SelectListItem> CategorySelectListItems = unitOfWork.CategoryRepository.GetAll()
            //    .Select(c => new SelectListItem
            //{
            //    Text = c.CategoryName,
            //    Value = c.CategoryId.ToString() // this should be a string because the value of the select list is a string
            //});

            ViewBag.Categories = selectListItems;
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(ProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                productVM.ImageURL=FileManagement.UploadFile(productVM.Image,"images");
                var mappedProduct = mapper.Map<ProductViewModel,Product>(productVM);
                unitOfWork.ProductRepository.Add(mappedProduct);
                unitOfWork.Save();
                TempData["Message"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            TempData["Message"] = "Product dont created ";
            return View(productVM);
        }



        // update category
        [HttpGet]
        public IActionResult Update(int id)
        {

            if (id == 0) return BadRequest();

            Product product = unitOfWork.ProductRepository.GetById(id);
            if (product == null) return NotFound();

            IEnumerable<Category> categories = unitOfWork.CategoryRepository.GetAll();
            SelectList selectListItems = new SelectList(categories, "CategoryId", "CategoryName");
            ViewBag.Categories = selectListItems;

            var mappedProduct = mapper.Map<Product, ProductViewModel>(product);
            return View(mappedProduct);
        }

        [HttpPost]
        public IActionResult Update(ProductViewModel productVM)
        {
            if (ModelState.IsValid)// ModelState is a my model (the class) and IsValid is a property
            {
                var mappedProduct = mapper.Map<ProductViewModel, Product>(productVM);
                unitOfWork.ProductRepository.Update(mappedProduct);
                unitOfWork.Save();
                TempData["Message"] = "Category updated successfully";
                //return RedirectToAction("Index");
                return RedirectToAction(nameof(Index));// another way to write the above line
            }
            return View(productVM);
        }



        // delete category
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            Product product = unitOfWork.ProductRepository.GetById(id);
            if (product == null) return NotFound();

            IEnumerable<Category> categories = unitOfWork.CategoryRepository.GetAll();
            SelectList selectListItems = new SelectList(categories, "CategoryId", "CategoryName");
            ViewBag.Categories = selectListItems;

            var mappedProduct = mapper.Map<Product, ProductViewModel>(product);
            return View(mappedProduct);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteCategories(int id)
        {
            Product product = unitOfWork.ProductRepository.GetById(id);
            if (product == null) return NotFound();
            unitOfWork.ProductRepository.Delete(product);
            unitOfWork.Save();
            TempData["Message"] = "Category deleted successfully";
            return RedirectToAction(nameof(Index));
        }


    }
}
