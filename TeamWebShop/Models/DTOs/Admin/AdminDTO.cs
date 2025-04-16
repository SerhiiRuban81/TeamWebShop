using System.ComponentModel.DataAnnotations;

namespace TeamWebShop.Models.DTOs.Admins
{
    public class AdminDTO
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = default!;

        [Required]
        [Display(Name = "Role")]
        public string Role { get; set; } = "Admin";

        [Display(Name = "Permissions")]
        public List<string>? Permissions { get; set; }
    }
}
