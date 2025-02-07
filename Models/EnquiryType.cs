using System.ComponentModel.DataAnnotations;

namespace Система_за_управление_на_гадатели_MVC.Models
{
    public class EnquiryType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
