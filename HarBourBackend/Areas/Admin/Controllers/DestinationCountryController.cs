using System;
using Domain.Models;
using HarBourBackEnd.Helpers.Enums;
using HarBourBackEnd.ViewModels.DestinationCountry;
using HarBourBackEnd.ViewModels.Position;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Service;
using Service.Service.Interfaces;

namespace HarBourBackEnd.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class DestinationCountryController : Controller
	{
		private readonly IDestinationCountryService _countryService;

		public DestinationCountryController(IDestinationCountryService countryService)
		{
			_countryService = countryService;
		}


		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var datas = await _countryService.GetAll();

			var model = datas.Select(m=> new CountryVM{ Id=m.Id, Name=m.Name}).ToList();

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
        public async Task<IActionResult> Create(CountryCreateVM request)
        {
            if (!ModelState.IsValid) return View();

            await _countryService.Create(new DestinationCountry { Name = request.Name });

            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var country = await _countryService.GetById((int)id);

            if (country is null) return NotFound();

            await _countryService.Delete(country);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var existCountry = await _countryService.GetById((int)id);

            if (existCountry is null) return NotFound();

            CountryEditVM model = new() { NewName = existCountry.Name };

            return View(model);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CountryEditVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id is null) return BadRequest();

            var country = await _countryService.GetById((int)id);

            if (country is null) return NotFound();

            await _countryService.Edit((int)id, new DestinationCountry { Name = request.NewName });

            return RedirectToAction(nameof(Index));
        }
    }
}

