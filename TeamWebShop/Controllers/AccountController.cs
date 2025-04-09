using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamWebShop.Data;
using TeamWebShop.Models.DTOs.Admin;

namespace TeamWebShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ShopUser> userManager;
        private readonly SignInManager<ShopUser> signInManager;

        public AccountController(UserManager<ShopUser> userManager,
            SignInManager<ShopUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register() => View();


        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserDTO dTO)
        {
            if (!ModelState.IsValid)
                return View(dTO);
            ShopUser shopUser = new ShopUser
            {
                Email = dTO.Email,
                IsPrivatePerson = dTO.IsPrivatePerson,
                PhoneNumber = dTO.PhoneNumber
            };
            var result = await userManager.CreateAsync(shopUser, dTO.Password);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(shopUser, false); // false meanse do not remain in system
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(dTO);
        }


        public IActionResult Login() => View();

    }
}
