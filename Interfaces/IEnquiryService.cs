using Система_за_управление_на_гадатели_MVC.Models;
using Система_за_управление_на_гадатели_MVC.Models.Identity;
using Система_за_управление_на_гадатели_MVC.Models.ViewModels;

namespace Система_за_управление_на_гадатели_MVC.Interfaces
{
    public interface IEnquiryService
    {
        public Task<ICollection<EnquiryStatus>> GetAllEnquiryStatusesAsync();

        public Task<ICollection<EnquiryType>> GetAllEnquiryTypesAsync();

        public Task CreateEnquiryAsync(CreateEnquiryViewModel model);

        public Task<ICollection<Enquiry>> GetAllEnquriesByIdAsync(ApplicationUser user);
    }
}
