using System;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Service.Service.Interfaces;

namespace Service.Service
{
    public class PartnerService : IPartnerService
    {
        private readonly IPartnerRepository _partnerRepository;

        public PartnerService(IPartnerRepository partnerRepository)
        {
            _partnerRepository = partnerRepository;
        }

        public async Task Create(Partner partner)
        {
            await _partnerRepository.Create(partner);
        }

        public async Task Delete(Partner partner)
        {
           await _partnerRepository.Delete(partner);
        }

        public async Task Edit(int id, Partner partner)
        {
            var existPartner = await GetById(id);

            existPartner.Logo = partner.Logo;

            await _partnerRepository.Edit(existPartner);
        }

        public async Task<IEnumerable<Partner>> GetAll()
        {
            return await _partnerRepository.GetAll();
        }

        public async Task<Partner> GetById(int id)
        {
            return await _partnerRepository.GetById(id);
        }
    }
}

