using System;
using Domain.Models;
using HarBourBackEnd.ViewModels;
using HarBourBackEnd.ViewModels.Contact;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;

namespace HarBourBackEnd.Controllers
{
	public class ContactController : Controller
    {
        private readonly ISettingService _settingService;
        private readonly IComplaintService _complaintService;
        private readonly UserManager<AppUser> _userManager;
        public ContactController(ISettingService settingService,
                                 IComplaintService complaintService,
                                 UserManager<AppUser> userManager)
        {
            _settingService = settingService;
            _complaintService = complaintService;
            _userManager = userManager;

        }

        public async Task<IActionResult> Index()
        {
            AppUser user = new();
            if (User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
            }
            var setting = await _settingService.GetAll();

            Dictionary<string, string> values = new();

            foreach (KeyValuePair<int, Dictionary<string, string>> item in setting)
            {
                values.Add(item.Value["Key"], item.Value["Value"]);
            }

            ContactVM model = new()
            {
                Settings = values,
                UserEmail = user.Email,
                UserFullName = user.FullName,
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddSuggestion(string userFullName, string userEmail, string suggestion,string subject,string phoneNumber)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Problem();
            }
            await _complaintService.Create(new ComplaintSuggest { UserEmail = userEmail, UserFullName = userFullName, UserSuggest = suggestion, Subject = subject, UserPhone = phoneNumber });
            return Ok();
        }

    }
}

