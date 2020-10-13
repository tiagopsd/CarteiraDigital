using CarteiraDigital.Domain.Entities;
using CarteiraDigital.Domain.Models;
using CarteiraDigital.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraDigital.Domain.Service
{
    public interface IUserService
    {
        Task<IResult<UserModel>> Register(UserModel userModel);
        Task<User> GetByCpf(string cpf);
    }
}
