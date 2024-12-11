using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Система_за_управление_на_гадатели_MVC.Models.Identity;

namespace Система_за_управление_на_гадатели_MVC.Models
{
    public class Enquiry
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ClientName { get; set; } = string.Empty;

        [Required]
        public int EnquiryTypeId { get; set; }

        [ForeignKey(nameof(EnquiryTypeId))]
        public EnquiryType EnquiryType { get; set; } = null!;

        [Required]
        public int EnquiryStatusId { get ; set; }

        [ForeignKey(nameof(EnquiryStatusId))]
        public EnquiryStatus EnquiryStatus { get; set; }

        public string ApplicationUserId { get; set; } = string.Empty;

        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; } = null!;

        [Required]
        public int SeerId { get; set; }

        [ForeignKey(nameof(SeerId))]
        public Seer Seer { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime ApplicationUserBirthday {  get; set; } 

        public DateTime? EnquirySentToCheck { get; set; }

        public DateTime? EnquiryCheckInProgress { get; set; }

        public DateTime? EnquiryCheckFinished { get; set; }




    }  
}
