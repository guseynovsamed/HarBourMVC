using System;
using HarBourBackEnd.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;

namespace HarBourBackEnd.Controllers
{
	public class WaterSportController : Controller
    {

        private readonly IWaterSportService _sportService;

		public WaterSportController(IWaterSportService sportService)
		{
            _sportService = sportService;
		}


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var sport = await _sportService.GetAll();

            WaterSportPageVM model = new()
            {
                WaterSports = sport
            };

            return View(model);
        }
    }
}

