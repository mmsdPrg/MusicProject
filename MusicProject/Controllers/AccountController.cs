using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicProject.Areas.Identity.Data;
using MusicProject.Data;
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
        DB_Music DB;
        public AccountController(UserManager<ApplicationUser> _UserManager, SignInManager<ApplicationUser> _SignInManager, DB_Music _DB)
        {
            UserManager = _UserManager;
            SignInManager = _SignInManager;
            DB = _DB;
        }
        #region SignUP&SignIN
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
                    EmailConfirmed = false,
                    Family=model.Family
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
                smtpClient.Credentials = new System.Net.NetworkCredential("aspnetmvc123@gmail.com", "txbgumxwljwxpurj");

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
                        if(await UserManager.IsInRoleAsync(user, "Admin"))
                        {
                            return Redirect("/admin/home/index");
                        }
                        TempData["Suc"] = "خوش آمدید";
                    }
                    else
                    {
                        TempData["Err"] = "رمز شما اشتباه است";
                    }
                }
                else
                {
                    MailMessage mailMessage = new MailMessage("aspnetmvc123@gmail.com", user.Email);
                    string token = await UserManager.GenerateEmailConfirmationTokenAsync(user);
                    var address = Url.Action("EmailConfirm", "account", new { id = user.Id, token = token }, "https");
                    mailMessage.Subject = "تایید ایمیل ";
                    mailMessage.Body = $"<h1>تایید ایمیل</h1>" +
                        $"<p><a href='{address}'>لینک فعالسازی </a><hr>با سلام برای تایید ایمیل خود روی لینک زیر کلیک کنید</p>";
                    mailMessage.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.Credentials = new System.Net.NetworkCredential("aspnetmvc123@gmail.com", "txbgumxwljwxpurj");
                    smtp.EnableSsl = true;
                    try
                    {
                        smtp.Send(mailMessage);
                        TempData["info"] = "ایمیل تایید مجددا برای شما ارسال شد";
                    }
                    catch (Exception)
                    {
                        TempData["Err"] = "ایمیل تایید ارسال نشد";
                    }
                }

            }
            else
            {
                TempData["Err"] = "این کاربر وجود ندارد";
            }
            return RedirectToAction("SignIn", "account");
        }
        public async Task<IActionResult> SigningOut()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
        public async Task<IActionResult> CheckUserName(string Email)
        {
            ApplicationUser user = await UserManager.FindByEmailAsync(Email);
            if (user == null)
                return Json(true);
            else
                return Json(false);
        }

        public async Task<IActionResult> ForgotPass(string UserName)
        {
            ApplicationUser user = await UserManager.FindByEmailAsync(UserName);
            if (user != null)
            {
                string Token = await UserManager.GeneratePasswordResetTokenAsync(user);
                MailMessage mailMessage = new MailMessage("aspnetmvc123@gmail.com", user.Email);
                var address = Url.Action("ForgotPassConfirm", "account", new { id = user.Id, Token = Token }, "https");
                mailMessage.Subject = "بازیابی رمز عبور";
                mailMessage.Body = $"<h1> بازیابی رمز عبور</h1>" +
                    $"<p><a href='{address}'>لینک بازیابی رمز عبور </a><hr>با سلام برای بازیابی رمز عبور خود روی لینک زیر کلیک کنید</p>";
                mailMessage.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential("aspnetmvc123@gmail.com","txbgumxwljwxpurj");
                try
                {
                    smtp.Send(mailMessage);
                    return Json("ایمیل بازیابی رمز برای شما ارسال شد");
                }
                catch (Exception)
                {
                    return Json("سرویس ارسال ایمیل با مشکل مواجه شده است به پشتیبانی مراجعه کنید");
                }
            }
            else
                return Json("این کاربر وجود ندارد");
        }
        public IActionResult ForgotPassConfirm(string id, string token)
        {
            HttpContext.Session.SetString("idForgotUser", id);
            HttpContext.Session.SetString("TokenForget", token);
            return View();
        }
        public async Task<IActionResult> ForgotPassConfirmation(ForgotPassViewModel model)
        {
            var id = HttpContext.Session.GetString("idForgotUser");
            var token = HttpContext.Session.GetString("TokenForget");
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            IdentityResult result = await UserManager
                   .ResetPasswordAsync(user, token, model.password);
            if (result.Succeeded)
            {
                TempData["Suc"] = "رمز شما با موفقیت تغییر یافت";
            }
            return RedirectToAction("index", "home");

        }
        public IActionResult UserProfile()
        {
            return View();
        }
        public async Task<JsonResult> EditProfile([FromBody]EditProfileViewModel model)
        {
            ApplicationUser user = await UserManager.FindByEmailAsync(model.Email);
            if (model.OldPassword != null && model.NewPassword != null)
            {
                var IsOldPasswordTrue = await SignInManager.PasswordSignInAsync(user, model.OldPassword, false, false);
                if (IsOldPasswordTrue.Succeeded)
                {
                    user.Name = model.Name;
                    user.Family = model.Last;
                    user.Email = model.Email;
                    await UserManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    var Result = await UserManager.UpdateAsync(user);
                    if (Result.Succeeded)
                    {
                        return Json(new { Name = user.Name, family = user.Family });
                    }
                }else
                {
                    return Json(new { text = "رمز قدیمی اشتباه است" });
                }
            }
            else
            {
                user.Email = model.Email;
                user.Name = model.Name;
                user.Family = model.Last;
                var Result = await UserManager.UpdateAsync(user);
                if (Result.Succeeded)
                {
                    return Json(new { Name = user.Name, family = user.Family,text="تغییرات اعمال شد" });
                }
            }
            return Json(new {text="خطا در ویرایش"});


        }

        #endregion
    }
}
