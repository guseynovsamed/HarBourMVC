using System;
using Domain.Models;

namespace Service.Service.Interfaces
{
	public interface IYachtCategoryService
	{
        Task<IEnumerable<YachtCategory>> GetAll();
        Task<YachtCategory> GetById(int id);
        Task Create(YachtCategory yachtCategory);
        Task Edit(int id, YachtCategory yachtCategory);
        Task Delete(YachtCategory yachtCategory);
    }
}

