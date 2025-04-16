using Microsoft.AspNetCore.Mvc;
using TeamWebShop.Models.DTOs.Admins;

namespace TeamWebShop.Controllers
{
    public class AdminController : Controller
    {
        private static readonly List<AdminDTO> admins = new()
        {
            new AdminDTO { Id = 1, Email = "admin1@gmail.com", Permissions = new List<string> { "ManageUsers", "ViewReports" } },
            new AdminDTO { Id = 2, Email = "admin2@gmail.com", Permissions = new List<string> { "ManageProducts" } }
        };

        public IActionResult Index()
        {
            return View(admins);
        }

        public IActionResult Details(int id)
        {
            var admin = admins.FirstOrDefault(a => a.Id == id);
            if (admin == null) return NotFound();
            return View(admin);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AdminDTO newAdmin)
        {
            if (ModelState.IsValid)
            {
                newAdmin.Id = admins.Max(a => a.Id) + 1;
                admins.Add(newAdmin);
                return RedirectToAction(nameof(Index));
            }
            return View(newAdmin);
        }
        public IActionResult Edit(int id)
        {
            var admin = admins.FirstOrDefault(a => a.Id == id);
            if (admin == null) return NotFound();
            return View(admin);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, AdminDTO updatedAdmin)
        {
            if (!ModelState.IsValid) return View(updatedAdmin);

            var existingAdmin = admins.FirstOrDefault(a => a.Id == id);
            if (existingAdmin == null) return NotFound();

            existingAdmin.Email = updatedAdmin.Email;
            existingAdmin.Role = updatedAdmin.Role;
            existingAdmin.Permissions = updatedAdmin.Permissions;

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var admin = admins.FirstOrDefault(a => a.Id == id);
            if (admin == null) return NotFound();
            return View(admin);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var admin = admins.FirstOrDefault(a => a.Id == id);
            if (admin != null)
            {
                admins.Remove(admin);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
