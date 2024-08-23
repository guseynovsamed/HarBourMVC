using System;
using Domain.Models;
using HarBourBackEnd.ViewModels.Blog;
using HarBourBackEnd.ViewModels.Comment;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Service.Interfaces;

namespace HarBourBackEnd.Controllers
{
    public class BlogDetailController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IBlogService _blogService;
        private readonly UserManager<AppUser> _userManager;

        public BlogDetailController(ICommentService commentService
            , IBlogService blogService
            , UserManager<AppUser> userManager)
        {
            _commentService = commentService;
            _blogService = blogService;
            _userManager = userManager;
        }



        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {
            if (id is null) return BadRequest();

            var existBlog = await _blogService.GetById((int)id);

            if (existBlog is null) return NotFound();

            BlogDetailVM blog = new()
            {
                Id = existBlog.Id,
                BackgroundImage = existBlog.BackgroundImage,
                Content = existBlog.Content,
                CreateDate = existBlog.CreateDate,
                Image = existBlog.Image,
                Title = existBlog.Title,
                Description=existBlog.Description,
            };


            var blogs = await _blogService.GetAll();
            AppUser user = new();

            if (User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
            }


            CommentVM commentDatas = new()
            {
                UserId = user.Id,
                UserName = user.UserName,
                UserEmail=user.Email,
                BlogId=existBlog.Id
            };

            BlogDetailPageVM model = new()
            {
                Blog = blog,
                Blogs = blogs.ToList(),
                CommentData = commentDatas,
                BlogComments = existBlog.Comments
            };


            return View(model);

        }





        [HttpPost]
        public async Task<IActionResult> AddComment(string userId, int blogId, string comment)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Problem();
            }

            var user = await _userManager.FindByIdAsync(userId);
                

            var newComment = new Comment
            {
                BlogId = blogId,
                UserId = userId,
                CommentText = comment,
                CreateDate = DateTime.Now
            };

            await _commentService.Create(newComment);

            var commentData = new
            {
                UserFullName = user.FullName,
                CreateDate = newComment.CreateDate.ToString("dd MM yyyy"),
                TextComment = newComment.CommentText
            };

            return Json(commentData);
        }



        //[HttpPost]
        //public async Task<IActionResult> AddComment(string userId, int blogId, string comment)
        //{
        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        return Problem();
        //    }

        //    await _commentService.Create(new Comment { BlogId = blogId, UserId = userId, CommentText = comment });
        //    return Ok();
        //}




        //public async Task<IActionResult> AddComment(int blogId, string comment)
        //{
        //    if (!User.Identity.IsAuthenticated)
        //        return Json(new { redirectUrl = Url.Action("Login", "Account") });

        //    AppUser existUser = new();
        //    existUser = await _userManager.FindByNameAsync(User.Identity.Name);

        //    Review newReview = new()
        //    {
        //        Message = comment,
        //        ProductId = blogId,
        //        AppUserId = existUser.Id
        //    };

        //    await _context.Reviews.AddAsync(newReview);
        //    await _context.SaveChangesAsync();

        //    return PartialView("_ReviewPartial", newReview);
        //}
    }
}

