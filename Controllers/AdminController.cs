using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Система_за_управление_на_гадатели_MVC.Interfaces;
using Система_за_управление_на_гадатели_MVC.Models;
using Система_за_управление_на_гадатели_MVC.Models.Identity;
using Система_за_управление_на_гадатели_MVC.Models.ViewModels;

namespace Система_за_управление_на_гадатели_MVC.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IEnquiryService enquiryService;

        private IAdminService adminService;

        private ISeersService seersService;

        public AdminController(IEnquiryService enquiryService, IAdminService adminService, ISeersService seersService)
        {
            this.enquiryService = enquiryService;
            this.adminService = adminService;
            this.seersService = seersService;
        }

        [HttpGet]
        public async Task<IActionResult> SeeAllEnquries()
        {
            var enquries = await enquiryService.GetAllEnquriesAsync();

            return View(enquries);
        }

        public async Task<IActionResult> DeleteEnquiryById(int enquiryId)
        {
            await adminService.DeleteEnquiryByIdAsync(enquiryId);

            return RedirectToAction("SeeAllEnquries");
        }

        [HttpGet]
        public async Task<IActionResult> ChangeEnquiryById(int enquiryId, string userId)
        {
            var enquiryToBeChanged = await enquiryService.GetEnquiryByIdAsync(enquiryId, userId);

            var enquiryViewModel = new ChangeEnquiryByIdViewModel()
            {
                EnquiryId = enquiryId,
                ClientId = userId,
                ClientName = enquiryToBeChanged.ClientName,
                Description = enquiryToBeChanged.Description,
                ClientBirthDate = enquiryToBeChanged.ApplicationUserBirthday,
                EnquiryTypeId = enquiryToBeChanged.EnquiryTypeId,
                EnquiryTypes = await enquiryService.GetAllEnquiryTypesAsync(),
                SeerId = enquiryToBeChanged.SeerId,
                Seers = (await seersService.GetAllSeersAsync())
                    .Select(s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = s.ApplicationUser.UserName
                    }).ToList(),
            };

            return View(enquiryViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeEnquiryById(ChangeEnquiryByIdViewModel model)
        {
            if (ModelState.IsValid)
            {
                await enquiryService.ChangeEnquiryInformation(model);

                return RedirectToAction("SeeAllEnquries");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SeeAllUsers()
        {
            var users = await adminService.GetApplicationUsersAsync();

            return View(users);
        }

        public async Task<IActionResult> MakeSeerById(string userId)
        {
            await adminService.MakeSeerById(userId);

            return RedirectToAction("SeeAllUsers");
        }

        public async Task<IActionResult> DeleteUserById(string userId)
        {
            var user = await adminService.GetUserById(userId);

            await adminService.RemoveUser(user);

            return RedirectToAction("SeeAllUsers");
        }
    }
}
