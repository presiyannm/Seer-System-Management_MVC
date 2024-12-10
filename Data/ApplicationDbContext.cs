using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Система_за_управление_на_гадатели_MVC.Models;
using Система_за_управление_на_гадатели_MVC.Models.Identity;

namespace Система_за_управление_на_гадатели_MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EnquiryType>().HasData(
                new EnquiryType { Id = 1, Name = "хороскоп" },
                new EnquiryType { Id = 2, Name = "китайски хороскоп" },
                new EnquiryType { Id = 3, Name = "лунен хороскоп" },
                new EnquiryType { Id = 4, Name = "таро" },
                new EnquiryType { Id = 5, Name = "руни" },
                new EnquiryType { Id = 6, Name = "тао оракул" },
                new EnquiryType { Id = 7, Name = "гласът на провидението" },
                new EnquiryType { Id = 8, Name = "друга" }
                );

            modelBuilder.Entity<EnquiryStatus>().HasData(
                new EnquiryStatus { Id = 1, Status = "чакащ" },
                new EnquiryStatus { Id = 2, Status = "за преглед"},
                new EnquiryStatus { Id = 3, Status = "в процес"},
                new EnquiryStatus { Id = 4, Status = "изпълнен"},
                new EnquiryStatus { Id = 5, Status = "отказан"}
                );
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<EnquiryType> EnquiryTypes { get; set; }

        public DbSet<EnquiryStatus> EnquiryStatuses { get; set; }

        public DbSet<Enquiry> Enquiries { get; set; }

        public DbSet<Seer> Seers { get; set; }
    }
}
