using System;
using HarBourBackEnd.ViewModels.Staff;
using Microsoft.AspNetCore.Mvc;
using Service.Service;
using Service.Service.Interfaces;

namespace HarBourBackEnd.Controllers
{
    public class StaffDetailController : Controller
    {
        private readonly IStaffService _staffService;

        public StaffDetailController(IStaffService staffService)
        {
            _staffService = staffService;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {

            if (id is null) return BadRequest();

            var existStaff = await _staffService.GetById((int)id);

            if (existStaff is null) return NotFound();


            StaffDetailVM staff = new()
            {
                Id = existStaff.Id,
                Awards = existStaff.Awards,
                Biography = existStaff.Biography,
                Description = existStaff.Description,
                Education = existStaff.Education,
                Fullname = existStaff.Fullname,
                Image = existStaff.Image,
                Mail = existStaff.Mail,
                Phone = existStaff.Phone,
                Position = existStaff.Position.Name
            };

            var staffs = await _staffService.GetAll();

            StaffDetailPageVM model = new()
            {
                StaffDetail = staff,
                Staff = staffs
            };

            return View(model);
        }
    }
}

