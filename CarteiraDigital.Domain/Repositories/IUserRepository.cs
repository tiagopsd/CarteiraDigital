using CarteiraDigital.Domain.Entities;
using CarteiraDigital.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraDigital.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> ExistCpf(string cpf);
        Task<User> GetByCpf(string cpf);
    }
}
