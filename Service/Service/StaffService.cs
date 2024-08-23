using System;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Service.Service.Interfaces;

namespace Service.Service
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;

        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public async Task Create(Staff staff)
        {
            await _staffRepository.Create(staff);
        }

        public async Task Delete(Staff staff)
        {
            await _staffRepository.Delete(staff);
        }

        public async Task Edit(int id, Staff staff)
        {
            var existStaff = await _staffRepository.GetById(id);

            existStaff.Fullname = staff.Fullname;
            existStaff.Description = staff.Description;
            existStaff.Awards = staff.Awards;
            existStaff.Image = staff.Image;
            existStaff.Education = staff.Education;
            existStaff.PositionId = staff.PositionId;
            existStaff.Phone = staff.Phone;
            existStaff.Mail = staff.Mail;
            existStaff.Biography = staff.Biography;

            await _staffRepository.Edit(existStaff);

        }

        public async Task<IEnumerable<Staff>> GetAll()
        {
            return await _staffRepository.GetAllWithInclude();
        }

        public async Task<Staff> GetById(int id)
        {
            return await _staffRepository.GetByIdWithPositions(id);
        }
    }
}

