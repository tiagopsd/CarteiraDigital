using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarteiraDigital.Domain.Entities;
using CarteiraDigital.Domain.Enumerators;
using CarteiraDigital.Domain.Models;
using CarteiraDigital.Domain.Models.Interfaces;
using CarteiraDigital.Domain.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarteiraDigital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost, Route("Deposit")]
        public async Task<IResult<DepositModel>> Deposit(DepositModel depositModel)
        {
            return await _accountService.Deposit(depositModel);
        }

        [HttpPost, Route("Withdraw")]
        public async Task<IResult<WithdrawModel>> Withdraw(WithdrawModel withdrawModel)
        {
            return await _accountService.Withdraw(withdrawModel);
        }

        [HttpPost, Route("Transfer")]
        public async Task<IResult<TransferModel>> Transfer(TransferModel transferModel)
        {
            return await _accountService.Transfer(transferModel);
        }

        [HttpPost, Route("Balance")]
        public async Task<IResult<BalanceModel>> Balance(BalanceModel balanceModel)
        {
            return await _accountService.Balance(balanceModel);
        }
    }
}
