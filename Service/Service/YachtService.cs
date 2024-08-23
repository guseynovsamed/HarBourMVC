using System;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Service.Service.Interfaces;

namespace Service.Service
{
    public class YachtService : IYachtService
    {
        private readonly IYachtRepository _yachtRepository;

        public YachtService(IYachtRepository yachtRepository)
        {
            _yachtRepository = yachtRepository;
        }


        public async Task ChangeMainImage(int yachtId, int id)
        {
            await _yachtRepository.ChangeMainImage(yachtId,id);
        }

        public async Task Create(Yacht yacht)
        {
            await _yachtRepository.Create(yacht);
        }

        public async Task Delete(Yacht yacht)
        {
            await _yachtRepository.Delete(yacht);
        }

        public async Task DeleteImage(YachtImage image)
        {
            await _yachtRepository.DeleteImage(image);
        }

        public async Task Edit(int id, Yacht yacht)
        {
            var existYacht = await _yachtRepository.GetById(id);

            existYacht.Build = yacht.Build;
            existYacht.YachtCategoryId = yacht.YachtCategoryId;
            existYacht.Description = yacht.Description;
            existYacht.Guest = yacht.Guest;
            existYacht.Information = yacht.Information;
            existYacht.Length = yacht.Length;
            existYacht.Name = yacht.Name;
            existYacht.Price = yacht.Price;
            existYacht.YachtImages = yacht.YachtImages;

            await _yachtRepository.Edit(existYacht);
        }

        public async Task<IEnumerable<Yacht>> GetAll()
        {
            return await _yachtRepository.GetAllWithIncludes();
        }

        public async Task<Yacht> GetById(int id)
        {
            return await _yachtRepository.GetByIdWithCategory(id);
        }
    }
}

