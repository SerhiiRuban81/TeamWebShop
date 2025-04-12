using System.ComponentModel.DataAnnotations;

namespace TeamWebShop.Models.DTOs.Users
{
    public class UserDTO
    {
        public string Id { get; set; } = default!;
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = default!;
        [Display(Name = "Is private person")]
        public bool IsPrivatePerson { get; set; }
        [Display(Name = "Phone number")]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

    }
}
