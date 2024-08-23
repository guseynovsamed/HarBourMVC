using System;
using Domain.Models;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Service.Interfaces;

namespace Service.Service
{
    public class YachtCategoryService : IYachtCategoryService
    {
        private readonly IYachtCategoryRepository _yachtCategoryRepository;

        public YachtCategoryService(IYachtCategoryRepository yachtCategoryRepository)
        {
            _yachtCategoryRepository = yachtCategoryRepository;
        }


        public async Task Create(YachtCategory yachtCategory)
        {
            await _yachtCategoryRepository.Create(yachtCategory);
        }

        public async Task Delete(YachtCategory yachtCategory)
        {
            await _yachtCategoryRepository.Delete(yachtCategory);
        }

        public async Task Edit(int id, YachtCategory yachtCategory)
        {
            var existCategory = await GetById(id);

            existCategory.Description = yachtCategory.Description;
            existCategory.Name = yachtCategory.Name;
            existCategory.IconImage = yachtCategory.IconImage;

            await _yachtCategoryRepository.Edit(existCategory);
        }

        public async Task<IEnumerable<YachtCategory>> GetAll()
        {
            return await _yachtCategoryRepository.GetAll();
        }

        public async Task<YachtCategory> GetById(int id)
        {
            return await _yachtCategoryRepository.GetById(id);
        }
    }
}

