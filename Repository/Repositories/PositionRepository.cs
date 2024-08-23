using System;
using Domain.Models;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class PositionRepository : BaseRepository<Position>, IPositionRepository
    {
        public PositionRepository(AppDbContext context) :base(context) { }
     
    }
}

