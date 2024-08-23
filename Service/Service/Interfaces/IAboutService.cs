using System;
using Domain.Models;

namespace Service.Service.Interfaces
{
	public interface IAboutService
	{
        Task<IEnumerable<About>> GetAll();
        Task<About> GetById(int id);
        Task Create(About about);
        Task Edit(int id, About about);
        Task Delete(About about);
        Task DeleteImage(AboutImage image);
        Task ChangeMainImage(int aboutId, int id);
    }
}

