using System;
using System.Data;
using System.IO;
using Domain.Models;
using HarBourBackEnd.Helpers;
using HarBourBackEnd.Helpers.Extentions;
using HarBourBackEnd.ViewModels.About;
using HarBourBackEnd.ViewModels.DestinationCity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Service;
using Service.Service.Interfaces;

namespace HarBourBackEnd.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class DestinationCityController : Controller
    {
        private readonly IDestinationCityService _cityService;
        private readonly IWebHostEnvironment _env;
        private readonly IDestinationCountryService _countryService;


        public DestinationCityController(IDestinationCityService cityService, IWebHostEnvironment env, IDestinationCountryService countryService)
        {
            _cityService = cityService;
            _countryService = countryService;
            _env = env;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var city = await _cityService.GetAll();

            var paginatedDatas = await _cityService.GetAllPaginatedDatas(page);

            var cityCount = await _cityService.GetCount();

            var pageCount = _cityService.GetPageCount(cityCount, 4);


            var mapedDatas = paginatedDatas.Select(m => new CityVM
            {
                AccommodationDay = m.AccommodationDay,
                BackgroundImage = m.BackgroundImage,
                Country = m.Country.Name,
                Departure = m.Departure,
                Description = m.Description,
                DestinationImages = m.DestinationImages,
                Id = m.Id,
                MaxLimit = m.MaxLimit,
                MinLimit = m.MinLimit,
                Name = m.Name,
                Price = m.Price
            }).ToList();

            Paginate<CityVM> model = new(mapedDatas, pageCount, page);

            return View(model);
        }



        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var country = await _countryService.GetAll();

            ViewBag.country = new SelectList(country, "Id", "Name");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(CityCreateVM request)
        {

            var country = await _countryService.GetAll();

            ViewBag.country = new SelectList(country, "Id", "Name");



            if (!ModelState.IsValid)
            {
                return View();
            }

            foreach (var item in request.DestinationImages)
            {
                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("DestinationImages", "File type must be image");
                    return View();
                }

                if (!item.CheckFileSize(2))
                {
                    ModelState.AddModelError("DestinationImages", "Image size must be less than 2");
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



            List<CityImageVM> images = new();


            foreach (var item in request.DestinationImages)
            {
                string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;

                string path = Path.Combine(_env.WebRootPath, "assets/img", fileName);

                await item.SaveFileToLocalAsync(path);

                images.Add(new CityImageVM
                {
                    Image = fileName,
                });
            }



            images.FirstOrDefault().IsMain = true;

            DestinationCity city = new()
            {
                AccommodationDay = request.AccommodationDay,
                Departure = request.Departure,
                Description = request.Description,
                MaxLimit = request.MaxLimit,
                MinLimit = request.MinLimit,
                Name = request.Name,
                Price = request.Price,
                CountryId = request.CountryId,
                BackgroundImage = fileNameBack,
                DestinationImages = images.Select(m => new DestinationImage { Image = m.Image, IsMain = m.IsMain }).ToList()
            };


            await _cityService.Create(city);

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var city = await _cityService.GetById((int)id);

            if (city is null) return NotFound();

            CityDetailVM model = new()
            {
                AccommodationDay = city.AccommodationDay,
                BackgroundImage = city.BackgroundImage,
                Country = city.Country.Name,
                Departure = city.Departure,
                Description = city.Description,
                DestinationImages = city.DestinationImages,
                MaxLimit = city.MaxLimit,
                MinLimit = city.MinLimit,
                Name = city.Name,
                Price = city.Price
            };

            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var existCity = await _cityService.GetById((int)id);
            if (existCity is null) return NotFound();

            foreach (var item in existCity.DestinationImages)
            {
                var path = Path.Combine(_env.WebRootPath, "assets/img", item.Image);
                path.DeleteFileFromLocal();
            }

            var pathBack = Path.Combine(_env.WebRootPath, "assets/img", existCity.BackgroundImage);
            pathBack.DeleteFileFromLocal();


            await _cityService.Delete(existCity);
            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        public async Task<IActionResult> DeleteImage(int? id, int? cityId)
        {
            if (id is null) return BadRequest();

            var city = await _cityService.GetById((int)cityId);

            if (city is null) return NotFound();


            var existImage = city.DestinationImages.FirstOrDefault(m => m.Id == id);

            if (existImage.IsMain)
            {
                return Problem();
            }

            string path = Path.Combine(_env.WebRootPath, "assets/img", existImage.Image);
            path.DeleteFileFromLocal();

            await _cityService.DeleteImage(existImage);
            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> ChangeMainImage(int? id, int? cityId)
        {
            if (id is null || cityId is null) return BadRequest();

            await _cityService.ChangeMainImage((int)cityId, (int)id);

            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var country = await _countryService.GetAll();

            ViewBag.country = new SelectList(country, "Id", "Name");

            if (id is null) return BadRequest();

            var city = await _cityService.GetById((int)id);

            if (city is null) return NotFound();


            CityEditVM model = new()
            {
                AccommodationDay = city.AccommodationDay,
                ExistBackgroundImage = city.BackgroundImage,
                CountryId = city.CountryId,
                Departure = city.Departure,
                Description = city.Description,
                ExistDestinationImages = city.DestinationImages.ToList(),
                MaxLimit = city.MaxLimit,
                MinLimit = city.MinLimit,
                Name = city.Name,
                Price = city.Price
            };

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CityEditVM request)
        {

            var country = await _countryService.GetAll();

            ViewBag.country = new SelectList(country, "Id", "Name");

            if (id is null) return BadRequest();

            var existCity = await _cityService.GetById((int)id);

            if (existCity is null) return NotFound();

            if (!ModelState.IsValid)
            {
                request.ExistDestinationImages = existCity.DestinationImages.ToList();
                return View(request);
            }

            var images = existCity.DestinationImages.ToList();
            request.ExistBackgroundImage = existCity.BackgroundImage;

            if (request.NewBackgroundImage is not null)
            {
                string newFileName = Guid.NewGuid().ToString() + "_" + request.NewBackgroundImage.FileName;
                string newPath = Path.Combine(_env.WebRootPath, "assets/img", newFileName);
                await request.NewBackgroundImage.SaveFileToLocalAsync(newPath);

                string oldPath = Path.Combine(_env.WebRootPath, "assets/img", existCity.BackgroundImage);
                oldPath.DeleteFileFromLocal();

                request.ExistBackgroundImage = newFileName;

            }

            if (request.NewDestinationImages is not null)
            {
                foreach (var item in request.NewDestinationImages)
                {
                    if (!item.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("NewProductImages", "File type must be image");
                        request.ExistDestinationImages = existCity.DestinationImages.ToList();
                        return View(request);
                    }

                    if (!item.CheckFileSize(2))
                    {
                        ModelState.AddModelError("NewProductImages", "Image size must be less than 2");
                        request.ExistDestinationImages = existCity.DestinationImages.ToList();
                        return View(request);
                    }
                }

                foreach (var item in request.NewDestinationImages)
                {
                    string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;

                    string path = Path.Combine(_env.WebRootPath, "assets/img", fileName);

                    await item.SaveFileToLocalAsync(path);

                    images.Add(new DestinationImage
                    {
                        Image = fileName
                    });
                }
            }


            DestinationCity city = new()
            {
                AccommodationDay = request.AccommodationDay,
                BackgroundImage = request.ExistBackgroundImage,
                CountryId = request.CountryId,
                Departure = request.Departure,
                Description = request.Description,
                DestinationImages = images,
                MaxLimit = request.MaxLimit,
                MinLimit = request.MinLimit,
                Name = request.Name,
                Price = request.Price
            };

            await _cityService.Edit((int)id, city);
            return RedirectToAction(nameof(Index));
        }
    }
}
