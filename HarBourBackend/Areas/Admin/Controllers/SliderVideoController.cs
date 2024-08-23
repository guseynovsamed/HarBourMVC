using System;
using System.Data;
using Domain.Models;
using HarBourBackEnd.Helpers.Extentions;
using HarBourBackEnd.ViewModels.Sliders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Service.Interfaces;

namespace HarBourBackEnd.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class SliderVideoController : Controller
    {
        private readonly ISliderVideoService _sliderVideoService;
        private readonly IWebHostEnvironment _env;

        public SliderVideoController(ISliderVideoService sliderVideoService, IWebHostEnvironment env)
        {
            _env = env;
            _sliderVideoService = sliderVideoService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var datas = await _sliderVideoService.GetAll();

            List<SliderVideoVM> model = datas.Select(m => new SliderVideoVM
            {
                Id = m.Id,
                Description = m.Description,
                MainTitle = m.MainTitle,
                TopTitle = m.TopTitle
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
        public async Task<IActionResult> Create(SliderVideoCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!request.Video.CheckFileType("video/mp4"))
            {
                ModelState.AddModelError("Video", "File type must be video");
                return View();
            }


            if (!request.Video.CheckFileSize(90))
            {
                ModelState.AddModelError("Video", "File size must be less than 90 Mb");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + "-" + request.Video.FileName;

            string path = Path.Combine(_env.WebRootPath, "assets/img", fileName);

            await request.Video.SaveFileToLocalAsync(path);

            await _sliderVideoService.Create(new SliderVideo
            {
                Video = fileName,
                TopTitle = request.TopTitle,
                MainTitle = request.MainTitle,
                Description = request.Description
            });

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            SliderVideo slider = await _sliderVideoService.GetById((int)id);
            if (slider is null) return NotFound();

            SliderVideoDetailVM sliderDetail = new()
            {
                Video = slider.Video,
                MainTitle = slider.MainTitle,
                TopTitle = slider.TopTitle,
                Description = slider.Description
            };

            return View(sliderDetail);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var existSlider = await _sliderVideoService.GetById((int)id);

            if (existSlider is null) return NotFound();

            string existImage = Path.Combine(_env.WebRootPath, "assets/img", existSlider.Video);

            existImage.DeleteFileFromLocal();

            await _sliderVideoService.Delete(existSlider);

            return RedirectToAction(nameof(Index));

        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var existSlider = await _sliderVideoService.GetById((int)id);

            if (existSlider is null) return NotFound();

            SliderVideoEditVM response = new()
            {
                TopTitle = existSlider.TopTitle,
                Description = existSlider.Description,
                MainTitle = existSlider.MainTitle,
                ExistVideo = existSlider.Video
            };

            return View(response);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SliderVideoEditVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id is null) return BadRequest();

            var existSlider = await _sliderVideoService.GetById((int)id);

            if (existSlider is null) return NotFound();

            if (request.NewVideo is not null)
            {
                if (!request.NewVideo.CheckFileType("video/mp4"))
                {
                    ModelState.AddModelError("NewVideo", "File type must be video");
                    request.ExistVideo = existSlider.Video;
                    return View(request);
                }

                if (!request.NewVideo.CheckFileSize(90))
                {
                    ModelState.AddModelError("NewVideo", "File size must be less than 90 Mb");
                    request.ExistVideo = existSlider.Video;
                    return View(request);
                }

                string oldPath = Path.Combine(_env.WebRootPath, "assets/img", existSlider.Video);
                oldPath.DeleteFileFromLocal();


                string newFileName = Guid.NewGuid().ToString() + "-" + request.NewVideo.FileName;
                string newPath = Path.Combine(_env.WebRootPath, "assets/img", newFileName);
                await request.NewVideo.SaveFileToLocalAsync(newPath);

                await _sliderVideoService.Edit((int)id, new SliderVideo { MainTitle = request.MainTitle, Description = request.Description, TopTitle = request.TopTitle, Video = newFileName });

            }
            else
            {
                await _sliderVideoService.Edit((int)id, new SliderVideo { MainTitle = request.MainTitle, Description = request.Description, TopTitle = request.TopTitle, Video = existSlider.Video });
            }


            return RedirectToAction(nameof(Index));

        }


    }
}

