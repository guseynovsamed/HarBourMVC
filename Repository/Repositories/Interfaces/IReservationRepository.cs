using System;
using Domain.Models;

namespace Repository.Repositories.Interfaces
{
	public interface IReservationRepository : IBaseRepository<Reservation>
    {
        Task<List<Reservation>> GetAllWithIncludes();
    }
}

