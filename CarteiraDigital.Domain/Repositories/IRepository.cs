using CarteiraDigital.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraDigital.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        Task<TEntity> FindAsync(params object[] id);
        Task<int> SaveAsync();
        Task UpdateAsync(params TEntity[] entities);
        Task AddAsync(params TEntity[] entities);
    }
}
