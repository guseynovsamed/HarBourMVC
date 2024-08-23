using System;
using System.Collections.Generic;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Service.Service.Interfaces;

namespace Service.Service
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _rezrepository;

        public ReservationService(IReservationRepository rezreservation)
        {
            _rezrepository = rezreservation;
        }

        public async Task Create(Reservation rez)
        {
            await _rezrepository.Create(rez);
        }

        public async Task Delete(Reservation rez)
        {
            await _rezrepository.Delete(rez);
        }

        public async Task Edit(int id, Reservation rez)
        {
            var existRez = await _rezrepository.GetById(id);

            existRez.AppUser = rez.AppUser;
            existRez.Guests = rez.Guests;
            existRez.EndDate = rez.EndDate;
            existRez.StartDate = rez.StartDate;
            existRez.Yacht = rez.Yacht;

            await _rezrepository.Edit(existRez);
        }

        public async Task<List<Reservation>> GetAll()
        {
            var datas = await _rezrepository.GetAllWithIncludes();

            return datas.ToList();
        }

        public async Task<Reservation> GetById(int id)
        {
            return await _rezrepository.GetById(id);
        }
    }
}

