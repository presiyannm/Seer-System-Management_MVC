using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Система_за_управление_на_гадатели_MVC.Models.ViewModels
{
    public class AccountResetPasswordViewModel
    {
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        public string NewPasswordRepeat { get; set; }
    }
}
