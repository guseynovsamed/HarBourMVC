using System;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Service.Service.Interfaces;

namespace Service.Service
{
	public class ComplaintService : IComplaintService
    {
        private readonly IComplaintRepository _complaintRepository;

        public ComplaintService(IComplaintRepository complaintRepository)
        {
            _complaintRepository = complaintRepository;
        }
        public async Task Create(ComplaintSuggest suggest)
        {
            await _complaintRepository.Create(suggest);
        }

        public async Task Delete(ComplaintSuggest suggest)
        {
            await _complaintRepository.Delete(suggest);
        }

        public async Task<IEnumerable<ComplaintSuggest>> GetAll()
        {
            return await _complaintRepository.GetAll();
        }

        public async Task<ComplaintSuggest> GetById(int id)
        {
            return await _complaintRepository.GetById(id);
        }
    }
}

