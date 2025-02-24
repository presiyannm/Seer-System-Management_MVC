using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Система_за_управление_на_гадатели_MVC.Interfaces;
using Система_за_управление_на_гадатели_MVC.Models;
using Система_за_управление_на_гадатели_MVC.Models.Identity;

namespace Система_за_управление_на_гадатели_MVC.Controllers
{
    [Authorize(Roles = "Seer")]
    public class SeerController : Controller
    {
        private readonly ISeersService seersService;

        private readonly IEnquiryService enquiryService;

        private readonly UserManager<ApplicationUser> userManager;

        public SeerController(ISeersService seersService, UserManager<ApplicationUser> userManager, IEnquiryService enquiryService)
        {
            this.seersService = seersService;
            this.userManager = userManager;
            this.enquiryService = enquiryService;
        }

        [HttpGet]
        public async Task<IActionResult> MySeerEnquries(
        string sortOrder,
        string currentFilter,
        string searchString,
        string? statusFilter,
        int? pageNumber,
        bool showUnfinishedOnly = false)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var statuses = await enquiryService.GetAllEnquiryStatusesAsync();
            ViewBag.Statuses = statuses;

            ViewData["ClientNameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "client_desc" : "";
            ViewData["StatusSortParam"] = sortOrder == "status_asc" ? "status_desc" : "status_asc";
            ViewData["DateSortParam"] = sortOrder == "date_asc" ? "date_desc" : "date_asc";
            ViewData["StatusFilter"] = statusFilter;

            if (!string.IsNullOrEmpty(searchString))
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            ViewData["StatusFilter"] = statusFilter;
            ViewData["ShowUnfinishedOnly"] = showUnfinishedOnly;

            try
            {
                var enquiries = await seersService.GetAllSeerEnquriesAsync(userId);

                // Apply search filter
                if (!string.IsNullOrEmpty(searchString))
                {
                    enquiries = enquiries.Where(e =>
                        e.ClientName.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                        e.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                        e.WantedResult.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                // Apply unfinished-only filter
                if (showUnfinishedOnly)
                {
                    enquiries = enquiries.Where(e => e.Answer == null || e.EnquiryStatus.Status != "изпълнен").ToList();
                }

                // Apply status filter
                if (!string.IsNullOrEmpty(statusFilter))
                {
                    enquiries = enquiries.Where(e => e.EnquiryStatus.Status == statusFilter).ToList();
                }

                // Apply sorting
                switch (sortOrder)
                {
                    case "client_desc":
                        enquiries = enquiries.OrderByDescending(e => e.ClientName).ToList();
                        break;
                    case "status_asc":
                        enquiries = enquiries.OrderBy(e => e.EnquiryStatus.Status).ToList();
                        break;
                    case "status_desc":
                        enquiries = enquiries.OrderByDescending(e => e.EnquiryStatus.Status).ToList();
                        break;
                    case "date_asc":
                        enquiries = enquiries.OrderBy(e => e.EnquirySentToCheck).ToList();
                        break;
                    case "date_desc":
                        enquiries = enquiries.OrderByDescending(e => e.EnquirySentToCheck).ToList();
                        break;
                    default:
                        enquiries = enquiries.OrderBy(e => e.ClientName).ToList();
                        break;
                }

                int pageSize = 5;
                return View(PaginatedList<Enquiry>.Create(enquiries.AsQueryable(), pageNumber ?? 1, pageSize));
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }

        public async Task<IActionResult> UpdateEnquiryById(int enquiryId, string userId, string? answer)
        {
            var seerId = await seersService.UpdateEnquiryById(enquiryId, userId, answer);
            return RedirectToAction("MySeerEnquries", new { userId = userId });
        }
        
    }
}
