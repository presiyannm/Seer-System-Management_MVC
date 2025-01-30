using Microsoft.EntityFrameworkCore;
using Система_за_управление_на_гадатели_MVC.Data;
using Система_за_управление_на_гадатели_MVC.Extensions;
using Система_за_управление_на_гадатели_MVC.Interfaces;
using Система_за_управление_на_гадатели_MVC.Models;
using Система_за_управление_на_гадатели_MVC.Models.Identity;
using Система_за_управление_на_гадатели_MVC.Models.ViewModels;

namespace Система_за_управление_на_гадатели_MVC.Services
{
    public class EnquiryService : IEnquiryService
    {
        private readonly ApplicationDbContext context;

        public EnquiryService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<ICollection<EnquiryStatus>> GetAllEnquiryStatusesAsync()
        {
            var enquiryStatuses = await context.EnquiryStatuses.ToListAsync();

            return enquiryStatuses;
        }

        public async Task<ICollection<EnquiryType>> GetAllEnquiryTypesAsync()
        {
            var enquiryTypes = await context.EnquiryTypes.ToListAsync();

            return enquiryTypes;
        }

        public async Task<ICollection<Enquiry>> GetAllEnquriesByIdAsync(ApplicationUser user)
        {
            var enquries = await context.Enquiries
                .Include(e => e.Seer.ApplicationUser).Include(e => e.Seer)
                .Include(e => e.EnquiryStatus).Include(e => e.EnquiryType)
                .Include(e => e.ApplicationUser)
                .Where(e => e.ApplicationUserId == user.Id).ToListAsync();

            return enquries;
        }

        public async Task CreateEnquiryAsync(CreateEnquiryViewModel model)
        {
            var enquiryToCreate = new Enquiry()
            {
                ApplicationUserId = model.ClientId,
                Description = model.Description,
                WantedResult = model.WantedResult,
                ApplicationUserBirthday = model.ClientBirthDate,
                EnquiryStatusId = 1,
                EnquiryTypeId = model.EnquiryTypeId,
                SeerId = model.SeerId,
                ClientName = model.ClientName,
                EnquirySentToCheck = DateTime.Now
            };

            await context.Enquiries.AddAsync(enquiryToCreate);

            await context.SaveChangesAsync();
        }

        public async Task<Enquiry> GetEnquiryByIdAsync(int enquiryId, string userId)
        {
            var enquiry = await context.Enquiries
                .Include(x => x.ApplicationUser)
                .Where(x => x.Id ==  enquiryId && x.ApplicationUserId == userId)
                .FirstOrDefaultAsync();

            if (enquiry == null)
            {
                throw new Exception("Enquiry doesn't exist");
            }

            return enquiry;
        }

        public async Task<Enquiry> ChangeEnquiryInformation(ChangeEnquiryByIdViewModel model)
        {
            var enquiryToChange = await context.Enquiries
                .Include(x => x.ApplicationUser)
                .FirstOrDefaultAsync(x => x.Id == model.EnquiryId);

            if (enquiryToChange == null)
            {
                throw new Exception("Enquiry doesn't exist");
            }

            enquiryToChange.ClientName = model.ClientName;
            enquiryToChange.Description = model.Description;
            enquiryToChange.ApplicationUserBirthday = model.ClientBirthDate;
            enquiryToChange.EnquiryTypeId = model.EnquiryTypeId;
            
            if (model.SeerId != null)
            {
                enquiryToChange.SeerId = (int)model.SeerId;
            }

            await context.SaveChangesAsync();

            return enquiryToChange;
        }

        public async Task<Enquiry> CancelEnquiry(Enquiry enquiry)
        {
            enquiry.EnquiryStatusId = 5;

            await context.SaveChangesAsync();

            return enquiry;
        }

        public async Task<ICollection<Enquiry>> GetAllEnquriesAsync()
        {
            var enquries = await context.Enquiries
                .Include(x => x.ApplicationUser)
                .Include(x => x.Seer)
                .Include(x => x.Seer.ApplicationUser)
                .Include(x => x.EnquiryType)
                .Include(x => x.EnquiryStatus)
                .ToListAsync();

            return enquries;
        }
    }
}

