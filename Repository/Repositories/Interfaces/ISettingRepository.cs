using System;
using Domain.Models;

namespace Repository.Repositories.Interfaces
{
	public interface ISettingRepository : IBaseRepository<Setting>
	{
        Task<Dictionary<int, Dictionary<string, string>>> GetAll();
        Task<Setting> GetById(int id);
        Task Create(Setting setting);
        Task Edit(int id, Setting setting);
    }
}

