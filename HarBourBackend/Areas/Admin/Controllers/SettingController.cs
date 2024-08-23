using System;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;
using HarBourBackEnd.ViewModels.Setting;

namespace HarBourBackEnd.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class SettingController : Controller
	{
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Dictionary<int, Dictionary<string, string>> datas = await _settingService.GetAll();
            return View(datas);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(SettingCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _settingService.Create(new Setting { Key = request.Key, Value = request.Value });
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var existSetting = await _settingService.GetById((int)id);

            if (existSetting is null) return NotFound();

            await _settingService.Delete(existSetting);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var existSetting = await _settingService.GetById((int)id);

            if (existSetting is null) return NotFound();

            SettingEditVM model = new() { Key = existSetting.Key, Value = existSetting.Value };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SettingEditVM model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id is null) return BadRequest();

            var existEdit = await _settingService.GetById((int)id);

            if (existEdit is null) return NotFound();

            await _settingService.Edit((int)id, new Setting { Key = model.Key, Value = model.Value });

            return RedirectToAction(nameof(Index));
        }
    }
}

