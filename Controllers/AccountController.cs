using Microsoft.AspNetCore.Authorization;
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
                // Handle the file upload
                if (model.AccountPhotoFile != null && model.AccountPhotoFile.Length > 0)
                {
                    // Ensure the "images/AccountPhotos" directory exists
                    var uploadsFolder = Path.Combine("wwwroot", "images", "AccountPhotos");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Generate a unique file name
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.AccountPhotoFile.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Save the file to the server
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.AccountPhotoFile.CopyToAsync(fileStream);
                    }

                    // Set the AccountPhoto property to the file path
                    model.AccountPhoto = uniqueFileName;
                }

                // Create the ApplicationUser
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.EmailAddress,
                    AccountPhoto = model.AccountPhoto // Set the photo path
                };

                // Create the user and add to the "Client" role
                if (model.Password == model.ConfirmPassword)
                {
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "Client");
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Паролите не съвпадат.");
                }
            }

            // If we got this far, something failed; redisplay the form
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
            if (ModelState.IsValid)
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
                PhotoFileName = user.AccountPhoto,
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
        [HttpGet]
        public IActionResult ResetEmail()
        {
            return View();
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]

        public async Task<IActionResult> ResetEmail(AccountResetEmailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.OldEmail);

            if (user is null)
            {
                ModelState.AddModelError(nameof(model.OldEmail), "Потребител с този емайл не е намерен.");

                return View(model);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (result.Succeeded)
            {
                var token = await _userManager.GenerateChangeEmailTokenAsync(user, model.NewEmail);

                await _userManager.ChangeEmailAsync(user, model.NewEmail, token);

                await _userManager.UpdateAsync(user);

                await _signInManager.SignOutAsync();

                TempData["EmailChanged"] = "Your email has been successfully updated!";

                return RedirectToAction("Login", "Account");
            }

            else if(!result.Succeeded)
            {
                ModelState.AddModelError(nameof(model.Password), "Паролата не съвпада.");
            }

            return View(model);

        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangeUserInformation()
        {
            return View();
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> ChangeUserInformation(AccountChangeUserInformationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
            {
                throw new Exception("User cannot be null");
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            await _userManager.UpdateAsync(user);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

    }
}
