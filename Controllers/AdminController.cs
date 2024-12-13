using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Система_за_управление_на_гадатели_MVC.Interfaces;
using Система_за_управление_на_гадатели_MVC.Models.Identity;
using Система_за_управление_на_гадатели_MVC.Models.ViewModels;

namespace Система_за_управление_на_гадатели_MVC.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

    }
}
