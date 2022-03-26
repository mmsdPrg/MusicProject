using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicProject.Areas.Identity.Data;
using MusicProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MusicProject.Controllers
{
    public class AccountController : Controller
    {
        UserManager<ApplicationUser> UserManager;
        SignInManager<ApplicationUser> SignInManager;
        public AccountController(UserManager<ApplicationUser> _UserManager, SignInManager<ApplicationUser> _SignInManager)
        {
            UserManager = _UserManager;
            SignInManager = _SignInManager;
        }

        public IActionResult SignIn()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
        public async Task<IActionResult> SignupConfirm(SignUpViewModel model)
        {
            ApplicationUser user = await UserManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    Email = model.Email,
                    Name = model.Name,
                    UserName = model.Email,
                    EmailConfirmed = false
                };
                await UserManager.CreateAsync(user, model.Password);
                string token = await UserManager.GenerateEmailConfirmationTokenAsync(user);
                var address = Url.Action("EmailConfirm", "account", new { id = user.Id, token = token }, "https");
                MailMessage mailMessage = new MailMessage("aspnetmvc123@gmail.com", user.Email);
                mailMessage.Subject = "تایید ایمیل ";
                mailMessage.Body = $"<h1>تایید ایمیل</h1>" +
                    $"<p><a href='{address}'>لینک فعالسازی </a><hr>با سلام برای تایید ایمیل خود روی لینک زیر کلیک کنید</p>";
                mailMessage.IsBodyHtml = true;
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new System.Net.NetworkCredential("aspnetmvc123@gmail.com", "pP=-0987");
                try
                {
                    smtpClient.Send(mailMessage);
                }
                catch (Exception)
                {
                    throw;
                }
                TempData["Suc"] = "ایمیل تایید برای شما ارسال شد";
            }
            return RedirectToAction("index", "home");
        }
        public async Task<IActionResult> EmailConfirm(string id, string token)
        {
            ApplicationUser User = await UserManager.FindByIdAsync(id);
            IdentityResult result = await UserManager.ConfirmEmailAsync(User, token);
            if (result.Succeeded)
            {
                User.EmailConfirmed = true;
                TempData["Suc"] = "ایمیل شما تایید شد";
            }
            else
                TempData["Err"] = "ایمیل شما تایید نشده است";
            return RedirectToAction("index", "home");
        }
        public async Task<IActionResult> SignInConfirm(SignInViewModel model)
        {
            ApplicationUser user = await UserManager.FindByEmailAsync(model.UserName);
            if (user != null)
            {
                if (user.EmailConfirmed)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result = await SignInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);
                    if (result.Succeeded)
                    {
                        TempData["Suc"] = "خوش آمدید";
                    }
                    else
                        TempData["Err"] = "رمز شما اشتباه است";
                }
                else
                {
                    TempData["Err"] = "این کاربر وجود ندارد";
                }
            }
            return RedirectToAction("index", "home");
        }
    }
}
