﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Система_за_управление_на_гадатели_MVC.Data;
using Система_за_управление_на_гадатели_MVC.Models.Identity;
using Система_за_управление_на_гадатели_MVC.Models.ViewModels;

namespace Система_за_управление_на_гадатели_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext _context;

        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.EmailAddress,
                };

                if (model.Password == model.ConfirmPassword)
                {
                    await _userManager.CreateAsync(user, model.Password);

                    await _userManager.AddToRoleAsync(user, "Client");

                }

                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Login(AccountLoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Невалидно потребителско име или парола.");
                    return View(model);
                }

                var passwordMatch = await _userManager.CheckPasswordAsync(user, model.Password);

                if (!passwordMatch)
                {
                    ModelState.AddModelError(string.Empty, "Паролата е невалидна.");
                    return View(model);
                }

                await _signInManager.PasswordSignInAsync(user, model.Password, false, true);
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login");
            }

            var dashboardViewModel = new DashboardViewModel
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = await _userManager.GetRolesAsync(user)
            };

            return View(dashboardViewModel);

        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ResetPassword()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login");
            }

            return View();

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(AccountResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                if (user == null)
                {
                    return RedirectToAction("Login");
                }

                var passwordCheck = await _userManager.CheckPasswordAsync(user, model.OldPassword);

                if (!passwordCheck)
                {
                    ModelState.AddModelError(string.Empty, "Старата парола е грешна.");

                    return View(model);
                }

                if (model.NewPassword != model.NewPasswordRepeat)
                {
                    ModelState.AddModelError(string.Empty, "Новите пароли не съвпадат.");

                    return View(model);
                }

                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                if (result.Succeeded)
                {
                    await _signInManager.SignOutAsync();

                    TempData["SuccessMessage"] = "Паролата беше сменена успешно.";
                    return RedirectToAction("Login");
                }

                return View(model);
            }

            return View(model);

        }

        [Authorize]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

    }
}
