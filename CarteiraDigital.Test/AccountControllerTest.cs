using CarteiraDigital.API.Controllers;
using CarteiraDigital.Domain.Models;
using CarteiraDigital.Domain.Models.Interfaces;
using CarteiraDigital.Domain.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CarteiraDigital.Test
{
    public class AccountControllerTest
    {
        AccountController _accountController;

        
        public AccountControllerTest()
        {
            
            _accountController = new AccountController(iAccountService);
        
        }

        [Fact]
        public void Deposit_CpfValidate()
        {
            var okResult = _accountController.Deposit(new Domain.Models.DepositModel
            {
                Amount = 100,
                Cpf = "81412410958",
                Password = "12345678"
            }).Result;
            // Assert
            _ = Assert.IsType<IResult<DepositModel>>(!okResult.Success);
        }
    }
}
