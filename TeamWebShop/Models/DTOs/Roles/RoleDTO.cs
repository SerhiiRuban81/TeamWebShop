using System.ComponentModel.DataAnnotations;

namespace TeamWebShop.Models.DTOs.Roles
{
    public class RoleDTO
    {
        public string Id { get; set; } = default!;
        [Display(Name = "Role Name")]
        public string Name { get; set; } = default!;
    }
}
