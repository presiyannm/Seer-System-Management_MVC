﻿using Microsoft.AspNetCore.Authorization;
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

        public ClientController(IEnquiryService enquiryService , ISeersService seersService, UserManager<ApplicationUser> userManager)
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

        //[HttpPost]
        //public async Task ChangeMyEnquiryStatus(int enquiryId)
        //{

        //}
    }
}