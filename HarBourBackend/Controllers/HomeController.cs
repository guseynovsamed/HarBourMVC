using System;
using Domain.Models;
using HarBourBackEnd.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Service;
using Service.Service.Interfaces;

namespace HarBourBackEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderVideoService _sliderVideoService;
        private readonly ISubscriberService _subscriberService;
        private readonly IYachtService _yachtService;
        private readonly IAboutService _aboutService;
        private readonly IYachtCategoryService _categoryService;
        private readonly IWaterSportService _sportService;
        private readonly IDestinationCityService _cityService;
        private readonly ICommentService _commentService;
        private readonly IBlogService _blogService;
        private readonly ISettingService _settingService;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(ISliderVideoService sliderVideoService,
            ISubscriberService subscriberService,
            IYachtService yachtService,
            IYachtCategoryService categoryService,
            IWaterSportService sportService,
            IDestinationCityService cityService,
            ICommentService commentService,
            IBlogService blogService,
            ISettingService settingService,
            UserManager<AppUser> userManager
            )
        {
            _sliderVideoService = sliderVideoService;
            _subscriberService = subscriberService;
            _yachtService = yachtService;
            _categoryService = categoryService;
            _sportService = sportService;
            _cityService = cityService;
            _commentService = commentService;
            _blogService = blogService;
            _settingService = settingService;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            var sliderVideos = await _sliderVideoService.GetAll();
            var subscribers = await _subscriberService.GetAll();
            var yachts = await _yachtService.GetAll();
            var categories = await _categoryService.GetAll();
            var sports = await _sportService.GetAll();
            var cities = await _cityService.GetAll();
            var comments = await _commentService.GetAll();
            var blogs = await _blogService.GetAll();

            HomeVM model = new()
            {
                SliderVideos = sliderVideos,
                Cities = cities,
                Yachts = yachts,
                WaterSports = sports,
                YachtCategories = categories,
                Comments = comments,
                Blogs = blogs,
                Subscribers = subscribers,
            };

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> Subscribe(string subscriberEmail)
        {
            await _subscriberService.Create(new Subscriber { SubscriberEmail = subscriberEmail });
            return Ok();
        }



        [Route("/StatusCodeError/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 404)
            {
                ViewBag.ErrorMessage = "Page could not be found !";
            }

            return View("Error");

        }
    }
}

