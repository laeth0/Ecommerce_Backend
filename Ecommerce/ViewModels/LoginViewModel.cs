using System.ComponentModel.DataAnnotations;

namespace Ecommerce.PL
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address !!")]
        public string Email { get; set; }



        [Required(ErrorMessage = "Phone number is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
