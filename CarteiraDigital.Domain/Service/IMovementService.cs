using CarteiraDigital.Domain.Entities;
using CarteiraDigital.Domain.Enumerators;
using CarteiraDigital.Domain.Models;
using CarteiraDigital.Domain.Models.Interfaces;
using CarteiraDigital.Domain.Models.Movement;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraDigital.Domain.Service
{
    public interface IMovementService
    {
        Task CreateMovement(decimal amount, MovementType movementType, Account account, decimal balance,
            decimal tax = 0, decimal bonus = 0);
        Task<bool> ExistDeposit(long accountId);
        Task<IResult<HistoryModel>> History(FilterMovementModel filter);
    }
}
