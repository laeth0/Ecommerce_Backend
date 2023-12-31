using AutoMapper;
using Ecommerce.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.PL.Areas.Users.Controllers
{
    [Area("Users")]
    //[Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,IMapper mapper)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
        }


        
        public async Task<IActionResult> Index()
        {
            var users = userManager.Users.Select(U => new UsersViewModel()
            {
                Id = U.Id,
                Fname = U.Fname,
                Lname = U.Lname,
                Email = U.Email,
                Roles = userManager.GetRolesAsync(U).Result // we use result because it is a task (mean convert the method from async to sync)
            }).ToListAsync();

            return View(users);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var mappedUser = mapper.Map<ApplicationUser, UsersViewModel>(user);
            return View(mappedUser);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UsersViewModel model,[FromRoute] string id) //to get the id from the route
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.Id);
                var mappedUser = mapper.Map(model, user);

                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var allRoles = await roleManager.Roles.ToListAsync();
            var roleSelectListItem= allRoles.Select(R => new SelectListItem
            {
                Text = R.Name,
                Value = R.Name
            });
            ViewBag.roles = roleSelectListItem;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UsersViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mappedUser = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Fname = model.Email.Split('@')[0],
                    Lname = model.Lname,
                    Email = model.Email,
                    UserName = model.Email
                }; 
                
                var result = await userManager.CreateAsync(mappedUser, model.Password);
                if (result.Succeeded)
                {
                    if (model.Roles is not null && model.Roles.Any() )
                    {
                        var addedRole = await userManager.AddToRolesAsync(mappedUser, model.Roles);
                        if (addedRole.Succeeded)
                            return RedirectToAction("Index");
                        else
                            ModelState.AddModelError(string.Empty, "Failed to add role");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to add role");
                }

            }


            return View(model);
        }





    }
}
