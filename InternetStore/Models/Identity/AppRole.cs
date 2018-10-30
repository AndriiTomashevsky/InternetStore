using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace InternetStore.Models.Identity
{
    public class AppRole : IdentityRole
    {
        public AppRole()
        {
        }

        public AppRole(string roleName) : base(roleName)
        {
        }

        [Remote("Validate", "RoleAdmin")]
        [Required]
        public override string Name { get { return base.Name; } set { base.Name = value; } }
    }
}
