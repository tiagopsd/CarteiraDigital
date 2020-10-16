using CarteiraDigital.Domain.Context;
using CarteiraDigital.Domain.Entities;
using CarteiraDigital.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarteiraDigital.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IContext context) : base(context)
        {
        }

        public async Task<bool> ExistCpf(string cpf)
        {
            return await Set.AnyAsync(d => d.Cpf == cpf);
        }

        public async Task<User> GetByCpf(string cpf)
        {
            return await Set.FirstOrDefaultAsync(d => d.Cpf == cpf);
        }
    }
}
