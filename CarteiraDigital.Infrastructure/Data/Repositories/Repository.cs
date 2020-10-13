using CarteiraDigital.Domain.Context;
using CarteiraDigital.Domain.Entities.Interfaces;
using CarteiraDigital.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CarteiraDigital.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        public DbSet<TEntity> Set { get; set; }

        public readonly Context _context;
        public Repository(IContext context)
        {
            _context = (Context)context;
            Set = _context.Set<TEntity>();
        }

        public async Task<TEntity> FindAsync(params object[] id)
        {
            try
            {
                return await Set.FindAsync(id);
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateAsync(params TEntity[] entities)
        {
            try
            {
                await Task.Run(() => Set.UpdateRange(entities));
            }
            catch
            {
                throw;
            }
        }

        public async Task AddAsync(params TEntity[] entities)
        {
            try
            {
                await Set.AddRangeAsync(entities);
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
