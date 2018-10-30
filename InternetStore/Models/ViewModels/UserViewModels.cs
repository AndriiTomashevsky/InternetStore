using InternetStore.Models.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace InternetStore.Models.ViewModels
{
    public class CreateModel
    {
        [Required]
        [Remote("Validate", "Admin")]
        public string Name { get; set; }

        [Required]
        [Remote("Validate", "Admin")]
        public string Email { get; set; }

        [UIHint("Password")]
        [Required]
        [Remote("Validate", "Admin")]
        public string Password { get; set; }

        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [UIHint("email")]
        public string Email { get; set; }

        [Required]
        [UIHint("password")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
