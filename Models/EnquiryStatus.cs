using System.ComponentModel.DataAnnotations;

namespace Система_за_управление_на_гадатели_MVC.Models
{
    public class EnquiryStatus
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Status {  get; set; }
    }
}
