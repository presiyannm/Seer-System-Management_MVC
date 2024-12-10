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
                var user = new ApplicationUser
                {
                    UserName = model.EmailAddress,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.EmailAddress,
                };

                if (model.Password == model.ConfirmPassword)
                {
                    await _userManager.CreateAsync(user, model.Password);
                }
                else
                {
                    ModelState.AddModelError("password", "Passwords do not match");

                    return View(model);
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
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    throw new Exception("User email is null");
                }

                var passwordMatch = await _userManager.CheckPasswordAsync(user, model.Password);

                if (passwordMatch)
                {
                    await _signInManager.PasswordSignInAsync(user, model.Password, false, true);
                }

                return RedirectToAction("Index", "Home");
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
