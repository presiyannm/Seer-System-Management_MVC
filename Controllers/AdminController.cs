using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

        private UserManager<ApplicationUser> userManager;

        public AdminController(IEnquiryService enquiryService, IAdminService adminService, ISeersService seersService, UserManager<ApplicationUser> userManager)
        {
            this.enquiryService = enquiryService;
            this.adminService = adminService;
            this.seersService = seersService;
            this.userManager = userManager;
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
        public async Task<IActionResult> SeeAllUsers(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            // Sorting logic
            ViewData["FirstNameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "first_desc" : "";
            ViewData["LastNameSortParam"] = sortOrder == "last_asc" ? "last_desc" : "last_asc";

            // Filtering logic
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var users = await adminService.GetApplicationUsersAsync();

            // Search logic
            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                         u.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Sorting logic
            switch (sortOrder)
            {
                case "first_desc":
                    users = users.OrderByDescending(u => u.FirstName).ToList();
                    break;
                case "last_asc":
                    users = users.OrderBy(u => u.LastName).ToList();
                    break;
                case "last_desc":
                    users = users.OrderByDescending(u => u.LastName).ToList();
                    break;
                default:
                    users = users.OrderBy(u => u.FirstName).ToList();
                    break;
            }

            // Pagination logic
            int pageSize = 2; // Number of items per page
            return View(PaginatedList<ApplicationUser>.Create(users.AsQueryable(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> DeleteUserById(string userId)
        {
            var currentUser = await userManager.GetUserAsync(User);

            if (userId == currentUser.Id)
            {
                TempData["ErrorMessage"] = "Не можеш да изтриеш собствения си акаунт";
                return RedirectToAction("SeeAllUsers");
            }

            var user = await adminService.GetUserById(userId);

            await adminService.RemoveUser(user);

            return RedirectToAction("SeeAllUsers");
        }

        [HttpGet]
        public async Task<IActionResult> EditUserById(string userId)
        {
            var user = await adminService.GetUserById(userId);

            var roles = await adminService.GetAllRolesAsync();

            var currentRoles = await userManager.GetRolesAsync(user);

            int selectedRoleIndex = 0;
            if (currentRoles.Any())
            {
                var currentRoleName = currentRoles.First();
                var currentRole = roles.FirstOrDefault(r => r.Name == currentRoleName);
                if (currentRole != null)
                {
                    selectedRoleIndex = roles.IndexOf(currentRole);
                }
            }

            var viewModel = new EditUserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.UserName,
                Roles = roles.Select((role, index) => new SelectListItem
                {
                    Text = role.Name,
                    Value = index.ToString()
                }).ToList(),
                SelectedRoleIndex = selectedRoleIndex
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserById(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                await adminService.UpdateUserAsync(model);

                return RedirectToAction("SeeAllUsers", "Admin");
            }

            return View(model);

        }

    }
}
