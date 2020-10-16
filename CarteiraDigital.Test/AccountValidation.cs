using AutoMapper;
using CarteiraDigital.Domain.Entities;
using CarteiraDigital.Domain.Models;
using CarteiraDigital.Domain.Repositories;
using CarteiraDigital.Infrastructure;
using CarteiraDigital.Infrastructure.Repositories;
using CarteiraDigital.Service.Services;
using CarteiraDigital.Service.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CarteiraDigital.Test
{
    public class AccountValidation
    {
        private MovementService _movementService;
        private AccountService _accountService;
        private UserService _userService;
        private Context _contextFake;
        private IUserRepository _userRepository;
        private DataFake _dataFake;

        public AccountValidation()
        {
            _contextFake = new ContextFake().CreateContext();
            _dataFake = new DataFake();

            _userRepository = new UserRepository(_contextFake);
            var accountRepository = (IAccountRepository)new AccountRepository(_contextFake);
            var movementRepository = (IMovementRepository)new MovementRepository(_contextFake);
            var configurationRepository = (IConfigurationRepository)new ConfigurationRepository(_contextFake);

            var userValidation = new UserValidation(_userRepository, configurationRepository);
            var sharedService = new SharedService(_userRepository, accountRepository);
            IMapper mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserModel, User>();
            }).CreateMapper();

            _movementService = new MovementService(movementRepository, sharedService);
            _accountService = new AccountService(accountRepository, _movementService, sharedService);
            _userService = new UserService(_userRepository, accountRepository, _movementService, mapper, userValidation);
        }

        [Fact]
        public void ValidateValidTransfer()
        {
            RegisteUser("09030191082");
            RegisteUser("58962319039");

            _ = _accountService.Deposit(new DepositModel
            {
                Amount = 500,
                Cpf = "09030191082",
                Password = "12345678"
            }).Result;

            var result = _accountService.Transfer(new TransferModel
            {
                Amount = 125,
                Cpf = "09030191082",
                DestinationCpf = "58962319039",
                Password = "12345678"
            }).Result;
            Assert.True(result.Success);

            ValidateMovement(125, "09030191082");
            ValidateMovement(125, "58962319039");
        }

        [Fact]
        public void ValidateInvalidTransfer()
        {
            RegisteUser("89588184096");
            RegisteUser("82932525026");

            var transferModel = new TransferModel
            {
                Amount = 100,
                Cpf = "123",
                Password = "123",
                DestinationCpf = "82932525026"
            };

            //Validar cpf 
            var result = _accountService.Transfer(transferModel).Result;
            Assert.False(result.Success);

            //validar senha 
            transferModel.Cpf = "89588184096";
            var result2 = _accountService.Transfer(transferModel).Result;
            Assert.False(result2.Success);

            //validar saldo
            transferModel.Password = "12345678";
            var result3 = _accountService.Transfer(transferModel).Result;
            Assert.False(result3.Success);
        }

        [Fact]
        public void ValidateInvalidWithdraw()
        {
            //teste usuario e senha no saque
            var withdrawModel = _dataFake.GetWithdrawModel("123");
            withdrawModel.Amount = 100;
            withdrawModel.Password = "123";

            var result = _accountService.Withdraw(withdrawModel).Result;
            Assert.False(result.Success);

            RegisteUser("109.232.780-01");

            withdrawModel.Cpf = "109.232.780-01";
            var result2 = _accountService.Withdraw(withdrawModel).Result;
            Assert.False(result2.Success);
        }

        [Fact]
        public void ValidateInvalidWithdraw2()
        {
            var withdrawModel2 = _dataFake.GetWithdrawModel("09606592006");
            withdrawModel2.Amount = 100;

            RegisteUser("09606592006");

            //testa retirada sem saldo
            var result2 = _accountService.Withdraw(withdrawModel2).Result;
            Assert.False(result2.Success);
        }

        private void ValidateMovement(decimal amount, string cpf, decimal tax = 0, decimal bonus = 0)
        {
            var movement = _movementService.History(_dataFake.GetFilterMovement(cpf)).Result;

            var lastMovement = movement.Model.MovementModels
                .OrderByDescending(d => d.DateTime)
                .FirstOrDefault();

            Assert.Equal(amount, lastMovement.Amount);
            Assert.Equal(tax, lastMovement.Tax);
            Assert.Equal(bonus, lastMovement.Bonus);
        }

        [Fact]
        public void ValidateSucessWithdrawAndTaxWithMovement()
        {
            //testa taxa saque
            RegisteUser("68676183082");
            var depositModel = _dataFake.GetDepositModel("68676183082");
            depositModel.Amount = 150;

            _ = _accountService.Deposit(depositModel).Result;
            var withdrawModel = new WithdrawModel
            {
                Amount = 100,
                Cpf = "68676183082",
                Password = "12345678"
            };

            var tax = withdrawModel.Amount * (decimal)0.01;
            var result = _accountService.Withdraw(withdrawModel).Result;
            Assert.True(result.Success);
            Assert.Equal(tax, result.Model.Tax);

            ValidateMovement(withdrawModel.Amount, "68676183082", tax);
        }

        [Fact]
        public void ValidateErrorDeposit()
        {
            //teste usuario e senha no deposito
            var result = _accountService.Deposit(_dataFake.GetDepositModel("59781111038")).Result;
            Assert.False(result.Success);

            RegisteUser("59781111038");

            //testa valor invalido
            var result1 = _accountService.Deposit(_dataFake.GetDepositModel("59781111038")).Result;
            Assert.False(result1.Success);

            //limpa tabela de usuario
            CleanUserDataTable();
        }

        [Fact]
        public void ValidateFirstAndSecondDepositWithMoviment()
        {
            RegisteUser("79797121020");
            var deposit = _dataFake.GetDepositModel("79797121020");
            deposit.Amount = (decimal)183.53;

            var result = _accountService.Deposit(deposit).Result;
            Assert.True(result.Success);

            var balance = _accountService.Balance(_dataFake.GetBalanceUserModel2("79797121020")).Result;

            var bonus = deposit.Amount * (decimal)0.10;
            var total = deposit.Amount + bonus;

            Assert.Equal(balance.Model.Balance, total);

            var movement = _movementService.History(_dataFake.GetFilterMovement("79797121020")).Result;
            var firstMovement = movement.Model.MovementModels.Where(d => d.Amount > 0).First();

            Assert.Equal(movement.Model.CurrentBalance, total);
            Assert.Equal(firstMovement.Amount, deposit.Amount);
            Assert.Equal(firstMovement.Bonus, bonus);

            ValidateSencondDeposit("79797121020");
        }

        private void ValidateSencondDeposit(string cpf)
        {
            var deposit = _dataFake.GetDepositModel(cpf);
            deposit.Amount = (decimal)183.53;

            var result = _accountService.Deposit(deposit).Result;
            Assert.True(result.Success);

            var balance = _accountService.Balance(_dataFake.GetBalanceUserModel2(cpf)).Result;

            var bonus = deposit.Amount * (decimal)0.10;
            var total = deposit.Amount;

            Assert.Equal(balance.Model.Balance, total * 2 + bonus);

            var movement = _movementService.History(_dataFake.GetFilterMovement(cpf)).Result;

            var lastMovement = movement.Model.MovementModels.OrderByDescending(d => d.DateTime).First();

            Assert.Equal(movement.Model.CurrentBalance, total * 2 + bonus);
            Assert.Equal(lastMovement.Amount, deposit.Amount);
            Assert.Equal(0, lastMovement.Bonus);
        }

        private void CleanUserDataTable()
        {
            var users = _userRepository.GetAll().Result;
            users.ForEach(d => _userRepository.Delete(d));
            _contextFake.SaveChanges();
        }

        private void RegisteUser(string cpf)
        {
            var user = new DataFake().GetUserModel(cpf);
            user.Cpf = cpf;
            _ = _userService.Register(user).Result;
            _contextFake.SaveChanges();
        }
    }
}
