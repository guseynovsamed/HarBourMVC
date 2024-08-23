using System;
using Domain.Models;

namespace Service.Service.Interfaces
{
	public interface IDestinationCityService
	{
        Task<IEnumerable<DestinationCity>> GetAll();
        Task<DestinationCity> GetById(int id);
        Task Create(DestinationCity city);
        Task Edit(int id, DestinationCity city);
        Task Delete(DestinationCity city);
        Task DeleteImage(DestinationImage image);
        Task ChangeMainImage(int cityId, int id);
        Task<List<DestinationCity>> GetAllPaginatedDatas(int page, int take = 4);
        Task<int> GetCount();
        int GetPageCount(int count, int take);
    }
}

