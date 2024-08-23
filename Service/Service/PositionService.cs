using System;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Service.Service.Interfaces;

namespace Service.Service
{
    public class PositionService : IPositionService
    {
        private readonly IPositionRepository _positionRepository;

        public PositionService(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }

        public async Task Create(Position position)
        {
            await _positionRepository.Create(position);
        }

        public async Task Delete(Position position)
        {
            await _positionRepository.Delete(position);
        }

        public async Task Edit(int id, Position position)
        {
            var existPosition = await GetById(id);

            existPosition.Name = position.Name;

            await _positionRepository.Edit(existPosition);
        }

        public async Task<IEnumerable<Position>> GetAll()
        {
            return await _positionRepository.GetAll();

        }

        public async Task<Position> GetById(int id)
        {
            return await _positionRepository.GetById(id);
        }
    }
}

