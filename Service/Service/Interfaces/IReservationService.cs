using System;
using System.Collections.Generic;
using Domain.Models;

namespace Service.Service.Interfaces
{
	public interface IReservationService
	{
        Task Create(Reservation rez);
        Task<List<Reservation>> GetAll();
        Task Delete(Reservation rez);
        Task<Reservation> GetById(int id);
        Task Edit(int id, Reservation rez);
    }
}

