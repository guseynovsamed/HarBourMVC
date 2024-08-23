using System;
using Domain.Models;
using HarBourBackEnd.ViewModels;
using HarBourBackEnd.ViewModels.Blog;
using HarBourBackEnd.ViewModels.Comment;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Service.Interfaces;

namespace HarBourBackEnd.Controllers
{
    public class BlogController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IBlogService _blogService;
        private readonly UserManager<AppUser> _userManager;

        public BlogController(ICommentService commentService
            , IBlogService blogService
            , UserManager<AppUser> userManager)
        {
            _commentService = commentService;
            _blogService = blogService;
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Blog> blogs = await _blogService.GetAll();
            ViewBag.count = blogs.Count();

            BlogPageVM model = new()
            {
                Blogs = blogs.Take(2).ToList()
            };


            return View(model);
        }



        [HttpGet]
        public async Task<IActionResult> ShowMore(int skip)
        {
            IEnumerable<Blog> blogs = await _blogService.GetAll();

            var blogsSkip = blogs.Skip(skip).Take(2).ToList();

            BlogPageVM model = new() { Blogs = blogsSkip };

            return PartialView("_BlogPartialView", model);

        }


        public async Task<IActionResult> Search(string searchText)
        {
            IEnumerable<Blog> blogs = await _blogService.GetAll();

            blogs = searchText != null
                ? blogs.Where(m => m.Title.ToLower().Contains(searchText.ToLower()))
                : blogs.Take(2);

            BlogPageVM model = new() { Blogs = blogs.ToList() };

            return PartialView("_BlogPartialView", model);
        }


    }
}

