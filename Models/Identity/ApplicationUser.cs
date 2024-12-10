using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Система_за_управление_на_гадатели_MVC.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName {  get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

    }
}
