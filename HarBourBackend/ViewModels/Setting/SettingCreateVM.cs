using System;
using System.ComponentModel.DataAnnotations;

namespace HarBourBackEnd.ViewModels.Setting
{
	public class SettingCreateVM
	{
        [Required]
        public string Key { get; set; }
        [Required]
        public string Value { get; set; }
    }
}

