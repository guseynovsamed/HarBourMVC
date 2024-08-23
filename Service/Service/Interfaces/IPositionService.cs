using System;
using Domain.Models;

namespace Service.Service.Interfaces
{
	public interface IPositionService
	{
        Task<IEnumerable<Position>> GetAll();
        Task<Position> GetById(int id);
        Task Create(Position position);
        Task Edit(int id, Position position);
        Task Delete(Position position);
    }
}

