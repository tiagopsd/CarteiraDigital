using CarteiraDigital.Domain.Entities;
using CarteiraDigital.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraDigital.Domain.Service
{
    public interface ISharedService
    {
        Task<IResult<Account>> AccountValidate(string cpf, string password);
    }
}
