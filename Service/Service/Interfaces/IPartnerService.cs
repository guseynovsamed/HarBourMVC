using System;
using Domain.Models;

namespace Service.Service.Interfaces
{
	public interface IPartnerService
	{
        Task<IEnumerable<Partner>> GetAll();
        Task<Partner> GetById(int id);
        Task Create(Partner partner);
        Task Edit(int id, Partner partner);
        Task Delete(Partner partner);
    }
}

