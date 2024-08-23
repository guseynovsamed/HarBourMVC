using System;
using Domain.Models;

namespace Service.Service.Interfaces
{
	public interface IStaffService
	{
        Task<IEnumerable<Staff>> GetAll();
        Task<Staff> GetById(int id);
        Task Create(Staff staff);
        Task Edit(int id, Staff staff);
        Task Delete(Staff staff);
    }
}

