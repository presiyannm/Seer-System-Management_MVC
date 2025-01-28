using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Система_за_управление_на_гадатели_MVC.Models.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        [Display(Name = "Първо име")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Полето е задължително")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Полето е задължително")]
        [EmailAddress]
        [Display(Name = "Имейл")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Полето е задължително")]
        [Display(Name = "Потребителско име")]
        public string Username { get; set; } = string.Empty;

        public List<SelectListItem>? Roles { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        [Display(Name = "Избрана роля")]
        public int SelectedRoleIndex { get; set; }
    }
}