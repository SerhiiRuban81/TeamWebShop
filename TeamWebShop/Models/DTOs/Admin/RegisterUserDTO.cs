using System.ComponentModel.DataAnnotations;

namespace TeamWebShop.Models.DTOs.Admin
{
    public class RegisterUserDTO
    {
        //public int ID { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = default!;

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;

        [Required]
        [Display(Name = "Password confirmation")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = default!;

        // Приватна особа (true) чи організація (false)
        // Is private person or organization
        [Display(Name = "Is private person")]
        public bool IsPrivatePerson { get; set; } = true;


        // Номер телефону
        // Phone number
        [Display(Name = "Phone number")]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

        // Лишатися в системі (true), чи виходити (false)
        // Remain in system or exit
        [Display(Name = "Remain in system")]
        public bool IsPersistent { get; set; } = false;


    }
}
