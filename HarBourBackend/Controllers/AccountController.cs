using System;
using Domain.Models;
using HarBourBackEnd.ViewModels.Account;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using HarBourBackEnd.Helpers;
using MailKit.Net.Smtp;
using HarBourBackEnd.Helpers.Enums;

namespace HarBourBackEnd.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppSettings _appSettings;

        public AccountController(UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager,
                                 RoleManager<IdentityRole> roleManager,
                                 IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _appSettings = appSettings.Value;
        }


        [HttpGet]
        public  IActionResult SignIn()
        {
            return View();
        }



        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }



        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(RegisterVM request)
        {
            if (!ModelState.IsValid) return View(request);

            AppUser newUser = new()
            {
                FullName = request.FullName,
                UserName = request.UserName,
                Email = request.Email,
            };

            var result = await _userManager.CreateAsync(newUser, request.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
                return View(request);
            }

            await _userManager.AddToRoleAsync(newUser, nameof(Roles.Admin));

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            string url = Url.Action(nameof(ConfirmEmail), "Account", new { userId = newUser.Id, token }, Request.Scheme, Request.Host.ToString());

            string html = string.Empty;

            using (StreamReader reader = new("wwwroot/templates/emailconfirmation.html"))
            {
                html = await reader.ReadToEndAsync();
            }

            html = html.Replace("{link}", url);
            html = html.Replace("{Username}", newUser.FullName);

            string subject = "Email confirmation";

            SendEmail(newUser.Email, subject, html);

            return RedirectToAction(nameof(VerifyEmail));
        }

        [HttpGet]
        public IActionResult VerifyEmail()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.ConfirmEmailAsync(user, token);
            return RedirectToAction(nameof(SignIn));
        }


        public void SendEmail(string to, string subject, string html, string from = null)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from ?? _appSettings.From));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect(_appSettings.Server, _appSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_appSettings.Username, _appSettings.Password);
            smtp.Send(email);
            smtp.Disconnect(true);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(LoginVM request)
        {
            if (!ModelState.IsValid) return View(request);

            var existUser = await _userManager.FindByEmailAsync(request.EmailOrUsername);

            if (existUser is null)
            {
                existUser = await _userManager.FindByNameAsync(request.EmailOrUsername);
            }

            if (existUser is null)
            {
                ModelState.AddModelError(string.Empty, "Login failed");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(existUser, request.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Login failed");
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> CreateRoles()
        {
            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                if (!await _roleManager.RoleExistsAsync(nameof(role)))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
                }
            }
            return Ok();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM forgotPassword)
        {
            if (!ModelState.IsValid) return View();

            AppUser existUser = await _userManager.FindByEmailAsync(forgotPassword.Email);

            if (existUser is null)
            {
                ModelState.AddModelError("Email", "User not found");
                return View();
            }

            string token = await _userManager.GeneratePasswordResetTokenAsync(existUser);
            string url = Url.Action(nameof(ResetPassword), "Account", new { userId = existUser.Id, token }, Request.Scheme, Request.Host.ToString());

            string html = string.Empty;

            using (StreamReader reader = new("wwwroot/templates/forgotpassword.html"))
            {
                html = await reader.ReadToEndAsync();
            }

            html = html.Replace("{link}", url);
            html = html.Replace("{Username}", existUser.FullName);

            string subject = "Password reset email";

            SendEmail(existUser.Email, subject, html);


            return RedirectToAction("VerifyEmail");

        }


        [HttpGet]
        public IActionResult ResetPassword(string userId, string token)
        {
            var model = new ResetPasswordVM { UserId = userId, Token = token };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPassword)
        {
            if (!ModelState.IsValid) return View(resetPassword);

            AppUser existUser = await _userManager.FindByIdAsync(resetPassword.UserId);

            if (existUser is null) return NotFound();

            if (await _userManager.CheckPasswordAsync(existUser, resetPassword.Password))
            {
                ModelState.AddModelError("", "Same as the old password");
                return View(resetPassword);
            }

            await _userManager.ResetPasswordAsync(existUser, resetPassword.Token, resetPassword.Password);

            return RedirectToAction(nameof(SignIn));
        }


    }
}

