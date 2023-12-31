using System.ComponentModel.DataAnnotations;

namespace Ecommerce.PL
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "First name is required")]
        public string Fname { get; set; }



        [Required(ErrorMessage = "Last name is required")]
        public string Lname { get; set; }



        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address !!")]
        public string Email { get; set; }



        [Required(ErrorMessage = "Phone number is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }



        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password should be matched")]
        public string ConfirmPassword { get; set; }




        [Required(ErrorMessage = "You Should be agree on the licence")]
        public bool isAgree { get; set;}

    }
}
