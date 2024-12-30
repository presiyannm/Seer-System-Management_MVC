using Microsoft.AspNetCore.Mvc;
using Система_за_управление_на_гадатели_MVC.Models;
using Система_за_управление_на_гадатели_MVC.Models.Identity;

namespace Система_за_управление_на_гадатели_MVC.Interfaces
{
    public interface IAdminService
    {
        public Task DeleteEnquiryByIdAsync(int enquiryId);

        public Task<ICollection<ApplicationUser>> GetApplicationUsersAsync();

        public Task MakeSeerById(string userId);
    }
}
