using System;
using Domain.Models;

namespace Service.Service.Interfaces
{
	public interface IComplaintService
	{
        Task Create(ComplaintSuggest suggest);
        Task<IEnumerable<ComplaintSuggest>> GetAll();
        Task Delete(ComplaintSuggest suggest);
        Task<ComplaintSuggest> GetById(int id);
    }
}

