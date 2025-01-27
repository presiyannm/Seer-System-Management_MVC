using System.ComponentModel.DataAnnotations;

namespace Система_за_управление_на_гадатели_MVC.Models.ViewModels
{
    public class AccountResetEmailViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Полето е задължително")]
        public string OldEmail { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Полето е задължително")]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Полето е задължително")]
        public string NewEmail { get; set; } = string.Empty;
    }
}
