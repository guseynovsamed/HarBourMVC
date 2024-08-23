using System;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Service.Service.Interfaces;

namespace Service.Service
{
    public class AboutService : IAboutService
    {
        private readonly IAboutRepository _aboutRepository;

        public AboutService(IAboutRepository aboutRepository)
        {
            _aboutRepository = aboutRepository;
        }

        public async Task ChangeMainImage(int aboutId, int id)
        {
            await _aboutRepository.ChangeMainImage(aboutId,id);
        }

        public async Task Create(About about)
        {
            await _aboutRepository.Create(about);
        }

        public async Task Delete(About about)
        {
            await _aboutRepository.Delete(about);
        }

        public async Task DeleteImage(AboutImage image)
        {
            await _aboutRepository.DeleteImage(image);
        }

        public async Task Edit(int id, About about)
        {
            var existAbout = await _aboutRepository.GetById(id);

            existAbout.AboutImages = about.AboutImages;
            existAbout.Advantage1 = about.Advantage1;
            existAbout.Advantage2 = about.Advantage2;
            existAbout.Advantage3 = about.Advantage3;
            existAbout.Description = about.Description;
            existAbout.Title = about.Title;

            await _aboutRepository.Edit(existAbout);
        }

        public async Task<IEnumerable<About>> GetAll()
        {
            return await _aboutRepository.GetAllWithImages();
        }


        public async Task<About> GetById(int id)
        {
            return await _aboutRepository.GetByIdWithImages(id);
        }

    }
}

