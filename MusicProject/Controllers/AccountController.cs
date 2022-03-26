using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicProject.Areas.Identity.Data;
using MusicProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicProject.Controllers
{
    public class AccountController : Controller
    {
        UserManager<ApplicationUser> UserManager;
        public AccountController(UserManager<ApplicationUser> _UserManager)
        {
            UserManager = _UserManager;
        }

        public IActionResult SignIn()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
        public IActionResult SignupConfirm(SignUpViewModel model)
        {

        }
    }
}
