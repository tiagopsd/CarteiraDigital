using CarteiraDigital.Domain.Entities;
using CarteiraDigital.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraDigital.Domain.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> GetByUserId(long userId);
        Task<Account> GetByCpf(string cpf);
    }
}
