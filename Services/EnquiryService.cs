using Microsoft.EntityFrameworkCore;
using Система_за_управление_на_гадатели_MVC.Data;
using Система_за_управление_на_гадатели_MVC.Interfaces;
using Система_за_управление_на_гадатели_MVC.Models;
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

        public async Task CreateEnquiryAsync(CreateEnquiryViewModel model)
        {
            var enquiryToCreate = new Enquiry()
            {
                ApplicationUserId = model.ClientId,
                Description = model.Description,
                ApplicationUserBirthday = model.ClientBirthDate,
                EnquiryStatusId = 1,
                EnquiryTypeId = model.EnquiryTypeId,
                SeerId = model.SeerId,
                EnquirySentToCheck = DateTime.Now
            };

            await context.Enquiries.AddAsync(enquiryToCreate);

            await context.SaveChangesAsync();
        }
    }
}

