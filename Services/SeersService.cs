using Microsoft.EntityFrameworkCore;
using Система_за_управление_на_гадатели_MVC.Data;
using Система_за_управление_на_гадатели_MVC.Interfaces;
using Система_за_управление_на_гадатели_MVC.Models;

namespace Система_за_управление_на_гадатели_MVC.Services
{
    public class SeersService : ISeersService
    {
        private ApplicationDbContext context;

        private IEnquiryService enquiryService;

        public SeersService(ApplicationDbContext context, IEnquiryService enquiryService)
        {
            this.context = context;
            this.enquiryService = enquiryService;
        }

        public async Task<ICollection<Seer>> GetAllSeersAsync()
        {
            var seers = await context.Seers.Include(u => u.ApplicationUser).ToListAsync();

            return seers;
        }

        public async Task<Seer> GetSeerByIdAsync(string userId)
        {
            var seer = await context.Seers.FirstOrDefaultAsync(x => x.ApplicationUserId == userId);

            if (seer == null)
            {
                throw new Exception("Seer is null");
            }

            return seer;
        }
        
        public async Task<ICollection<Enquiry>> GetAllSeerEnquriesAsync(string userId)
        {
            var seer = await GetSeerByIdAsync(userId);

            var enquries = await context.Enquiries
                .Include(x => x.Seer)
                .Include(x => x.ApplicationUser)
                .Include(x => x.EnquiryStatus)
                .Include(x => x.EnquiryType)
                .Where(x => x.SeerId == seer.Id).ToListAsync();

            return enquries;
        }

        public async Task<string> UpdateEnquiryById(int enquiryId, string userId, string? answer)
        {
            var enquiryToUpdate = await enquiryService.GetEnquiryByIdAsync(enquiryId, userId);

            var currentSeer = await context.Seers
                .FirstOrDefaultAsync(x => x.Id == enquiryToUpdate.SeerId);

            if (currentSeer == null)
            {
                throw new Exception("Seer is null");
            }

            switch (enquiryToUpdate.EnquiryStatusId)
            {
                case 1:
                    enquiryToUpdate.EnquiryStatusId = 2;
                    break;

                case 2:
                    enquiryToUpdate.EnquiryStatusId = 3;
                    enquiryToUpdate.EnquiryCheckInProgress = DateTime.Now;
                    break;

                case 3:
                    enquiryToUpdate.EnquiryStatusId = 4;
                    enquiryToUpdate.EnquiryCheckFinished = DateTime.Now;
                    enquiryToUpdate.Answer = answer;
                    break;
            }

            await context.SaveChangesAsync();

            return currentSeer.ApplicationUserId;
        }
    }
}

