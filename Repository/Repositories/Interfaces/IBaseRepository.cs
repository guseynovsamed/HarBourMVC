using System;
using Domain.Common;

namespace Repository.Repositories.Interfaces
{
	public interface IBaseRepository <T> where T : BaseEntity
	{
		Task Create(T entity);
		Task Delete(T entity);
		Task Edit(T entity);
		Task<T> GetById(int id);
		Task<IEnumerable<T>> GetAll();
	}
}

