using System;
using Domain.Models;
using HarBourBackEnd.Helpers.Enums;
using HarBourBackEnd.ViewModels.Partner;
using HarBourBackEnd.ViewModels.Position;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Service;
using Service.Service.Interfaces;

namespace HarBourBackEnd.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class PositionController : Controller
	{
		private readonly IPositionService _positionService;
        private readonly IStaffService _staffService;

		public PositionController(IPositionService positionService, IStaffService staffService)
		{
			_positionService = positionService;
            _staffService = staffService;
		}


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var datas = await _positionService.GetAll();

            var staff = await _staffService.GetAll();

            int countStaf = staff.Count();


            var model = datas.Select(m => new PositionVM { Id = m.Id, Name=m.Name }).ToList();

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
        public async Task<IActionResult> Create(PositionCreateVM request)
        {
            if (!ModelState.IsValid) return View();

            await _positionService.Create(new Position { Name = request.Name });

            return RedirectToAction(nameof(Index));

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var category = await _positionService.GetById((int)id);

            if (category is null) return NotFound();

            await _positionService.Delete(category);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var existPosition = await _positionService.GetById((int)id);

            if (existPosition is null) return NotFound();

            PositionEditVM model = new() { NewName = existPosition.Name };

            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, PositionEditVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id is null) return BadRequest();

            var position = await _positionService.GetById((int)id);

            if (position is null) return NotFound();

            await _positionService.Edit((int)id, new Position { Name = request.NewName });

            return RedirectToAction(nameof(Index));
        }
    }
}

