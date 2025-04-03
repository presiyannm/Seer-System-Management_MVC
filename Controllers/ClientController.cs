using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Система_за_управление_на_гадатели_MVC.Interfaces;
using Система_за_управление_на_гадатели_MVC.Models.Identity;
using Система_за_управление_на_гадатели_MVC.Models.ViewModels;

namespace Система_за_управление_на_гадатели_MVC.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private readonly IEnquiryService enquiryService;
        private readonly ISeersService seersService;
        private readonly UserManager<ApplicationUser> userManager;

        public ClientController(IEnquiryService enquiryService, ISeersService seersService, UserManager<ApplicationUser> userManager)
        {
            this.enquiryService = enquiryService;
            this.seersService = seersService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> CreateEnquiry(string userId)
        {
            var createEnquiryViewModel = new CreateEnquiryViewModel
            {
                ClientId = userId,
                EnquiryTypes = await enquiryService.GetAllEnquiryTypesAsync(),
                Seers = await seersService.GetAllSeersAsync()
            };

            return View(createEnquiryViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEnquiry(CreateEnquiryViewModel model)
        {
            if (ModelState.IsValid)
            {
                await enquiryService.CreateEnquiryAsync(model);
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ShowMyEnquries(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("User cannot be null");
            }

            var enquries = await enquiryService.GetAllEnquriesByIdAsync(user);

            return View(enquries);
        }

        [HttpGet]
        public async Task<IActionResult> ChangeEnquiryById(int enquiryId, string userId)
        {
            var enquiryToBeChanged = await enquiryService.GetEnquiryByIdAsync(enquiryId, userId);

            var enquiryViewModel = new ChangeEnquiryByIdViewModel()
            {
                EnquiryId = enquiryId,
                ClientName = enquiryToBeChanged.ClientName,
                Description = enquiryToBeChanged.Description,
                ClientBirthDate = enquiryToBeChanged.ApplicationUserBirthday,
                EnquiryTypeId = enquiryToBeChanged.EnquiryTypeId,
                SeerId = enquiryToBeChanged.SeerId,
                EnquiryTypes = await enquiryService.GetAllEnquiryTypesAsync()
            };

            return View(enquiryViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeEnquiryById(ChangeEnquiryByIdViewModel model)
        {
            if (ModelState.IsValid)
            {
                await enquiryService.ChangeEnquiryInformation(model);
                return RedirectToAction("ShowMyEnquries", new { userId = model.ClientId });
            }

            return View(model);
        }

        public async Task<IActionResult> CancelEnquiryById(int enquiryId, string userId)
        {
            var enquiryToBeChanged = await enquiryService.GetEnquiryByIdAsync(enquiryId, userId);

            await enquiryService.CancelEnquiry(enquiryToBeChanged);

            return RedirectToAction("ShowMyEnquries", new { userId = userId });
        }

        [HttpGet]
        public IActionResult RateSeerBySeerId(int seerId)
        {
            ViewBag.SeerId = seerId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RateSeerBySeerId(AddSeerRatingViewModel model)
        {
            if (ModelState.IsValid)
            {
                await seersService.AddSeerRatingAsync(model.SeerId, model.Rating);
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SeeAllSeers()
        {
            var seers = await seersService.GetAllSeersAsync();
            return View(seers);
        }
    }
}
