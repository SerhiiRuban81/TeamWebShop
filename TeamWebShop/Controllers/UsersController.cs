using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamWebShop.Data;
using TeamWebShop.Models.DTOs.Users;
using TeamWebShop.Models.ViewModels.Users;

namespace TeamWebShop.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<ShopUser> userManager;
        private readonly Mapper mapper;

        public UsersController(UserManager<ShopUser> userManager, Mapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<ShopUser> users = await userManager.Users.
                 ToListAsync();
            IEnumerable<UserDTO> userDTOs = mapper.Map<IEnumerable<UserDTO>>(users);
            return View(userDTOs);
        }

        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null) return NotFound();
            ShopUser? user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound("user not found");
            UserDTO userDTO = mapper.Map<UserDTO>(user);
            return View(userDTO);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserDTO userDTO)
        {
            if (!ModelState.IsValid) return View(userDTO);
            ShopUser? user = await userManager.FindByIdAsync(userDTO.Id.ToString());
            if (user != null)
            {
                user.PhoneNumber = userDTO.PhoneNumber;
                IdentityResult result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

            }
            else
                ModelState.AddModelError(string.Empty, "User not found");
            return View(userDTO);
        }
        public async Task<IActionResult> ChangePassword(string? id)
        {
            if (id == null) return NotFound();
            ShopUser? user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound("User not found");
            ChangePasswordVM vM = new ChangePasswordVM
            {
                Id = user.Id,
                Email = user.Email
            };
            return View(vM);
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM vM)
        {
            if (!ModelState.IsValid) return View(vM);
            ShopUser? user = await userManager.FindByIdAsync(vM.Id);
            if (user == null) return NotFound("User not found");
            var result = await userManager.ChangePasswordAsync(user, vM.OldPassword, vM.NewPassword);
            if (result.Succeeded) return RedirectToAction("Index");
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
            return View(vM);
        }
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null) return NotFound();
            ShopUser? email = await userManager.FindByIdAsync(id);
            if (email == null) return NotFound("User not found");
            UserDTO userDTO = mapper.Map<UserDTO>(email);
            return View(userDTO);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(UserDTO userDTO)
        {
            if (userDTO == null) return NotFound();
            ShopUser? user = await userManager.FindByIdAsync(userDTO.Id.ToString());
            if (user == null) return NotFound("User not found");
            IdentityResult result = await userManager.DeleteAsync(user);
            if (result.Succeeded) return RedirectToAction("Index");
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
            return View(userDTO);

        }
    }
}
