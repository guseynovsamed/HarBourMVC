using System;
using Domain.Models;
using Repository.Repositories.Interfaces;

namespace Service.Service.Interfaces
{
    public class SliderVideoService : ISliderVideoService
    {
        private readonly ISliderVideoRepository _sliderVideoRepository;

        public SliderVideoService(ISliderVideoRepository sliderVideoRepository)
        {
            _sliderVideoRepository = sliderVideoRepository;
        }

        public async Task Create(SliderVideo slider)
        {
            await _sliderVideoRepository.Create(slider);
        }

        public async Task Delete(SliderVideo slider)
        {
            await _sliderVideoRepository.Delete(slider);
        }

        public async Task Edit(int id, SliderVideo slider)
        {
            var existSlider = await GetById(id);

            existSlider.Video = slider.Video;
            existSlider.TopTitle = slider.TopTitle;
            existSlider.MainTitle = slider.MainTitle;
            existSlider.Description = slider.Description;

            await _sliderVideoRepository.Edit(existSlider);
        }

        public async Task<IEnumerable<SliderVideo>> GetAll()
        {
            return await _sliderVideoRepository.GetAll();
        }

        public async Task<SliderVideo> GetById(int id)
        {
            return await _sliderVideoRepository.GetById(id);
        }
    }
}

