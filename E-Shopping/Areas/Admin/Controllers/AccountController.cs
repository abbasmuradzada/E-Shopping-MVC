using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Shopping.AppCode;
using E_Shopping.Models.Entity.Membership;
using E_Shopping.Models.FormModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Shopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        readonly SignInManager<ShopUser> signInManager;
        readonly UserManager<ShopUser> userManager;

        public AccountController(SignInManager<ShopUser> signInManager,
        UserManager<ShopUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        public IActionResult SignIn()
        {
            return View(new UserFormModel
            {
                UserName = "abbasam@code.edu.az",
                Password = "123"
            });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(UserFormModel model)
        {
            ShopUser user = null;

            if (model.UserName.IsEmail())
            {
                user = await userManager.FindByEmailAsync(model.UserName);
            }
            else
            {
                user = await userManager.FindByNameAsync(model.UserName);
            }

            if (user == null)
            {
                ModelState.AddModelError("UserName", "İstifadəçi adı və ya şifrə səhvdir");
            }

            if (user != null && !user.EmailConfirmed)
            {
                ModelState.AddModelError("UserName", "Email hesabınızı təsdiq edin");
            }



            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, true, true);


                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home", routeValues: new
                    {
                        area = "Admin"
                    });
                }
                else
                {
                    ModelState.AddModelError("UserName", "İstifadəçi adı və ya şifrə səhvdir");
                }

            }


            return View(model);
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
