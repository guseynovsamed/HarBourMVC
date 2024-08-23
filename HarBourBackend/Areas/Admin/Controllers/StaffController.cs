using System;
using Domain.Models;
using HarBourBackEnd.Helpers.Enums;
using HarBourBackEnd.Helpers.Extentions;
using HarBourBackEnd.ViewModels.Staff;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Org.BouncyCastle.Asn1.Ocsp;
using Service.Service.Interfaces;

namespace HarBourBackEnd.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class StaffController : Controller
    {
        private readonly IStaffService _staffService;
        private readonly IWebHostEnvironment _env;
        private readonly IPositionService _positionService;

        public StaffController(IStaffService staffService, IWebHostEnvironment env, IPositionService positionService)
        {
            _staffService = staffService;
            _positionService = positionService;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var staff = await _staffService.GetAll();

            var model = staff.Select(m => new StaffVM
            {
                Id = m.Id,
                Awards = m.Awards,
                Image = m.Image,
                Biography = m.Biography,
                Description = m.Description,
                Education = m.Education,
                Fullname = m.Fullname,
                Mail = m.Mail,
                Phone = m.Phone,
                Position = m.Position.Name
            }).ToList();

            return View(model);
        }




        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var position = await _positionService.GetAll();

            ViewBag.position = new SelectList(position, "Id", "Name");


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(StaffCreateVM request)
        {
            var position = await _positionService.GetAll();

            ViewBag.position = new SelectList(position, "Id", "Name");

            if (!ModelState.IsValid)
            {
                return View();
            }


            if (!request.Image.CheckFileType("image/"))
            {
                ModelState.AddModelError("Image", "File type must be image");
                return View();
            }


            if (!request.Image.CheckFileSize(5))
            {
                ModelState.AddModelError("Image", "File size must be less than 5 Mb");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + "-" + request.Image.FileName;

            string path = Path.Combine(_env.WebRootPath, "assets/img", fileName);

            await request.Image.SaveFileToLocalAsync(path);


            Staff staff = new()
            {
                Fullname = request.Fullname,
                Phone = request.Phone,
                Mail = request.Mail,
                Description = request.Description,
                Image = fileName,
                Biography = request.Biography,
                Awards = request.Awards,
                PositionId = request.PositionId,
                Education = request.Education,
            };

            await _staffService.Create(staff);
            return RedirectToAction(nameof(Index));
        }




        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var staff = await _staffService.GetById((int)id);

            if (staff is null) return NotFound();

            StaffDetailVM model = new()
            {
                Fullname = staff.Fullname,
                Phone = staff.Phone,
                Mail = staff.Mail,
                Description = staff.Description,
                Image = staff.Image,
                Biography = staff.Biography,
                Awards = staff.Awards,
                Position = staff.Position.Name,
                Education = staff.Education
            };

            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var existStaff = await _staffService.GetById((int)id);

            if (existStaff is null) return NotFound();

            var path = Path.Combine(_env.WebRootPath, "assets/img", existStaff.Image);

            path.DeleteFileFromLocal();

            await _staffService.Delete(existStaff);

            return RedirectToAction(nameof(Index));

        }




        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var position = await _positionService.GetAll();

            ViewBag.position = new SelectList(position, "Id", "Name");

            if (id is null) return BadRequest();

            var existStaff = await _staffService.GetById((int)id);

            if (existStaff is null) return NotFound();

            StaffEditVM staffEdit = new()
            {
                Fullname = existStaff.Fullname,
                Phone = existStaff.Phone,
                Mail = existStaff.Mail,
                Description = existStaff.Description,
                Image = existStaff.Image,
                Biography = existStaff.Biography,
                Awards = existStaff.Awards,
                PositionId = existStaff.PositionId,
                Education = existStaff.Education
            };

            return View(staffEdit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, StaffEditVM request)
        {
            var position = await _positionService.GetAll();
            ViewBag.position = new SelectList(position, "Id", "Name");
            if (!ModelState.IsValid)
            {
                return View();
            }



            if (id is null) return BadRequest();

            var existStaff = await _staffService.GetById((int)id);

            if (existStaff is null) return NotFound();

            if (request.NewImage is not null)
            {
                if (!request.NewImage.CheckFileType("image/"))
                {
                    ModelState.AddModelError("NewImage", "File type must be image");
                    return View();
                }


                if (!request.NewImage.CheckFileSize(5))
                {
                    ModelState.AddModelError("NewImage", "File size must be less than 5 Mb");
                    return View();
                }

                string oldPath = Path.Combine(_env.WebRootPath, "assets/img", existStaff.Image);
                oldPath.DeleteFileFromLocal();


                string newFileName = Guid.NewGuid().ToString() + "-" + request.NewImage.FileName;
                string newPath = Path.Combine(_env.WebRootPath, "assets/img", newFileName);
                await request.NewImage.SaveFileToLocalAsync(newPath);

                await _staffService.Edit((int)id, new Staff
                {
                    Fullname = request.Fullname,
                    Description = request.Description,
                    Phone = request.Phone,
                    Mail = request.Mail,
                    Biography = request.Biography,
                    Awards = request.Awards,
                    PositionId = request.PositionId,
                    Education = request.Education,
                    Image = newFileName
                });

            }
            else
            {
                await _staffService.Edit((int)id, new Staff
                {
                    Fullname = request.Fullname,
                    Description = request.Description,
                    Phone = request.Phone,
                    Mail = request.Mail,
                    Biography = request.Biography,
                    Awards = request.Awards,
                    PositionId = request.PositionId,
                    Education = request.Education,
                    Image = existStaff.Image
                });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

