using AutoMapper;
using CarteiraDigital.Domain.Entities;
using CarteiraDigital.Domain.Enumerators;
using CarteiraDigital.Domain.Models;
using CarteiraDigital.Domain.Models.Interfaces;
using CarteiraDigital.Domain.Repositories;
using CarteiraDigital.Domain.Service;
using CarteiraDigital.Domain.Validations;
using CarteiraDigital.Infrastructure;
using CarteiraDigital.Infrastructure.CrossCutting.Utils;
using System;
using System.Threading.Tasks;

namespace CarteiraDigital.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserValidation _validation;
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMovementService _movementService;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository,
            IAccountRepository accountRepository,
            IMovementService movementService,
            IMapper mapper,
            IUserValidation validation)
        {
            _mapper = mapper;
            _validation = validation;
            _userRepository = userRepository;
            _accountRepository = accountRepository;
            _movementService = movementService;
        }

        public async Task<IResult<UserModel>> Register(UserModel userModel)
        {
            try
            {
                var result = _validation.ValidateModel(userModel);
                if (!result.Success)
                    return result.LoggerError();

                userModel.Password = userModel.Password.MD5Hash();

                var user = _mapper.Map<User>(userModel);
                user.Cpf = user.Cpf.RemoveMaskCpf();

                await _userRepository.AddAsync(user);
                var account = await CreateAccount(user);

                await _movementService.CreateMovement(0, MovementType.Create, account, 0);

                await _userRepository.SaveAsync();

                return Result<UserModel>.BuildSucess(userModel, "Cadastro realizado com sucesso!");
            }
            catch (Exception error)
            {
                return Result<UserModel>.BuildError("Error ao realizar cadastro!", error).LoggerError();
            }
        }

        public async Task<Account> CreateAccount(User user)
        {
            var account = new Account
            {
                Balance = 0,
                User = user
            };

            await _accountRepository.AddAsync(account);
            return account;
        }

        public async Task<User> GetByCpf(string cpf)
        {
            return await _userRepository.GetByCpf(cpf.RemoveMaskCpf());
        }
    }
}
