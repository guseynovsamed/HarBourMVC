using System;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Service.Service.Interfaces;

namespace Service.Service
{
    public class DestinationCountryService : IDestinationCountryService
    {
        private readonly IDestinationCountryRepository _countryRepository;


        public DestinationCountryService(IDestinationCountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task Create(DestinationCountry country)
        {
            await _countryRepository.Create(country);
        }

        public async Task Delete(DestinationCountry country)
        {
            await _countryRepository.Delete(country);
        }

        public async Task Edit(int id, DestinationCountry country)
        {
            var existCountry = await _countryRepository.GetById(id);

            existCountry.Name = country.Name;
            existCountry.Cities = country.Cities;

            await _countryRepository.Edit(existCountry);
        }

        public async Task<IEnumerable<DestinationCountry>> GetAll()
        {
            return await _countryRepository.GetAll();
        }

        public async Task<DestinationCountry> GetById(int id)
        {
            return await _countryRepository.GetById(id);
        }
    }
}

