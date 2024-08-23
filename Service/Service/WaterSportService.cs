using System;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Service.Service.Interfaces;

namespace Service.Service
{
    public class WaterSportService : IWaterSportService
    {
        private readonly IWaterSportRepository _sportRepository;

        public WaterSportService(IWaterSportRepository sportRepository)
        {
            _sportRepository = sportRepository;
        }


        public async Task ChangeMainImage(int sportId, int id)
        {
           await _sportRepository.ChangeMainImage(sportId,id);
        }

        public async Task Create(WaterSport sport)
        {
            await _sportRepository.Create(sport);
        }

        public async Task Delete(WaterSport sport)
        {
            await _sportRepository.Delete(sport);
        }

        public async Task DeleteImage(WaterSportImage image)
        {
            await _sportRepository.DeleteImage(image);
        }

        public async Task Edit(int id, WaterSport sport)
        {
            var existSport = await _sportRepository.GetById(id);

            existSport.Name = sport.Name;
            existSport.Information = sport.Information;
            existSport.Description = sport.Description;
            existSport.BackgroundImage = sport.BackgroundImage;
            existSport.Price = sport.Price;
            existSport.SportImages = sport.SportImages;
            existSport.Policy = sport.Policy;

            await _sportRepository.Edit(existSport);
        }

        public async Task<IEnumerable<WaterSport>> GetAll()
        {
            return await _sportRepository.GetAllWithIncludes();
        }

        public async Task<WaterSport> GetById(int id)
        {
            return await _sportRepository.GetByIdWithImages(id);
        }
    }
}

