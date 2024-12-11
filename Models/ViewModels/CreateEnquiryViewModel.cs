using System.ComponentModel.DataAnnotations;

namespace Система_за_управление_на_гадатели_MVC.Models.ViewModels
{
    public class CreateEnquiryViewModel
    {
        [Required]
        public string ClientId { get; set; }

        [Required]
        public string ClientName { get; set; } = string.Empty;
  
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ClientBirthDate { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int EnquiryTypeId { get; set; }
        public ICollection<EnquiryType>? EnquiryTypes { get; set; }

        [Required]
        public int SeerId { get; set; }

        public ICollection<Seer>? Seers { get; set; }

    }
}
