using CarteiraDigital.Domain.Context;
using CarteiraDigital.Domain.Entities;
using CarteiraDigital.Domain.Models.Interfaces;
using CarteiraDigital.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraDigital.Infrastructure.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(IContext context) : base(context)
        {
        }

        public async Task<Account> GetByCpf(string cpf)
        {
            return await Set.Include(d => d.User).FirstOrDefaultAsync(d => d.User.Cpf == cpf);
        }

        public async Task<Account> GetByUserId(long userId)
        {
            return await Set.FirstOrDefaultAsync(d => d.UserId == userId);
        }
    }
}
