using System.ComponentModel.DataAnnotations;

namespace HMT.Models.HMTModel
{
    public class LoginView
    {
        [Key]
        [MaxLength(100)]
        [Required(ErrorMessage = ("Please enter Email"))]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Wrong Email Format")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter password")]
        [MinLength(5, ErrorMessage = "You need to set a password of at least 5 characters")]
        public string Password { get; set; }
    }
}
