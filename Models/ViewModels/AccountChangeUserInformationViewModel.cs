using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Система_за_управление_на_гадатели_MVC.Models.ViewModels
{
    public class AccountChangeUserInformationViewModel
    {
        [Required(ErrorMessage = "Полето е задължително")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Полето е задължително")]
        public string LastName { get; set; } = string.Empty;

        // Property for uploading the profile photo
        public IFormFile? ProfilePhoto { get; set; }
    }
}