using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamWebShop.Data;
using TeamWebShop.Models.DTOs.Roles;
using TeamWebShop.Models.DTOs.Users;
using TeamWebShop.Models.ViewModels.Roles;

namespace TeamWebShop.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ShopUser> userManager;
        private readonly IMapper mapper;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ShopUser> userManager, IMapper mapper)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await roleManager.Roles.ToListAsync();
            IEnumerable<RoleDTO> roleDTOs = mapper.Map<IEnumerable<RoleDTO>>(roles);
            return View(roleDTOs);
        }
        public IActionResult CreateRole() => View();

        [HttpPost]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                ModelState.AddModelError(string.Empty, "Role name cannot be empty!");
                return View(model: roleName);
            }
            IdentityRole role = new IdentityRole { Name = roleName };
            await roleManager.CreateAsync(role);
            return RedirectToAction("Index");

        }
        public async Task<ActionResult> UserList()
        {
            IEnumerable<ShopUser> users = await userManager.Users.ToListAsync();
            IEnumerable<UserDTO> userDTOs = mapper.Map<IEnumerable<UserDTO>>(users);
            return View(userDTOs);


        }
        public async Task<IActionResult> ChangeRole(string? id)
        {

            if (id == null) return NotFound();
            ShopUser? user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var listRoles = await roleManager.Roles.ToListAsync();
            var userRoles = await userManager.GetRolesAsync(user);
            ChangeRoleVM vM = new ChangeRoleVM
            {
                Id = user.Id,
                Email = user.Email,
                ListRoles = listRoles,
                UserRoles = userRoles
            };
            return View(vM);
        }
        [HttpPost]
        public async Task<IActionResult> ChangeRole(ChangeRoleVM vM)
        {
            ShopUser? user = await userManager.FindByIdAsync(vM.Id);
            if (user == null) return NotFound();
            var listRoles = await roleManager.Roles.ToListAsync();
            var userRoles = await userManager.GetRolesAsync(user);
            if (ModelState.IsValid)
            {
                var addetRoles = vM.Roles.Except(userRoles);
                var removedRoles = userRoles.Except(vM.Roles);
                await userManager.AddToRolesAsync(user, addetRoles);
                await userManager.RemoveFromRolesAsync(user, removedRoles);
                return RedirectToAction("UserList");

            }
            vM.ListRoles = listRoles;
            vM.UserRoles = userRoles;
            vM.Email = user.Email;
            return View(vM);
        }
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null) return NotFound();
            IdentityRole? role = await roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();
            RoleDTO roleDTO = mapper.Map<RoleDTO>(role);
            return View(roleDTO);
        }
        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditConfirmed(RoleDTO roleDTO)
        {
            if (!ModelState.IsValid) return View(roleDTO);
            IdentityRole? role = await roleManager.FindByIdAsync(roleDTO.Id.ToString());
            if (role == null) return NotFound();
            role.Name = roleDTO.Name;
            IdentityResult result = await roleManager.UpdateAsync(role);
            if (result.Succeeded)
                return RedirectToAction("Index");
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
            return View(roleDTO);
        }
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null) return NotFound();
            IdentityRole? role = await roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();
            RoleDTO roleDTO = mapper.Map<RoleDTO>(role);
            return View(roleDTO);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string? id)
        {
            if (id == null) return NotFound();
            IdentityRole? role = await roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();
            await roleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }

    }
}
