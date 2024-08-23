using System;
using Domain.Models;

namespace Service.Service.Interfaces
{
	public interface IDestinationCountryService
	{
        Task<IEnumerable<DestinationCountry>> GetAll();
        Task<DestinationCountry> GetById(int id);
        Task Create(DestinationCountry country);
        Task Edit(int id, DestinationCountry country);
        Task Delete(DestinationCountry country);
    }
}

