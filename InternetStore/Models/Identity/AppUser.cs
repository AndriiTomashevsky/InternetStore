using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace InternetStore.Models.Identity
{
    public class AppUser : IdentityUser
    {
        [Remote("Validate", "Admin")]
        public string NewEmail { get; set; }

        [UIHint("Password")]
        [Remote("Validate", "Admin")]
        public string Password { get; set; }

        public string RoleName { get; set; }
    }
}
