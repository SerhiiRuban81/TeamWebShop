using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Register(RegisterUserDTO userDTO)
        {
            if (!ModelState.IsValid) return View(userDTO);
            
            ShopUser shopUser = new ShopUser
            {
                UserName = userDTO.Email,
                Email = userDTO.Email,
                PhoneNumber = userDTO.PhoneNumber,
                IsPrivatePerson = userDTO.IsPrivatePerson
            };
            var result = await userManager.CreateAsync(shopUser, userDTO.Password);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(shopUser, false);
                return RedirectToAction("Index", "Home");
            }
            else 
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(userDTO);
        }

        public IActionResult Login() => View();

        [HttpPost]

        public async Task<IActionResult> Login(LoginUserDTO dTO)
        {
            if (!ModelState.IsValid)
                return View(dTO);
            ShopUser? shopUser = await userManager.FindByEmailAsync(dTO.Email);
            if (shopUser != null)
            {
                var result = await signInManager.PasswordSignInAsync(shopUser, dTO.Password,
                    dTO.RememberMe, false);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                else
                    ModelState.AddModelError(string.Empty, "Email/Password is incorrect");
            }
            else
                ModelState.AddModelError(string.Empty, "No such user found");
            return View(dTO);
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
