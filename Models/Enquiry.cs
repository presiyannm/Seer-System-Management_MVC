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

        public string ClientName { get; set; } = string.Empty;

        public int EnquiryTypeId { get; set; }

        [ForeignKey(nameof(EnquiryTypeId))]
        public EnquiryType EnquiryType { get; set; } = null!;

        public int EnquiryStatusId { get ; set; }

        [ForeignKey(nameof(EnquiryStatusId))]
        public EnquiryStatus EnquiryStatus { get; set; }

        public string ApplicationUserId { get; set; } = string.Empty;

        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; } = null!;

        public int SeerId { get; set; }

        [ForeignKey(nameof(SeerId))]
        public Seer Seer { get; set; }

        public string Description { get; set; }

        public string WantedResult { get; set; }

        public string? Answer { get; set; }

        public DateTime ApplicationUserBirthday {  get; set; } 

        public DateTime? EnquirySentToCheck { get; set; }

        public DateTime? EnquiryCheckInProgress { get; set; }

        public DateTime? EnquiryCheckFinished { get; set; }




    }  
}
