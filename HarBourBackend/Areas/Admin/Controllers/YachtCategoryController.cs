using System;
using System.Data;
using Domain.Models;
using HarBourBackEnd.Helpers.Extentions;
using HarBourBackEnd.ViewModels.YacthCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using Service.Service.Interfaces;

namespace HarBourBackEnd.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class YachtCategoryController : Controller
    {
        private readonly IYachtCategoryService _categoryService;
        private readonly IWebHostEnvironment _env;


        public YachtCategoryController(IYachtCategoryService categoryService, IWebHostEnvironment env)
        {
            _categoryService = categoryService;
            _env = env;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var datas = await _categoryService.GetAll();

            List<YachtCategoryVM> model = datas.Select(m => new YachtCategoryVM
            {
                Id = m.Id,
                Description = m.Description,
                IconImage = m.IconImage,
                Name = m.Name
            }).ToList();
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
        public async Task<IActionResult> Create(YachtCategoryCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!request.IconImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("SliderImage", "File type must be image");
                return View();
            }

            if (!request.IconImage.CheckFileSize(3))
            {
                ModelState.AddModelError("SliderImage", "File size must be less than 3 Mb");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + "-" + request.IconImage.FileName;

            string path = Path.Combine(_env.WebRootPath, "assets/img", fileName);

            await request.IconImage.SaveFileToLocalAsync(path);

            await _categoryService.Create(new YachtCategory { IconImage = fileName, Description = request.Description, Name = request.Name });

            return RedirectToAction(nameof(Index));

        }



        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var existCategory = await _categoryService.GetById((int)id);

            if (existCategory is null) return NotFound();

            YachtCategoryDetailVM response = new() { IconImage = existCategory.IconImage, Name = existCategory.Name, Description = existCategory.Description };
            return View(response);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var existCategory = await _categoryService.GetById((int)id);

            if (existCategory is null) return NotFound();

            string existImage = Path.Combine(_env.WebRootPath, "assets/img", existCategory.IconImage);

            existImage.DeleteFileFromLocal();

            await _categoryService.Delete(existCategory);
            return RedirectToAction(nameof(Index));

        }




        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var existCategory = await _categoryService.GetById((int)id);

            if (existCategory is null) return NotFound();

            YachtCategoryEditVM response = new()
            {
                Name = existCategory.Name,
                Description = existCategory.Description,
                IconImage = existCategory.IconImage
            };

            return View(response);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, YachtCategoryEditVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id is null) return BadRequest();

            var existCategory = await _categoryService.GetById((int)id);

            if (existCategory is null) return NotFound();

            if (request.NewIconImage is not null)
            {
                if (!request.NewIconImage.CheckFileType("image/"))
                {
                    ModelState.AddModelError("NewIconImage", "File type must be image");
                    request.IconImage = existCategory.IconImage;
                    return View(request);
                }

                if (!request.NewIconImage.CheckFileSize(3))
                {
                    ModelState.AddModelError("NewIconImage", "File size must be less than 3 Mb");
                    request.IconImage = existCategory.IconImage;
                    return View(request);
                }

                string oldPath = Path.Combine(_env.WebRootPath, "assets/img", existCategory.IconImage);
                oldPath.DeleteFileFromLocal();


                string newFileName = Guid.NewGuid().ToString() + "-" + request.NewIconImage.FileName;
                string newPath = Path.Combine(_env.WebRootPath, "assets/img", newFileName);
                await request.NewIconImage.SaveFileToLocalAsync(newPath);

                await _categoryService.Edit((int)id, new YachtCategory { Name = request.Name, Description = request.Description, IconImage = newFileName });

            }
            else
            {
                await _categoryService.Edit((int)id, new YachtCategory { Name = request.Name, Description = request.Description, IconImage = existCategory.IconImage });
            }


            return RedirectToAction(nameof(Index));

        }


    }
}


