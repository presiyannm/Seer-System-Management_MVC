using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Система_за_управление_на_гадатели_MVC.Data;
using Система_за_управление_на_гадатели_MVC.Interfaces;
using Система_за_управление_на_гадатели_MVC.Models;
using Система_за_управление_на_гадатели_MVC.Models.Identity;
using Система_за_управление_на_гадатели_MVC.Models.ViewModels;

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

        public async Task<ApplicationUser> GetUserById(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("User cannot be null");
            }

            return user;
        }

        public async Task RemoveUser(ApplicationUser user)
        {
            await userManager.DeleteAsync(user);
        }

        public async Task<List<IdentityRole>> GetAllRolesAsync()
        {
            var roles = await context.Roles.ToListAsync();

            return roles;
        }

        public async Task UpdateUserAsync(EditUserViewModel model)
        {
            var user = await GetUserById(model.Id);

            var roles = await GetAllRolesAsync();

            if (user == null)
            {
                throw new Exception("User cannot be null");
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.UserName = model.Username;

            var currentRoles = await userManager.GetRolesAsync(user);

            if (currentRoles.Any())
            {
                if(currentRoles.Contains("Seer"))
                {
                    var seerToRemove = await context.Seers.FirstOrDefaultAsync(s => s.ApplicationUserId == user.Id);

                    if (seerToRemove is null)
                    {
                        throw new Exception("Seer cannot be null");
                    }

                    context.Seers.Remove(seerToRemove);
                }
                
                await userManager.RemoveFromRolesAsync(user, currentRoles);
            }

            await userManager.AddToRoleAsync(user, $"{roles[model.SelectedRoleIndex]}");

            if (roles[model.SelectedRoleIndex].Name == "Seer")
            {
                var seer = new Seer()
                {
                    ApplicationUserId = user.Id,
                    ApplicationUser = user
                };

                await context.Seers.AddAsync(seer);

            }

            await context.SaveChangesAsync();

        }
    }
}
