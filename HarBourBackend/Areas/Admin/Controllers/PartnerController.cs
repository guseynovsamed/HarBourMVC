using System;
using Domain.Models;
using HarBourBackEnd.Helpers.Enums;
using HarBourBackEnd.Helpers.Extentions;
using HarBourBackEnd.ViewModels.Partner;
using HarBourBackEnd.ViewModels.Sliders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;

namespace HarBourBackEnd.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class PartnerController : Controller
    {
        private readonly IPartnerService _partnerService;
        private readonly IWebHostEnvironment _env;

        public PartnerController(IPartnerService partnerService, IWebHostEnvironment env)
        {
            _env = env;
            _partnerService = partnerService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var datas = await _partnerService.GetAll();

            List<PartnerVM> model = datas.Select(m => new PartnerVM { Id = m.Id, Logo = m.Logo }).ToList();

            return View(model);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(PartnerCreateVM request)
        {
            if (!ModelState.IsValid) return View();

            if (!request.Logo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Logo", "File type must be image");
                return View();
            }


            if (!request.Logo.CheckFileSize(5))
            {
                ModelState.AddModelError("Logo", "File size must be less than 5 Mb");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + "-" + request.Logo.FileName;

            string path = Path.Combine(_env.WebRootPath, "assets/img", fileName);

            await request.Logo.SaveFileToLocalAsync(path);

            await _partnerService.Create(new Partner { Logo = fileName });
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var existLogo = await _partnerService.GetById((int)id);

            if (existLogo is null) return NotFound();

            PartnerDetailVM response = new()
            {
                Logo = existLogo.Logo
            };

            return View(response);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var existLogo = await _partnerService.GetById((int)id);

            if (existLogo is null) return NotFound();

            string logo = Path.Combine(_env.WebRootPath, "assets/img", existLogo.Logo);

            logo.DeleteFileFromLocal();

            await _partnerService.Delete(existLogo);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var existLogo = await _partnerService.GetById((int)id);

            if (existLogo is null) return NotFound();

            PartnerEditVM response = new()
            {
                ExistLogo = existLogo.Logo
            };

            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id , PartnerEditVM request)
        {
            if (id is null) return BadRequest();

            var existLogo = await _partnerService.GetById((int)id);

            if (existLogo is null) return NotFound();

            if(request.NewLogo is not null)
            {
                if (!request.NewLogo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("NewLogo", "File type must be image");
                    request.ExistLogo = existLogo.Logo;
                    return View(request);
                }

                if (!request.NewLogo.CheckFileSize(5))
                {
                    ModelState.AddModelError("NewLogo", "File size must be less than 5 Mb");
                    request.ExistLogo = existLogo.Logo;
                    return View(request);
                }


                string oldPath = Path.Combine(_env.WebRootPath, "assets/img", existLogo.Logo);
                oldPath.DeleteFileFromLocal();


                string newFileName = Guid.NewGuid().ToString() + "-" + request.NewLogo.FileName;
                string newPath = Path.Combine(_env.WebRootPath, "assets/img", newFileName);
                await request.NewLogo.SaveFileToLocalAsync(newPath);

                await _partnerService.Edit((int)id, new Partner {  Logo = newFileName });
            }
            else
            {
                await _partnerService.Edit((int)id, new Partner { Logo = existLogo.Logo });
            }


            return RedirectToAction(nameof(Index));
        }
    }
}

