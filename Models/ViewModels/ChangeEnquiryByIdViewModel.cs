using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Система_за_управление_на_гадатели_MVC.Models.ViewModels
{
    public class ChangeEnquiryByIdViewModel
    {
        [Required(ErrorMessage = "Полето е задължително")]
        public int EnquiryId { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        public string ClientId { get; set; } = null!;

        [Required(ErrorMessage = "Полето е задължително")]
        public string ClientName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Полето е задължително")]
        [DataType(DataType.DateTime)]
        public DateTime ClientBirthDate { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Полето е задължително")]
        public int EnquiryTypeId { get; set; }

        public ICollection<EnquiryType>? EnquiryTypes { get; set; }

        public int? SeerId { get; set; }
        public IEnumerable<SelectListItem> Seers { get; set; } = new List<SelectListItem>();

    }
}
