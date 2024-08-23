using System;
using Domain.Models;

namespace Service.Service.Interfaces
{
	public interface IAccountService
	{
        Task<IList<string>> GetRoles(AppUser user);
    }
}

