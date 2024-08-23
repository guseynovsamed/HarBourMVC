using System;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(AppDbContext context) : base(context) { }
        public async Task<List<Reservation>> GetAllWithIncludes()
        {
            return await _entities.Include(m => m.Yacht).ToListAsync();
        }

    }
}

