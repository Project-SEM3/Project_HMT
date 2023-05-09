using System.ComponentModel.DataAnnotations;

namespace HMT.Models.HMTModel
{
    public class ChangePassword
    {
        [Required(ErrorMessage = "Please enter your current password")]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        [MinLength(5, ErrorMessage = "You need to set a password of at least 5 characters")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Please enter a new password")]
        [MinLength(5, ErrorMessage = "You need to set a password of at least 5 characters")]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [MinLength(5, ErrorMessage = "You need to set a password of at least 5 characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Re-enter password")]
        [Compare("NewPassword", ErrorMessage = "Re-enter the wrong password")]
        public string ConfirmPassword { get; set; }
    }
}
