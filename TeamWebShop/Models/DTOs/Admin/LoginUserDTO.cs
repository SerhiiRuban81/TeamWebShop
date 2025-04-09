using System.ComponentModel.DataAnnotations;

namespace TeamWebShop.Models.DTOs.Admin
{
    public class LoginUserDTO
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; } = default!;
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;
        [Display(Name = "Remain in system")]
        public bool RememberMe { get; set; }

    }
}
