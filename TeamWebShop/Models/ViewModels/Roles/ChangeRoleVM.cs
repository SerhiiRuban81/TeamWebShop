using Microsoft.AspNetCore.Identity;

namespace TeamWebShop.Models.ViewModels.Roles
{
    public class ChangeRoleVM
    {
        public string Id { get; set; } = default!;
        public string? Email { get; set; } = default!;
        public IEnumerable<IdentityRole>? ListRoles { get; set; } = default!;
        public IList<string>? UserRoles { get; set; } = default!;
        public IEnumerable<string> Roles { get; set; } = default!;

    }
}
