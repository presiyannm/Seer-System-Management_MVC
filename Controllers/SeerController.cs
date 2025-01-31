using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Система_за_управление_на_гадатели_MVC.Interfaces;
using Система_за_управление_на_гадатели_MVC.Models.Identity;

namespace Система_за_управление_на_гадатели_MVC.Controllers
{
    [Authorize(Roles = "Seer")]
    public class SeerController : Controller
    {
        private readonly ISeersService seersService;

        private readonly UserManager<ApplicationUser> userManager;

        public SeerController(ISeersService seersService, UserManager<ApplicationUser> userManager)
        {
            this.seersService = seersService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> MySeerEnquries(string userId)
        {
            var enquries = await seersService.GetAllSeerEnquriesAsync(userId);

            return View(enquries);
        }

        public async Task<IActionResult> UpdateEnquiryById(int enquiryId, string userId, string? answer)
        {
            var seerId = await seersService.UpdateEnquiryById(enquiryId, userId, answer);
            return RedirectToAction("MySeerEnquries", new { userId = userId });
        }
        
    }
}
