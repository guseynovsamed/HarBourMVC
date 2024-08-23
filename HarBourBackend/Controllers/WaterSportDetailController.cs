using System;
using Domain.Models;
using HarBourBackEnd.ViewModels.WaterSport;
using HarBourBackEnd.ViewModels.Yacht;
using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;

namespace HarBourBackEnd.Controllers
{
    public class WaterSportDetailController : Controller
    {
        private readonly IWaterSportService _waterSport;

        public WaterSportDetailController(IWaterSportService waterSport)
        {
            _waterSport = waterSport;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {
            if (id is null) return BadRequest();

            var existSport = await _waterSport.GetById((int)id);

            if (existSport is null) return NotFound();

            WaterSportDetailVM sport = new()
            {
                Id = existSport.Id,
                BackgroundImage = existSport.BackgroundImage,
                Description = existSport.Description,
                Information = existSport.Information,
                Name = existSport.Name,
                Policy = existSport.Policy,
                Price = existSport.Price,
                SportImages = existSport.SportImages
            };

            var sports = await _waterSport.GetAll();

            WaterSportDetailPageVM model = new()
            {
                WaterSportDetail = sport,
                WaterSports = sports
            };

            return View(model);
        }
    }
}

