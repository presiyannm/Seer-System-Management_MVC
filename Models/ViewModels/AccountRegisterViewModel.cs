using System.ComponentModel.DataAnnotations;

namespace Система_за_управление_на_гадатели_MVC.Models.ViewModels
{
    public class AccountRegisterViewModel
    {
        [Required]
        public string UserName { get; set; } =  string.Empty;

        [Required]
        public string Password { get; set; } =  string.Empty;

        [Required]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "Моля, качете снимка на профила.")]
        [DataType(DataType.Upload)]
        public IFormFile AccountPhotoFile { get; set; } = null!;

        public string AccountPhoto { get; set; } = string.Empty;
    }
}
