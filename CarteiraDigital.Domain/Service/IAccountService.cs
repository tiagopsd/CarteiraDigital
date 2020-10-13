using CarteiraDigital.Domain.Entities;
using CarteiraDigital.Domain.Enumerators;
using CarteiraDigital.Domain.Models;
using CarteiraDigital.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CarteiraDigital.Domain.Service
{
    public interface IAccountService
    {
        Task<IResult<DepositModel>> Deposit(DepositModel depositModel);
        Task<IResult<WithdrawModel>> Withdraw(WithdrawModel withdrawModel);
        Task<IResult<TransferModel>> Transfer(TransferModel transferModel);
        Task<IResult<BalanceModel>> Balance(BalanceModel balanceModel);
    }
}
