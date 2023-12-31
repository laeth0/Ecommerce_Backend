using Ecommerce.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.PL.Areas.Security.Controllers
{
    [Area("Security")]
    public class SecurityController : Controller
    {


        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public SecurityController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            { 
                var user= new ApplicationUser
                {
                    UserName = model.Email.Split("@")[0],
                    Fname = model.Fname,
                    Lname = model.Lname,
                    Email = model.Email,
                    isAgree= model.isAgree
                };

                var result = await userManager.CreateAsync(user, model.Password); // CreateAsync method accept two parameters (user, password)
                if (result.Succeeded)
                {
                    RedirectToAction(nameof(Login));
                }
                else
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description); // Description is a property of IdentityError class
                
            }
            return View(model);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid) // server side validation
            {
                var user = await userManager.FindByEmailAsync(model.Email); // this method return user object if the user is exist or null if the user is not exist 
                if (user != null)
                {
                    bool flag = await userManager.CheckPasswordAsync(user,model.Password);
                    if (flag)
                    {
                        var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                        if (result.Succeeded)
                            return RedirectToAction("Index", "Home", new { area = "Customer" });
                        else
                            ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                    }
                }

                TempData["ErrorMessage"] = "Email or Password is incorrect";

            }
            return View(model);
        }


        public new async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user =await userManager.FindByEmailAsync(model.Email);
                if(user is not null)
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);
                    string ResetPasswordLink = Url.Action("ResetPassword", "Security", new { email = model.Email ,token }, Request.Scheme);

                    var email = new Email()
                    {
                        Subject = "Reset Password",
                        Body = ResetPasswordLink,
                        To = model.Email
                    };

                    EmailManagement.SendEmail(email);
                    RedirectToAction("CheckInbox");
                }
            }
            return View(model);
        }



        [HttpGet]
        public IActionResult CheckInbox()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string email,string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {

            if (ModelState.IsValid) 
            { 
                var email = TempData["email"] as string;
                var token = TempData["token"] as string;
                var user =await userManager.FindByEmailAsync(email);

          
                var result = await userManager.ResetPasswordAsync(user, token, model.newPassword);
                if (result.Succeeded)
                    RedirectToAction("Login");
                else
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                

            }
            return View(model);
        }













    }
}
