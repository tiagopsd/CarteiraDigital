using CarteiraDigital.Domain.Entities;
using CarteiraDigital.Domain.Models;
using CarteiraDigital.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraDigital.Domain.Validations
{
    public interface IUserValidation
    {
        IResult<UserModel> ValidateModel(UserModel model);
    }
}
