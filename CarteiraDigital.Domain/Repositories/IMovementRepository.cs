using CarteiraDigital.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraDigital.Domain.Repositories
{
    public interface IMovementRepository : IRepository<Movement>
    {
        Task<bool> ExistDeposit(long accountId);
        Task<List<Movement>> GetAccountHistory(long accountId, DateTime startDate, DateTime endDate);
    }
}
