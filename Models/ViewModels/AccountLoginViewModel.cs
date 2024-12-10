using System.ComponentModel.DataAnnotations;

namespace Система_за_управление_на_гадатели_MVC.Models.ViewModels
{
    public class AccountLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
