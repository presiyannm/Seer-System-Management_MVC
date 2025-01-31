using System.ComponentModel.DataAnnotations;

namespace Система_за_управление_на_гадатели_MVC.Models.ViewModels
{
    public class AddSeerRatingViewModel
    {
        [Required(ErrorMessage = "Полето е задължително")]
        public int SeerId { get; set; }

        [Required(ErrorMessage = "Полето е задължително")]
        public double Rating { get; set; }
    }
}
