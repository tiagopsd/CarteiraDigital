using CarteiraDigital.Domain.Entities;
using CarteiraDigital.Domain.Enumerators;
using CarteiraDigital.Domain.Models;
using CarteiraDigital.Domain.Models.Interfaces;
using CarteiraDigital.Domain.Repositories;
using CarteiraDigital.Domain.Service;
using CarteiraDigital.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraDigital.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMovementService _movementService;
        private readonly ISharedService _sharedService;

        public AccountService(IAccountRepository accountRepository, IMovementService movementService,
            ISharedService sharedService)
        {
            _accountRepository = accountRepository;
            _movementService = movementService;
            _sharedService = sharedService;
        }

        public async Task<IResult<DepositModel>> Deposit(DepositModel movementModel)
        {
            try
            {
                var account = await _sharedService.AccountValidate(movementModel.Cpf, movementModel.Password);
                if (!account.Success)
                    return Result<DepositModel>.BuildError(account.Messages);

                var result = new Result<DepositModel>();
                var exist = await _movementService.ExistDeposit(account.Model.Id);

                decimal bonus = 0;
                if (!exist)
                {
                    bonus = movementModel.Amount.GetPercentage(10);
                    result.AddMessage("Parábens você acabou de realizar seu primeiro depósito," +
                        $" você recebeu 10% do valor a título de boas vindas!");
                }
                account.Model.Balance += (movementModel.Amount + bonus);
                await _movementService.CreateMovement(movementModel.Amount, MovementType.Deposit,
                    account.Model, account.Model.Balance, bonus: bonus);

                await _accountRepository.SaveAsync();
                return result.AddSuccess(movementModel, "Depósito realizado com sucesso!");
            }
            catch (Exception error)
            {
                return Result<DepositModel>.BuildError("Erro ao realizar desosito, favor tente novamento", error)
                    .LoggerError();
            }
        }

        public async Task<IResult<WithdrawModel>> Withdraw(WithdrawModel movementModel)
        {
            try
            {
                var account = await _sharedService.AccountValidate(movementModel.Cpf, movementModel.Password);
                if (!account.Success)
                    return Result<WithdrawModel>.BuildError(account.Messages);

                var tax = movementModel.Amount.GetPercentage(1);

                if (account.Model.Balance < movementModel.Amount + tax)
                    return Result<WithdrawModel>.BuildError($"Saldo insuficiente, " +
                        $"seu saldo permitido para transaferência é {account.Model.Balance - tax}");

                account.Model.Balance -= (movementModel.Amount + tax);
                await _movementService.CreateMovement(movementModel.Amount, MovementType.Withdraw, account.Model,
                    account.Model.Balance, tax: tax);

                await _accountRepository.SaveAsync();

                return Result<WithdrawModel>.BuildSucess(movementModel, "Saque realizado com sucesso!");
            }
            catch (Exception error)
            {
                return Result<WithdrawModel>.BuildError("Erro ao realizar saque, favor tente novamento", error);
            }
        }

        public async Task<IResult<TransferModel>> Transfer(TransferModel transferModel)
        {
            try
            {
                var account = await _sharedService.AccountValidate(transferModel.Cpf, transferModel.Password);
                if (!account.Success)
                    return Result<TransferModel>.BuildError(account.Messages);

                if (account.Model.Balance < transferModel.Amount)
                    return Result<TransferModel>.BuildError($"Saldo insuficiente, seu saldo atual é {account.Model.Balance}");

                var destinationAccount = await _accountRepository.GetByCpf(transferModel.DestinationCpf);
                if (destinationAccount == null)
                    return Result<TransferModel>.BuildError("Conta do destinatário não encontrado, favor refazer a transação");

                account.Model.Balance -= transferModel.Amount;
                destinationAccount.Balance += transferModel.Amount;

                await _movementService.CreateMovement(transferModel.Amount, MovementType.TransferDebit, account.Model,
                   account.Model.Balance);

                await _movementService.CreateMovement(transferModel.Amount, MovementType.TransferCredit, destinationAccount,
                  destinationAccount.Balance);

                await _accountRepository.SaveAsync();

                return Result<TransferModel>.BuildSucess(transferModel);
            }
            catch (Exception error)
            {
                return Result<TransferModel>.BuildError("Erro ao realizar a transferência, favor tente novamento", error);
            }
        }

        public async Task<IResult<BalanceModel>> Balance(BalanceModel balanceModel)
        {
            try
            {
                var account = await _sharedService.AccountValidate(balanceModel.Cpf, balanceModel.Password);
                if (!account.Success)
                    return Result<BalanceModel>.BuildError(account.Messages);

                balanceModel.Balance = account.Model.Balance;
                return Result<BalanceModel>.BuildSucess(balanceModel, "Consulta realizada com sucesso.");
            }
            catch (Exception error)
            {
                return Result<BalanceModel>.BuildError("Ocorreu algum erro, tente novamente.", error);
            }
        }
    }
}
