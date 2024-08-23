using System;
using HarBourBackEnd.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Service.Service.Interfaces;

namespace HarBourBackEnd.ViewComponents
{
	public class FooterViewComponent : ViewComponent
	{
        private readonly ISettingService _settingService;
        public FooterViewComponent(ISettingService settingService)
        {
            _settingService = settingService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var setting = await _settingService.GetAll();

            Dictionary<string, string> values = new();

            foreach (KeyValuePair<int, Dictionary<string, string>> item in setting)
            {
                values.Add(item.Value["Key"], item.Value["Value"]);
            }

            FooterVM response = new()
            {
                Settings = values,
            };


            return await Task.FromResult(View(response));
        }
    }
}

