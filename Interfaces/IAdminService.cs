using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Система_за_управление_на_гадатели_MVC.Models;
using Система_за_управление_на_гадатели_MVC.Models.Identity;
using Система_за_управление_на_гадатели_MVC.Models.ViewModels;

namespace Система_за_управление_на_гадатели_MVC.Interfaces
{
    public interface IAdminService
    {
        public Task DeleteEnquiryByIdAsync(int enquiryId);

        public Task<ICollection<ApplicationUser>> GetApplicationUsersAsync();

        public Task MakeSeerById(string userId);

        public Task<ApplicationUser> GetUserById(string userId);

        public Task RemoveUser(ApplicationUser user);

        public Task<List<IdentityRole>> GetAllRolesAsync();

        public Task UpdateUserAsync(EditUserViewModel model);
    }
}
