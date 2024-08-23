using System;
using Domain.Models;

namespace Service.Service.Interfaces
{
	public interface IYachtService
	{
        Task<IEnumerable<Yacht>> GetAll();
        Task<Yacht> GetById(int id);
        Task Create(Yacht yacht);
        Task Edit(int id, Yacht yacht);
        Task Delete(Yacht yacht);
        Task DeleteImage(YachtImage image);
        Task ChangeMainImage(int yachtId, int id);
    }
}

