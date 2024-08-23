using System;
using Domain.Models;

namespace Service.Service.Interfaces
{
	public interface ISettingService
    {
        Task<Dictionary<int, Dictionary<string, string>>> GetAll();
        Task<Setting> GetById(int id);
        Task Create(Setting setting);
        Task Edit(int id, Setting setting);
        Task Delete(Setting setting);
    }
}

