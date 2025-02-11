﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Система_за_управление_на_гадатели_MVC.Models.Identity;

namespace Система_за_управление_на_гадатели_MVC.Models
{
    public class Seer
    {
        [Key]
        public int Id { get; set; }

        public double Rating { get; set; }

        public List<double>? SumOfRating { get; set; }
        
        [Required]
        public string ApplicationUserId { get; set; }

        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; } = null!;

        public ICollection<Enquiry>? Enquiries { get; set; }


    }
}
