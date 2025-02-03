using System.ComponentModel.DataAnnotations;

namespace Система_за_управление_на_гадатели_MVC.Models.ViewModels
{
    public class AddSeerRatingViewModel
    {
        [Required(ErrorMessage = "Полето е задължително")]
        public int SeerId { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        [Range(0.0, 5.0, ErrorMessage = "Оценката трябва да е между 0.0 и 5.0")]
        public double Rating { get; set; }
    }
}
