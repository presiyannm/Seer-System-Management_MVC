using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Система_за_управление_на_гадатели_MVC.Models.ViewModels
{
    public class AccountResetPasswordViewModel
    {
        [Required(ErrorMessage = "Полето е задължително")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        [DataType(DataType.Password)]
        public string NewPasswordRepeat { get; set; }
    }
}
