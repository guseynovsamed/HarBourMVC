using System;
using Domain.Models;
using HarBourBackEnd.Helpers.Enums;
using HarBourBackEnd.Helpers.Extentions;
using HarBourBackEnd.ViewModels.About;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;

namespace HarBourBackEnd.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;
        private readonly IWebHostEnvironment _env;


        public AboutController(IAboutService aboutService, IWebHostEnvironment env)
        {
            _aboutService = aboutService;
            _env = env;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var about = await _aboutService.GetAll();

            List<AboutVM> model = about.Select(m => new AboutVM
            {
                Id = m.Id,
                AboutImages = m.AboutImages,
                Advantage1 = m.Advantage1,
                Advantage2 = m.Advantage2,
                Advantage3 = m.Advantage3,
                Description = m.Description,
                SinceDay = m.SinceDay,
                Title = m.Title
            }).ToList();

            return View(model);
        }




        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(AboutCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            foreach (var item in request.AboutImages)
            {
                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("AboutImages", "File type must be image");
                    return View();
                }

                if (!item.CheckFileSize(2))
                {
                    ModelState.AddModelError("AboutImages", "Image size must be less than 2");
                    return View();
                }
            }

            List<AboutImageVM> images = new();

            foreach (var item in request.AboutImages)
            {
                string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;

                string path = Path.Combine(_env.WebRootPath, "assets/img", fileName);

                await item.SaveFileToLocalAsync(path);

                images.Add(new AboutImageVM
                {
                    Image = fileName,
                });
            }


            images.FirstOrDefault().IsMain = true;

            About about = new()
            {
                Title = request.Title,
                Description = request.Description,
                SinceDay = request.SinceDay,
                Advantage1 = request.Advantage1,
                Advantage2 = request.Advantage2,
                Advantage3 = request.Advantage3,
                AboutImages = images.Select(m => new AboutImage { Image = m.Image, IsMain = m.IsMain }).ToList()
            };


            await _aboutService.Create(about);

            return RedirectToAction(nameof(Index));
        }





        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var about = await _aboutService.GetById((int)id);

            if (about is null) return NotFound();

            AboutDetailVM model = new()
            {
                Title = about.Title,
                SinceDay = about.SinceDay,
                Advantage1 = about.Advantage1,
                Advantage2 = about.Advantage2,
                Advantage3 = about.Advantage3,
                Description = about.Description,
                AboutImages = about.AboutImages
            };

            return View(model);
        }






        [HttpPost]
        public async Task<IActionResult> DeleteImage(int? id, int? aboutId)
        {
            if (id is null) return BadRequest();

            About about = await _aboutService.GetById((int)aboutId);

            if (about is null) return NotFound();


            var existImage = about.AboutImages.FirstOrDefault(m => m.Id == id);

            if (existImage.IsMain)
            {
                return Problem();
            }

            string path = Path.Combine(_env.WebRootPath, "assets/img", existImage.Image);
            path.DeleteFileFromLocal();

            await _aboutService.DeleteImage(existImage);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> ChangeMainImage(int? id, int? aboutId)
        {
            if (id is null || aboutId is null) return BadRequest();

            await _aboutService.ChangeMainImage((int)aboutId, (int)id);

            return Ok();
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var existAbout = await _aboutService.GetById((int)id);

            if (existAbout is null) return NotFound();

            foreach (var item in existAbout.AboutImages)
            {
                var path = Path.Combine(_env.WebRootPath, "assets/img", item.Image);
                path.DeleteFileFromLocal();
            }

            await _aboutService.Delete(existAbout);
            return RedirectToAction(nameof(Index));
        }






        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (id is null) return BadRequest();

            var about = await _aboutService.GetById((int)id);

            if (about is null) return NotFound();


            AboutEditVM aboutEdit = new()
            {
                Title = about.Title,
                Advantage1 = about.Advantage1,
                Advantage2 = about.Advantage2,
                Advantage3 = about.Advantage3,
                Description = about.Description,
                ExistAboutImages = about.AboutImages
            };

            return View(aboutEdit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, AboutEditVM request)
        {


            if (id is null) return BadRequest();

            var about = await _aboutService.GetById((int)id);

            if (about is null) return NotFound();

            List<AboutImage> images = about.AboutImages.ToList();
            if (!ModelState.IsValid)
            {
                request.ExistAboutImages = images;
                return View(request);
            }

            if (request.AboutImages is not null)
            {
                foreach (var item in request.AboutImages)
                {
                    if (!item.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("AboutImages", "File type must be image");
                        request.ExistAboutImages = about.AboutImages;
                        return View(request);
                    }

                    if (!item.CheckFileSize(2))
                    {
                        ModelState.AddModelError("AboutImages", "Image size must be less than 2");
                        request.ExistAboutImages = about.AboutImages;
                        return View(request);
                    }
                }

                foreach (var item in request.AboutImages)
                {
                    string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;

                    string path = Path.Combine(_env.WebRootPath, "assets/img", fileName);

                    await item.SaveFileToLocalAsync(path);

                    images.Add(new AboutImage
                    {
                        Image = fileName
                    });
                }
            }


            About aboutEdit = new()
            {
                Title = about.Title,
                Advantage1 = about.Advantage1,
                Advantage2 = about.Advantage2,
                Advantage3 = about.Advantage3,
                Description = about.Description,
                AboutImages = images
            };

            await _aboutService.Edit((int)id, aboutEdit);

            return RedirectToAction(nameof(Index));
        }



    }
}

