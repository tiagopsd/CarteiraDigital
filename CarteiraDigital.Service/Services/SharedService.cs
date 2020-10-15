using CarteiraDigital.Domain.Entities;
using CarteiraDigital.Domain.Models;
using CarteiraDigital.Domain.Models.Interfaces;
using CarteiraDigital.Domain.Repositories;
using CarteiraDigital.Domain.Service;
using CarteiraDigital.Infrastructure;
using CarteiraDigital.Infrastructure.CrossCutting.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraDigital.Service.Validations
{
    public class SharedService : ISharedService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;
        public SharedService(IUserRepository userRepository, IAccountRepository accountRepository)
        {
            _userRepository = userRepository;
            _accountRepository = accountRepository;
        }

        public async Task<IResult<Account>> AccountValidate(string cpf, string password)
        {
            var result = await PasswordValidate(cpf, password);
            if (!result.Success)
                return Result<Account>.BuildError(result.Messages).LoggerError();

            var account = await _accountRepository.GetByUserId(result.Model.Id);
            if (account == null)
                return Result<Account>.BuildError("Conta não encontrada, favor entrar em contato com suporte técnico.")
                    .LoggerError();

            return Result<Account>.BuildSucess(account);
        }

        private async Task<IResult<User>> PasswordValidate(string cpf, string password)
        {
            var user = await _userRepository.GetByCpf(cpf.RemoveMaskCpf());
            if (user == null)
                return Result<User>.BuildError("Usuário não encontrado.").LoggerError();
            else if (user.Password != password.MD5Hash())
                return Result<User>.BuildError("Senha inválida.").LoggerError();

            return Result<User>.BuildSucess(user);
        }
    }
}
