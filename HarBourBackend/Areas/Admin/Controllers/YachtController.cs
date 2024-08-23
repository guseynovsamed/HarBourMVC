using System;
using Domain.Models;
using HarBourBackEnd.Helpers.Enums;
using HarBourBackEnd.Helpers.Extentions;
using HarBourBackEnd.ViewModels.Yacht;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Service.Interfaces;

namespace HarBourBackEnd.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class YachtController : Controller
    {
        private readonly IYachtService _yachtService;
        private readonly IWebHostEnvironment _env;
        private readonly IYachtCategoryService _category;

        public YachtController(IYachtService yachtService, IWebHostEnvironment env, IYachtCategoryService category)
        {
            _category = category;
            _env = env;
            _yachtService = yachtService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var yacht = await _yachtService.GetAll();

            var model = yacht.Select(m => new YachtVM
            {
                YachtImages = m.YachtImages,
                Build = m.Build,
                Category = m.YachtCategory.Name,
                Description = m.Description,
                Guest = m.Guest,
                Id = m.Id,
                Information = m.Information,
                Length = m.Length,
                Name = m.Name,
                Price = m.Price
            }).ToList();

            return View(model);
        }



        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var category = await _category.GetAll();

            ViewBag.category = new SelectList(category, "Id", "Name");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(YachtCreateVM request)
        {
            var category = await _category.GetAll();

            ViewBag.category = new SelectList(category, "Id", "Name");

            if (!ModelState.IsValid)
            {
                return View();
            }

            foreach (var item in request.YachtImages)
            {
                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Images", "File type must be image");
                    return View();
                }

                if (!item.CheckFileSize(5))
                {
                    ModelState.AddModelError("Images", "Image size must be less than 5");
                    return View();
                }
            }

            List<YachtImageVM> images = new();


            foreach (var item in request.YachtImages)
            {
                string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;

                string path = Path.Combine(_env.WebRootPath, "assets/img", fileName);

                await item.SaveFileToLocalAsync(path);

                images.Add(new YachtImageVM
                {
                    Image = fileName
                });
            }


            images.FirstOrDefault().IsMain = true;


            Yacht yacht = new()
            {
                Build = request.Build,
                YachtCategoryId = request.CategoryId,
                Description = request.Description,
                Guest = request.Guest,
                Information = request.Information,
                Length = request.Length,
                Name = request.Name,
                Price = request.Price,
                YachtImages = images.Select(m => new YachtImage { Image = m.Image, IsMain = m.IsMain }).ToList()
            };


            await _yachtService.Create(yacht);
            return RedirectToAction(nameof(Index));
        }




        [HttpPost]
        public async Task<IActionResult> DeleteImage(int? id, int? yachtId)
        {
            if (id is null) return BadRequest();

            var yacht = await _yachtService.GetById((int)yachtId);

            if (yacht is null) return NotFound();


            var existImage = yacht.YachtImages.FirstOrDefault(m => m.Id == id);

            if (existImage.IsMain)
            {
                return Problem();
            }

            string path = Path.Combine(_env.WebRootPath, "Admin/assets/img", existImage.Image);
            path.DeleteFileFromLocal();

            await _yachtService.DeleteImage(existImage);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeMainImage(int? id, int? yachtId)
        {
            if (id is null || yachtId is null) return BadRequest();

            var yacht = await _yachtService.GetById((int)yachtId);

            if (yacht is null) NotFound();

            await _yachtService.ChangeMainImage((int)yachtId, (int)id);

            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var existYacht = await _yachtService.GetById((int)id);

            if (existYacht is null) return NotFound();

            foreach (var item in existYacht.YachtImages)
            {
                var path = Path.Combine(_env.WebRootPath, "img", item.Image);
                path.DeleteFileFromLocal();
            }

            await _yachtService.Delete(existYacht);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var yacht = await _yachtService.GetById((int)id);

            if (yacht is null) return NotFound();


            YachtDetailVM model = new()
            {
                Build = yacht.Build,
                Description = yacht.Description,
                Guest = yacht.Guest,
                Information = yacht.Information,
                Length = yacht.Length,
                Name = yacht.Name,
                Price = yacht.Price,
                YachtCategory = yacht.YachtCategory.Name,
                YachtImages = yacht.YachtImages
            };

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {


            var category = await _category.GetAll();

            ViewBag.category = new SelectList(category, "Id", "Name");

            if (id is null) return BadRequest();

            var yatch = await _yachtService.GetById((int)id);

            if (yatch is null) return NotFound();

            YachtEditVM model = new()
            {
                Name = yatch.Name,
                Build = yatch.Build,
                CategoryId = yatch.YachtCategoryId,
                Description = yatch.Description,
                Guest = yatch.Guest,
                Information = yatch.Information,
                Length = yatch.Length,
                YachtImages = yatch.YachtImages.ToList(),
                Price = yatch.Price
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, YachtEditVM request)
        {

            var category = await _category.GetAll();

            ViewBag.category = new SelectList(category, "Id", "Name");

            if (id is null) return BadRequest();

            var yacht = await _yachtService.GetById((int)id);

            if (yacht is null) return NotFound();


            List<YachtImage> images = yacht.YachtImages.ToList();
            if (!ModelState.IsValid)
            {
                request.YachtImages = images;
                return View(request);
            }

            if (request.NewYachtImages is not null)
            {
                foreach (var item in request.NewYachtImages)
                {
                    if (!item.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("NewYachtImages", "File type must be image");
                        request.YachtImages = yacht.YachtImages.ToList();
                        return View(request);
                    }

                    if (!item.CheckFileSize(5))
                    {
                        ModelState.AddModelError("NewYachtImages", "Image size must be less than 5");
                        request.YachtImages = yacht.YachtImages.ToList();
                        return View(request);
                    }
                }

                foreach (var item in request.NewYachtImages)
                {
                    string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;

                    string path = Path.Combine(_env.WebRootPath, "assets/img", fileName);

                    await item.SaveFileToLocalAsync(path);

                    images.Add(new YachtImage
                    {
                        Image = fileName
                    });
                }
            }


            Yacht model = new()
            {
                Name = request.Name,
                Build = request.Build,
                YachtCategoryId = request.CategoryId,
                Description = request.Description,
                Guest = request.Guest,
                Information = request.Information,
                Length = request.Length,
                YachtImages = images,
                Price = request.Price
            };

            await _yachtService.Edit((int)id, model);
            return RedirectToAction(nameof(Index));
        }
    }
}

