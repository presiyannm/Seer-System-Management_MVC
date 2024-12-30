using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Система_за_управление_на_гадатели_MVC.Data;
using Система_за_управление_на_гадатели_MVC.Interfaces;
using Система_за_управление_на_гадатели_MVC.Models;
using Система_за_управление_на_гадатели_MVC.Models.Identity;

namespace Система_за_управление_на_гадатели_MVC.Services
{
    public class AdminService : IAdminService
    {
        private ApplicationDbContext context;

        private UserManager<ApplicationUser> userManager;

        public AdminService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task DeleteEnquiryByIdAsync(int enquiryId)
        {
            var enquiry = await context.Enquiries.FirstOrDefaultAsync(x => x.Id == enquiryId);

            if (enquiry == null)
            {
                throw new Exception("Enquiry cannot be null");
            }

            context.Enquiries.Remove(enquiry);

            await context.SaveChangesAsync();
        }

        public async Task<ICollection<ApplicationUser>> GetApplicationUsersAsync()
        {
            var users = await context.ApplicationUsers.Include(x => x.Seer).ToListAsync();

            return users;
        }

        public async Task MakeSeerById(string userId)
        {
            var user = await context.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                throw new Exception("User cannot be null");
            }

            var seerToAdd = new Seer()
            {
                ApplicationUserId = userId,
                ApplicationUser = user
            };

            await context.Seers.AddAsync(seerToAdd);

            await userManager.AddToRoleAsync(user, "Seer");

            await context.SaveChangesAsync();

        }
    }
}
