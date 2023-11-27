using Ecommerce.BLL;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.PL.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository customerRepository;
        public CustomerController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        //Show all customers
        public IActionResult Index()
        {
            var customers = customerRepository.GetAll();
            return View(customers);
        }

        public IActionResult CustomerProducts(int id)
        {
            var customer = customerRepository.GetById(id);
            if (customer is null) return NotFound();
            var products = customerRepository.GetCustomerProducts(customer);
            return View(products);
        }

    }
}
