using System.ComponentModel.DataAnnotations;

namespace TeamWebShop.Models.ViewModels.Users
{
    public class ChangePasswordVM
    {
        public string Id { get; set; } = default!;
        [Display(Name = "Email")]
        public string? Email { get; set; } = default!;
        [Display(Name = "Enter the current password")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; } = default!;
        [Display(Name = "Enter a new password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = default!;
        [Display(Name = "Confirm your new password")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword))]
        public string ConfirmPassword { get; set; } = default!;
    }
}
