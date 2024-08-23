using System;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Service.Interfaces;

namespace Service.Service
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountService(UserManager<AppUser> userManager,
                                 RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IList<string>> GetRoles(AppUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

    }
}

