using System;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _entities;

        public BaseRepository( AppDbContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public async Task Create(T entity)
        {
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _entities.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(T entity)
        {
            _entities.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _entities.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _entities.FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}

