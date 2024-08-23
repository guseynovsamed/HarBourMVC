using System;
using HarBourBackEnd.ViewModels.Blog;
using HarBourBackEnd.ViewModels.DestinationCity;
using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;

namespace HarBourBackEnd.Controllers
{
    public class DestinationDetailController : Controller
    {
        private readonly IDestinationCityService _cityService;

        public DestinationDetailController(IDestinationCityService cityService)
        {
            _cityService = cityService;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {
            if (id is null) return BadRequest();

            var existCity = await _cityService.GetById((int)id);

            if (existCity is null) return NotFound();


            CityDetailVM city = new()
            {
                Id = existCity.Id,
                AccommodationDay = existCity.AccommodationDay,
                BackgroundImage = existCity.BackgroundImage,
                Country = existCity.Country.Name,
                Name = existCity.Name,
                Departure = existCity.Departure,
                Description = existCity.Description,
                DestinationImages = existCity.DestinationImages,
                MaxLimit = existCity.MaxLimit,
                MinLimit = existCity.MinLimit,
                Price = existCity.Price

            };

            var cities = await _cityService.GetAll();


            CityDetailPageVM model = new()
            {
                DestinationCities = cities.ToList(),
                CityDetail=city
            };

            return View(model);
        }

    }
}

