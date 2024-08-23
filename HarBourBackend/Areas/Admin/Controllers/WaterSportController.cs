using System;
using Domain.Models;
using HarBourBackEnd.Helpers.Enums;
using HarBourBackEnd.Helpers.Extentions;
using HarBourBackEnd.ViewModels.About;
using HarBourBackEnd.ViewModels.DestinationCity;
using HarBourBackEnd.ViewModels.WaterSport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using Service.Service.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace HarBourBackEnd.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class WaterSportController : Controller
    {
        private readonly IWaterSportService _sportService;
        private readonly IWebHostEnvironment _env;

        public WaterSportController(IWaterSportService sportService, IWebHostEnvironment env)
        {
            _env = env;
            _sportService = sportService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var sport = await _sportService.GetAll();

            List<WaterSportVM> model = sport.Select(m => new WaterSportVM
            {
                Id = m.Id,
                BackgroundImage = m.BackgroundImage,
                Description = m.Description,
                Information = m.Information,
                Name = m.Name,
                Policy = m.Policy,
                Price = m.Price,
                SportImages = m.SportImages
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
        public async Task<IActionResult> Create(WaterSportCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            foreach (var item in request.SportImages)
            {
                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("SportImages", "File type must be image");
                    return View();
                }

                if (!item.CheckFileSize(5))
                {
                    ModelState.AddModelError("SportImages", "Image size must be less than 5");
                    return View();
                }
            }

            if (!request.BackgroundImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("BackgroundImage", "File type must be image");
                return View();
            }

            if (!request.BackgroundImage.CheckFileSize(5))
            {
                ModelState.AddModelError("BackgroundImage", "File size must be less than 5 Mb");
                return View();
            }

            string fileNameBack = Guid.NewGuid().ToString() + "-" + request.BackgroundImage.FileName;

            string pathBack = Path.Combine(_env.WebRootPath, "assets/img", fileNameBack);

            await request.BackgroundImage.SaveFileToLocalAsync(pathBack);

            List<WaterSportImageVM> images = new();

            foreach (var item in request.SportImages)
            {
                string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;

                string path = Path.Combine(_env.WebRootPath, "assets/img", fileName);

                await item.SaveFileToLocalAsync(path);

                images.Add(new WaterSportImageVM
                {
                    Image = fileName,
                });
            }

            images.FirstOrDefault().IsMain = true;

            WaterSport model = new()
            {
                Description = request.Description,
                Information = request.Information,
                Name = request.Name,
                Policy = request.Policy,
                Price = request.Price,
                BackgroundImage = fileNameBack,
                SportImages = images.Select(m => new WaterSportImage { Image = m.Image, IsMain = m.IsMain }).ToList()
            };

            await _sportService.Create(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var sport = await _sportService.GetById((int)id);

            if (sport is null) return NotFound();

            WaterSportDetailVM model = new()
            {
                Description = sport.Description,
                Information = sport.Information,
                Name = sport.Name,
                Policy = sport.Policy,
                Price = sport.Price,
                BackgroundImage = sport.BackgroundImage,
                SportImages = sport.SportImages
            };

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> DeleteImage(int? id, int? sportId)
        {
            if (id is null) return BadRequest();

            var sport = await _sportService.GetById((int)sportId);

            if (sport is null) return NotFound();

            var existImage = sport.SportImages.FirstOrDefault(m => m.Id == id);

            if (existImage.IsMain)
            {
                return Problem();
            }

            string path = Path.Combine(_env.WebRootPath, "assets/img", existImage.Image);
            path.DeleteFileFromLocal();

            await _sportService.DeleteImage(existImage);
            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> ChangeMainImage(int? id, int? sportId)
        {
            if (id is null || sportId is null) return BadRequest();

            await _sportService.ChangeMainImage((int)sportId, (int)id);

            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var existSport = await _sportService.GetById((int)id);

            if (existSport is null) return NotFound();

            foreach (var item in existSport.SportImages)
            {
                var path = Path.Combine(_env.WebRootPath, "assets/img", item.Image);
                path.DeleteFileFromLocal();
            }

            var pathBack = Path.Combine(_env.WebRootPath, "assets/img", existSport.BackgroundImage);
            pathBack.DeleteFileFromLocal();

            await _sportService.Delete(existSport);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var existSport = await _sportService.GetById((int)id);

            if (existSport is null) return NotFound();

            WaterSportEditVM model = new()
            {
                Name = existSport.Name,
                Description = existSport.Description,
                Information = existSport.Information,
                Policy = existSport.Policy,
                Price = existSport.Price,
                ExistSportImages = existSport.SportImages,
                ExistBackgroundImage = existSport.BackgroundImage
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async  Task<IActionResult> Edit( int? id , WaterSportEditVM request)
        {

            if (id is null) return BadRequest();

            var existSport = await _sportService.GetById((int)id);

            if (existSport is null) return NotFound();

            if (!ModelState.IsValid)
            {
                request.ExistSportImages = existSport.SportImages.ToList();
                return View(request);
            }

            var images = existSport.SportImages.ToList();
            request.ExistBackgroundImage = existSport.BackgroundImage;


            if (request.NewBackgroundImage is not null)
            {
                string newFileName = Guid.NewGuid().ToString() + "_" + request.NewBackgroundImage.FileName;
                string newPath = Path.Combine(_env.WebRootPath, "Admin/assets/img", newFileName);
                await request.NewBackgroundImage.SaveFileToLocalAsync(newPath);

                string oldPath = Path.Combine(_env.WebRootPath, "assets/img", existSport.BackgroundImage);
                oldPath.DeleteFileFromLocal();

                request.ExistBackgroundImage = newFileName;

                if (request.NewSportImages is not null)
                {
                    foreach (var item in request.NewSportImages)
                    {
                        if (!item.CheckFileType("image/"))
                        {
                            ModelState.AddModelError("NewProductImages", "File type must be image");
                            request.ExistSportImages = existSport.SportImages.ToList();
                            return View(request);
                        }

                        if (!item.CheckFileSize(2))
                        {
                            ModelState.AddModelError("NewProductImages", "Image size must be less than 2");
                            request.ExistSportImages = existSport.SportImages.ToList();
                            return View(request);
                        }
                    }
                    foreach (var item in request.NewSportImages)
                    {
                        string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;

                        string path = Path.Combine(_env.WebRootPath, "assets/img", fileName);

                        await item.SaveFileToLocalAsync(path);

                        images.Add(new WaterSportImage
                        {
                            Image = fileName
                        });
                    }
                }
            }

            WaterSport sport = new()
            {
                Name = request.Name,
                Description = request.Description,
                Information = request.Information,
                Policy = request.Policy,
                Price = request.Price,
                BackgroundImage = request.ExistBackgroundImage,
                SportImages = images
            };
            await _sportService.Edit((int)id, sport);
            return RedirectToAction(nameof(Index));
        }
    }
}

