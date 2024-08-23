using System;
using Domain.Models;

namespace Service.Service.Interfaces
{
	public interface IWaterSportService
	{
        Task<IEnumerable<WaterSport>> GetAll();
        Task<WaterSport> GetById(int id);
        Task Create(WaterSport sport);
        Task Edit(int id, WaterSport sport);
        Task Delete(WaterSport sport);
        Task DeleteImage(WaterSportImage image);
        Task ChangeMainImage(int sportId, int id);
    }
}

