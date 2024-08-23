using System;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.Service.Interfaces;

namespace Service.Service
{
    public class SettingService : ISettingService
    {
        private readonly ISettingRepository _settingRepository;

        public SettingService(ISettingRepository settingRepository )
        {
            _settingRepository = settingRepository;
        }


        public async Task Create(Setting setting)
        {
            await _settingRepository.Create(setting);
        }

        public async Task Delete(Setting setting)
        {
            await _settingRepository.Delete(setting);
        }

        public async Task Edit(int id, Setting setting)
        {
            await _settingRepository.Edit(id, setting);
        }

        public async Task<Dictionary<int, Dictionary<string, string>>> GetAll()
        {
            return await _settingRepository.GetAll();
        }

        public async Task<Setting> GetById(int id)
        {
            return await _settingRepository.GetById(id);
        }
    }
}

