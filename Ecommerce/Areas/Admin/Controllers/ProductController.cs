using AutoMapper;
using Ecommerce.BLL;
using Ecommerce.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ecommerce.PL
{
    [Area("Admin")]
    [Authorize]
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
        public async Task<IActionResult> Index()
        {
            var products =await unitOfWork.ProductRepository.GetAll();
            if (products == null) return BadRequest();
            var mappedProducts = mapper.Map< IEnumerable<Product>, IEnumerable<ProductViewModel> >(products);
            return View(mappedProducts);
        }

        // create category
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            IEnumerable<Category> categories =await unitOfWork.CategoryRepository.GetAll();
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
        public async Task<IActionResult> Create(ProductViewModel productVM)
        {
            if (ModelState.IsValid)
            {
                productVM.ImageURL=await FileManagement.UploadFile(productVM.Image,"images");
                var mappedProduct = mapper.Map<ProductViewModel,Product>(productVM);
                await unitOfWork.ProductRepository.Add(mappedProduct);
                await unitOfWork.Save();
                TempData["Message"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            TempData["Message"] = "Product dont created ";
            return View(productVM);
        }



        // update category
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            if (id == 0) return BadRequest();

            Product product =await unitOfWork.ProductRepository.GetById(id);
            if (product == null) return NotFound();

            IEnumerable<Category> categories =await unitOfWork.CategoryRepository.GetAll();
            SelectList selectListItems = new SelectList(categories, "CategoryId", "CategoryName");
            ViewBag.Categories = selectListItems;
            ViewBag.ProductCategorieName = product.Category.CategoryName;

            var mappedProduct = mapper.Map<Product, ProductViewModel>(product);
            return View(mappedProduct);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductViewModel productVM)
        {
            if (ModelState.IsValid)// ModelState is a my model (the class) and IsValid is a property
            {
                //productVM.ImageURL = FileManagement.UploadFile(productVM.Image, "images");
                if (productVM.Image != null)
                {
                    if ( !string.IsNullOrEmpty(productVM.ImageURL) )
                        FileManagement.DeleteFile(productVM.ImageURL, "images");

                    productVM.ImageURL =await FileManagement.UploadFile(productVM.Image, "images");
                }

                var mappedProduct = mapper.Map<ProductViewModel, Product>(productVM);
                unitOfWork.ProductRepository.Update(mappedProduct);
                await unitOfWork.Save();
                TempData["Message"] = "Category updated successfully";
                //return RedirectToAction("Index");
                return RedirectToAction(nameof(Index));// another way to write the above line
            }
            return View(productVM);
        }


        // delete category
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) return BadRequest();
            Product product =await unitOfWork.ProductRepository.GetById(id);
            if (product == null) return NotFound();
            var mappedProduct = mapper.Map<Product, ProductViewModel>(product);
            return View(mappedProduct);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            Product product =await unitOfWork.ProductRepository.GetById(id);
            if (product == null) return NotFound();
            unitOfWork.ProductRepository.Delete(product);
            FileManagement.DeleteFile(product.ImageURL, "images");
            await unitOfWork.Save();
            TempData["Message"] = "Category deleted successfully";
            return RedirectToAction(nameof(Index));
        }


 

        /* [HttpGet]
         public IActionResult getall()
         {
             var products = unitOfWork.ProductRepository.GetAll();
             return Json(new { data = products });
         } */
        //[HttpGet]
        //public async Task<IActionResult> getAll()
        //{
        //    var products =await unitOfWork.ProductRepository.GetAll();

        //    // Create Json Serialize Options with ReferenceHandler.Preserve
        //    JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        //    {
        //        ReferenceHandler = ReferenceHandler.Preserve,
        //        // Other options as needed
        //    };

        //    // Serialize the data using JsonSerializerOptions
        //    var jsonData = JsonSerializer.Serialize(new { data = products }, jsonSerializerOptions);
        //    return Content(jsonData, "application/json");
        //}



    }
}
