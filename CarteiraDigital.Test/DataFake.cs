using CarteiraDigital.Domain.Entities;
using CarteiraDigital.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarteiraDigital.Test
{
    public class DataFake
    {
        public UserModel GetUserModel(string cpf)
        {
            return new UserModel
            {
                BirthDate = new DateTime(1996, 04, 18),
                Country = Domain.Enumerators.Country.Brasil,
                Cpf = cpf,
                Name = "Tiago Proença",
                Password = "12345678"
            };
        }

        public User GetUserValid(string cpf)
        {
            return new User
            {
                BirthDate = new DateTime(1996, 04, 18),
                Cpf = cpf,
                Name = "Tiago Proença",
                Password = "12345678",
            };
        }

        public DepositModel GetDepositModel(string cpf)
        {
            return new DepositModel
            {
                Amount = 0,
                Cpf = cpf,
                Password = "12345678"
            };
        }

        internal BalanceModel GetBalanceUserModel2(string cpf)
        {
            return new BalanceModel
            {
                Cpf = cpf,
                Password = "12345678"
            };
        }

        internal FilterMovementModel GetFilterMovement(string cpf)
        {
            return new FilterMovementModel
            {
                Cpf = cpf,
                Password = "12345678",
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.Date
            };
        }

        internal WithdrawModel GetWithdrawModel(string cpf)
        {
            return new WithdrawModel
            {
                Amount = 0,
                Cpf = cpf,
                Password = "12345678"
            };
        }
    }
}
