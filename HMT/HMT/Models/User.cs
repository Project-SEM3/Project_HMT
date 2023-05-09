using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using System.Transactions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HMT.Models
{
    public class User : IdentityUser
    {

        [Required, StringLength(100)]
        public string FullName { get; set; }

        public string? Address { get; set; }

        [BindProperty]
        public string? Image { get; set; }

        [NotMapped]
        [BindProperty]
        public IFormFile? FrontImage { get; set; }


        //public ICollection<Requests>? Requests { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Request>? Requests { get; set; }

        [InverseProperty("UserManager")]
        public virtual ICollection<Request>? RequestsFromManager { get; set; }
    }
}
