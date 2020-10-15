using AutoMapper;
using CarteiraDigital.Domain.Entities;
using CarteiraDigital.Domain.Enumerators;
using CarteiraDigital.Domain.Models;
using CarteiraDigital.Domain.Models.Interfaces;
using CarteiraDigital.Domain.Models.Movement;
using CarteiraDigital.Domain.Repositories;
using CarteiraDigital.Domain.Service;
using CarteiraDigital.Domain.Validations;
using CarteiraDigital.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraDigital.Service.Services
{
    public class MovementService : IMovementService
    {
        private readonly IMovementRepository _movementRepository;
        private readonly ISharedService _sharedService;

        public MovementService(IMovementRepository movementRepository,
            ISharedService sharedService)
        {
            _movementRepository = movementRepository;
            _sharedService = sharedService;
        }

        public async Task CreateMovement(decimal amount, MovementType movementType, Account account, decimal balance,
            decimal tax = 0, decimal bonus = 0)
        {
            var movement = new Movement
            {
                Type = movementType,
                Amount = amount,
                Tax = tax,
                Bonus = bonus,
                Balance = balance,
                DateTime = DateTime.Now,
                Account = account
            };

            await _movementRepository.AddAsync(movement);
        }

        public async Task<IResult<HistoryModel>> History(FilterMovementModel filter)
        {
            try
            {
                var account = await _sharedService.AccountValidate(filter.Cpf, filter.Password);
                if (!account.Success)
                    return Result<HistoryModel>.BuildError(account.Messages).LoggerError();

                var movements = await _movementRepository.GetAccountHistory(account.Model.Id, filter.StartDate, filter.EndDate);
                if (movements == null || !movements.Any())
                    return Result<HistoryModel>.BuildError("Você não possui movimentos na conta nesse período.").LoggerError();

                return Result<HistoryModel>.BuildSucess(BuildHistoryModel(account.Model, movements));
            }
            catch (Exception error)
            {
                return Result<HistoryModel>.BuildError("Erro ao obter histórico da conta, favor tente novamente!", error)
                     .LoggerError();
            }
        }

        public HistoryModel BuildHistoryModel(Account account, List<Movement> movements)
        {
            var initialBalance = movements.OrderBy(d => d.DateTime).FirstOrDefault().Balance;
            var FinalBalance = movements.OrderBy(d => d.DateTime).LastOrDefault().Balance;

            var historyModel = new HistoryModel
            {
                CurrentBalance = account.Balance,
                MovementModels = movements.OrderBy(d => d.DateTime).Select(d => new MovementModel
                {
                    Amount = d.Amount,
                    DateTime = d.DateTime,
                    Type = d.Type.ToString()
                }).ToList(),
                InitialBalance = initialBalance,
                FinalBalance = FinalBalance,
            };
            return historyModel;
        }

        public async Task<bool> ExistDeposit(long accountId)
        {
            return await _movementRepository.ExistDeposit(accountId);
        }
    }
}
