using Ecommerce.BLL;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.PL
{
    [Area("Customer")]
    public class CustomerController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public CustomerController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        //Show all customers
        public IActionResult Index()
        {
            var customers = unitOfWork.CustomerRepository.GetAll();
            return View(customers);
        }

        public IActionResult CustomerProducts(int id)
        {
            var customer = unitOfWork.CustomerRepository.GetById(id);
            if (customer is null) return NotFound();
            var products = customer.Products;
            return View(products);
        }

    }
}
