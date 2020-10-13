using CarteiraDigital.Domain.Context;
using CarteiraDigital.Domain.Entities;
using CarteiraDigital.Domain.Enumerators;
using CarteiraDigital.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraDigital.Infrastructure.Repositories
{
    public class MovementRepository : Repository<Movement>, IMovementRepository
    {
        public MovementRepository(IContext context) : base(context)
        {
        }

        public async Task<bool> ExistDeposit(long accountId)
        {
            return await Set.AnyAsync(d => d.AccountId == accountId && d.Type == MovementType.Deposit);
        }

        public async Task<List<Movement>> GetAccountHistory(long accountId, DateTime startDate, DateTime endDate)
        {
            return await Set.Where(d => d.AccountId == accountId
                && d.DateTime.Date >= startDate.Date
                && d.DateTime.Date <= endDate.Date)
                .ToListAsync();
        }
    }
}
