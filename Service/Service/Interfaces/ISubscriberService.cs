using System;
using Domain.Models;

namespace Service.Service.Interfaces
{
	public interface ISubscriberService
	{
        Task Create(Subscriber subscriber);
        Task Delete(Subscriber subscriber);
        Task<IEnumerable<Subscriber>> GetAll();
        Task<Subscriber> GetById(int id);
    }
}

