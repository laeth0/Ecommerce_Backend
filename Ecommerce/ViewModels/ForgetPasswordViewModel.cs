using System.ComponentModel.DataAnnotations;

namespace Ecommerce.PL
{
    public class ForgetPasswordViewModel
    {

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address !!")]
        public string Email { get; set; }

    }
}
