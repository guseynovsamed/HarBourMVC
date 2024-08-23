using System;
using HarBourBackEnd.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;

namespace HarBourBackEnd.Controllers
{
	public class DestinationController : Controller
    {
        private readonly IDestinationCityService _cityService;


        public DestinationController(IDestinationCityService cityService)
        {
            _cityService = cityService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var city = await _cityService.GetAll();

            DestinationPageVM model = new()
            {
                DestinationCities = city
            };


            return View(model);
        }
    }
}

