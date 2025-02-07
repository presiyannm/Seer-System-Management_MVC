using System.ComponentModel.DataAnnotations;

namespace Система_за_управление_на_гадатели_MVC.Models.ViewModels
{
    public class AccountLoginViewModel
    {
        [Required(ErrorMessage = "Полето е задължително")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Полето е задължително")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
