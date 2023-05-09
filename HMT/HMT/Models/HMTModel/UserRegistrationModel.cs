using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMT.Models.HMTModel
{
    public class UserRegistrationModel
    {

        [Required(ErrorMessage = "Please enter Full Name")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Please enter Address")]
        public string? Address { get; set; }

        [MaxLength(11)]
        [Required(ErrorMessage = "Please enter your phone number")]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "You need to set a password of at least 5 characters")]
        public string? Password { get; set; }

        [MinLength(5, ErrorMessage = "You need to set a password of at least 5 characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Re-enter password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

        [NotMapped]
        [BindProperty]
        public IFormFile? FrontImage { get; set; }

        [Required]
        public string? Rolle { get; set; }
    }
}
