using System.ComponentModel.DataAnnotations;

namespace Система_за_управление_на_гадатели_MVC.Models.ViewModels
{
    public class AccountRegisterViewModel
    {
        [Required(ErrorMessage = "Полето е задължително")]
        public string UserName { get; set; } =  string.Empty;

        [Required(ErrorMessage = "Полето е задължително.")]
        [StringLength(128, MinimumLength = 8, ErrorMessage = "Паролата трябва да бъде между 8 и 128 символа.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+{}\[\]:;<>,.?/~\\-]).+$",
        ErrorMessage = "Паролата трябва да съдържа поне една главна буква, една малка буква, една цифра и един специален символ.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Полето е задължително.")]
        [StringLength(128, MinimumLength = 8, ErrorMessage = "Паролата трябва да бъде между 8 и 128 символа.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+{}\[\]:;<>,.?/~\\-]).+$",
        ErrorMessage = "Паролата трябва да съдържа поне една главна буква, една малка буква, една цифра и един специален символ.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Полето е задължително")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Полето е задължително")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Полето е задължително")]
        [EmailAddress(ErrorMessage = "Имейлът трябва да е валиден")]
        public string EmailAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "Моля, качете снимка на профила.")]
        [DataType(DataType.Upload)]
        public IFormFile AccountPhotoFile { get; set; } = null!;

        public string AccountPhoto { get; set; } = string.Empty;
    }
}
