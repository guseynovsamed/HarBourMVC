using System;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Service.Service.Interfaces;

namespace Service.Service
{
    public class DestinationCityService : IDestinationCityService
    {
        private readonly IDestinationCityRepository _cityRepository;

        public DestinationCityService(IDestinationCityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }



        public async Task ChangeMainImage(int cityId, int id)
        {
            await _cityRepository.ChangeMainImage(cityId, id);
        }

        public async Task Create(DestinationCity city)
        {
            await _cityRepository.Create(city);
        }

        public async Task Delete(DestinationCity city)
        {
            await _cityRepository.Delete(city);
        }

        public async Task DeleteImage(DestinationImage image)
        {
            await _cityRepository.DeleteImage(image);
        }

        public async Task Edit(int id, DestinationCity city)
        {
            var existCity = await _cityRepository.GetById(id);

            existCity.AccommodationDay = city.AccommodationDay;
            existCity.CountryId = city.CountryId;
            existCity.Departure = city.Departure;
            existCity.Description = city.Description;
            existCity.MaxLimit = city.MaxLimit;
            existCity.MinLimit = city.MinLimit;
            existCity.Name = city.Name;
            existCity.Price = city.Price;
            existCity.DestinationImages = city.DestinationImages;
            existCity.BackgroundImage = city.BackgroundImage;

            await _cityRepository.Edit(existCity);
        }

        public async Task<IEnumerable<DestinationCity>> GetAll()
        {
            return await _cityRepository.GetAllWithIncludes();
        }

        public async Task<List<DestinationCity>> GetAllPaginatedDatas(int page, int take = 4)
        {
            return await _cityRepository.GetAllPaginatedDatas(page, take);
        }

        public async Task<DestinationCity> GetById(int id)
        {
            return await _cityRepository.GetByIdWithCountry(id);
        }

        public async Task<int> GetCount()
        {
            return await _cityRepository.GetCount();
        }

        public int GetPageCount(int count, int take)
        {
            return (int)Math.Ceiling((decimal)count / take);
        }
    }
}

