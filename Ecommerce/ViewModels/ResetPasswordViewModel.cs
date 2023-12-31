using System.ComponentModel.DataAnnotations;

namespace Ecommerce.PL
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Phone number is required")]
        [DataType(DataType.Password)]
        public string newPassword { get; set; }



        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("newPassword", ErrorMessage = "Password and Confirm Password should be matched")]
        public string ConfirmPassword { get; set; }

    }
}
