using System;
using HarBourBackEnd.ViewModels;
using HarBourBackEnd.ViewModels.Yacht;
using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;

namespace HarBourBackEnd.Controllers
{
	public class YachtController : Controller
    {
        private readonly IYachtService _yachtService;


		public YachtController(IYachtService yachtService)
		{
            _yachtService = yachtService;
		}

         
        [HttpGet]
        public async Task<IActionResult> Index(int? categoryId)
        {

            var yacht = await _yachtService.GetAll();

            if (categoryId is not null)
            {
                yacht = yacht.Where(m => m.YachtCategoryId == (int)categoryId).ToList();
            }

            YachtPageVM model = new()
            {
                Yachts = yacht
            };

            return View(model);
        }



        [HttpGet]
        public async Task<IActionResult> Sorting(string sort)
        {
            var yacht = await _yachtService.GetAll();

            switch (sort)
            {
                case "A to Z":
                    yacht = yacht.OrderBy(m => m.Name);
                    break;
                case "Z to A":
                    yacht = yacht.OrderByDescending(m => m.Name);
                    break;
                case "Old to New":
                    yacht = yacht.OrderBy(m => m.Id);
                    break;
                case "Cheap to Expensive":
                    yacht = yacht.OrderBy(m => m.Price);
                    break;
                case "Expensive to Cheap":
                    yacht = yacht.OrderByDescending(m => m.Price);
                    break;
                default:
                    break;
            }

            YachtPageVM model = new() { Yachts = yacht };

            return PartialView("_YachtPartialView", model);
        }
    }
}

