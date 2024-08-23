using System;
using Domain.Models;

namespace Repository.Repositories.Interfaces
{
	public interface IStaffRepository : IBaseRepository<Staff>
	{
        Task<Staff> GetByIdWithPositions(int id);
        Task<IEnumerable<Staff>> GetAllWithInclude();
    }
}

