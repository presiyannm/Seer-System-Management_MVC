using System.ComponentModel.DataAnnotations;

namespace Система_за_управление_на_гадатели_MVC.Models.ViewModels
{
    public class CreateEnquiryViewModel
    {
        [Required]
        public string ClientId { get; set; } = null!;

        [Required(ErrorMessage = "Полето е задължително")]
        [MaxLength(50)]
        public string ClientName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Полето е задължително")]
        [DataType(DataType.DateTime)]
        public DateTime ClientBirthDate { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Полето е задължително")]
        [MaxLength(500)]
        public string WantedResult { get; set; } = string.Empty;

        [Required(ErrorMessage = "Полето е задължително")]
        public int EnquiryTypeId { get; set; }
        public ICollection<EnquiryType>? EnquiryTypes { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        public int SeerId { get; set; }
        public ICollection<Seer>? Seers { get; set; }
    }

}
