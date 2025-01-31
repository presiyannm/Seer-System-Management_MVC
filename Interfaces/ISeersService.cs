using Система_за_управление_на_гадатели_MVC.Models;

namespace Система_за_управление_на_гадатели_MVC.Interfaces
{
    public interface ISeersService
    {
        public Task<ICollection<Seer>> GetAllSeersAsync();

        public Task<ICollection<Enquiry>> GetAllSeerEnquriesAsync(string userId);

        Task<string> UpdateEnquiryById(int enquiryId, string userId, string? answer);
    }
}
