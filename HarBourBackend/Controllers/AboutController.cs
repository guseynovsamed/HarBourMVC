using System;
using HarBourBackEnd.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;

namespace HarBourBackEnd.Controllers
{
    public class AboutController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IAboutService _aboutService;
        private readonly IPartnerService _partnerService;
        private readonly IStaffService _staffService;

        public AboutController(IAboutService aboutService,
                               ICommentService commentService,
                               IPartnerService partnerService,
                               IStaffService staffService)
        {
            _aboutService = aboutService;
            _commentService = commentService;
            _partnerService = partnerService;
            _staffService = staffService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var about = await _aboutService.GetAll();
            var comment = await _commentService.GetAll();
            var parnter = await _partnerService.GetAll();
            var staff = await _staffService.GetAll();


            AboutPageVM model = new()
            {
                About = about.FirstOrDefault(),
                Comment = comment,
                Partner=parnter,
                Staff=staff
            };

            return View(model);
        }




    }
}

