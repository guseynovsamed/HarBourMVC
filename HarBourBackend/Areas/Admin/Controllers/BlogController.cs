using System;
using Domain.Models;
using HarBourBackEnd.Helpers.Enums;
using HarBourBackEnd.Helpers.Extentions;
using HarBourBackEnd.ViewModels.Blog;
using HarBourBackEnd.ViewModels.WaterSport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using Service.Service;
using Service.Service.Interfaces;

namespace HarBourBackEnd.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IWebHostEnvironment _env;


        public BlogController(IBlogService blogService, IWebHostEnvironment env)
        {
            _blogService = blogService;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var blog = await _blogService.GetAll();

            var model = blog.Select(m => new BlogVM{
                Title = m.Title,
                BackgroundImage = m.BackgroundImage,
                Content = m.Content,
                CreateDate = m.CreateDate,
                Id = m.Id,
                Image = m.Image,
                Description=m.Description
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
        public async Task<IActionResult> Create(BlogCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
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


            Blog model = new()
            {
                Title = request.Title,
                BackgroundImage = fileNameBack,
                Content = request.Content,
                CreateDate = request.CreateDate,
                Image = fileName,
                Description=request.Description
                
            };

            await _blogService.Create(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var blog = await _blogService.GetById((int)id);

            if (blog is null) return NotFound();


            BlogDetailVM model = new()
            {
                Title = blog.Title,
                BackgroundImage = blog.BackgroundImage,
                Content = blog.Content,
                CreateDate = blog.CreateDate,
                Image = blog.Image,
                Description=blog.Description
            };

            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var existBlog = await _blogService.GetById((int)id);

            if (existBlog is null) return NotFound();

            var pathBack = Path.Combine(_env.WebRootPath, "assets/img", existBlog.BackgroundImage);
            pathBack.DeleteFileFromLocal();

            var path = Path.Combine(_env.WebRootPath, "assets/img", existBlog.Image);
            pathBack.DeleteFileFromLocal();


            await _blogService.Delete(existBlog);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id is null) return BadRequest();

            var blog = await _blogService.GetById((int)id);

            if (blog is null) return NotFound();

            BlogEditVM model = new()
            {
                Title = blog.Title,
                ExistBackgroundImage = blog.BackgroundImage,
                Content = blog.Content,
                ExistImage = blog.Image,
                Description=blog.Description
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id , BlogEditVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id is null) return BadRequest();

            var existBlog = await _blogService.GetById((int)id);

            if (existBlog is null) return NotFound();


           

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

            string pathBack = Path.Combine(_env.WebRootPath, "Admin/assets/img", fileNameBack);

            await request.BackgroundImage.SaveFileToLocalAsync(pathBack);


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


            Blog blog = new()
            {
                Title = request.Title,
                BackgroundImage = fileNameBack,
                Content = request.Content,
                Image = fileName,
                Description=request.Description
            };

            await _blogService.Edit((int)id, blog);
            return RedirectToAction(nameof(Index));
        }

    }

}

