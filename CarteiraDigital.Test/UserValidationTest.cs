using CarteiraDigital.API.Controllers;
using CarteiraDigital.Domain.Entities;
using CarteiraDigital.Domain.Models;
using CarteiraDigital.Domain.Models.Interfaces;
using CarteiraDigital.Domain.Repositories;
using CarteiraDigital.Domain.Service;
using CarteiraDigital.Domain.Validations;
using CarteiraDigital.Infrastructure;
using CarteiraDigital.Infrastructure.Repositories;
using CarteiraDigital.Service.Services;
using CarteiraDigital.Service.Validations;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CarteiraDigital.Test
{
    public class UserValidationTest
    {
        private UserValidation _userValidation;
        private DataFake _dataFake;
        public UserValidationTest()
        {
            var fakeContext = new ContextFake().CreateContext();
            var userRepository = (IUserRepository)new UserRepository(fakeContext);
            var configurationRepository = (IConfigurationRepository)new ConfigurationRepository(fakeContext);
            _userValidation = new UserValidation(userRepository, configurationRepository);
            _dataFake = new DataFake();
        }

        [Fact]
        public void ValidateSuccessBirthDateBrazil()
        {
            var userModel = _dataFake.GetUserModel("00605656088");
            userModel.BirthDate = DateTime.Now.AddYears(-18).Date;

            var result = _userValidation.ValidateModel(userModel);
            Assert.True(result.Success);
        }

        [Fact]
        public void ValidateSuccessBirthDateEUA()
        {
            var userModel = _dataFake.GetUserModel("52378834004");
            userModel.BirthDate = DateTime.Now.AddYears(-21).Date;
            userModel.Country = Domain.Enumerators.Country.EUA;

            var result = _userValidation.ValidateModel(userModel);
            Assert.True(result.Success);
        }

        [Fact]
        public void ValidateErrorBirthDateBrazil()
        {
            var userModel = _dataFake.GetUserModel("91587889056");
            userModel.BirthDate = DateTime.Now.AddYears(-17).Date;

            var result = _userValidation.ValidateModel(userModel);
            Assert.False(result.Success);
        }

        [Fact]
        public void ValidateErrorBirthDateEUA()
        {
            var userModel = _dataFake.GetUserModel("93473912042");
            userModel.BirthDate = DateTime.Now.AddYears(-20).Date;
            userModel.Country = Domain.Enumerators.Country.EUA;

            var result = _userValidation.ValidateModel(userModel);
            Assert.False(result.Success);
        }

        [Fact]
        public void ValidateErrorCpf()
        {
            var cpfsInvalid = new[]
            {
                "07901541961",
                "079.015.419-61",
                "123.123.123.12",
                "11122233322"
            }.ToList();

            var userModel = _dataFake.GetUserModel("");
            cpfsInvalid.ForEach(d =>
            {
                userModel.Cpf = d;
                var result = _userValidation.ValidateModel(userModel);
                Assert.False(result.Success);
            });
        }

        [Fact]
        public void ValidateSuccessCpf()
        {
            var cpfsValid = new[]
            {
                "07901541962",
                "079.015.419-62",
                "09135703970",
                "091.357.039-70"
            }.ToList();

            var userModel = _dataFake.GetUserModel("");
            cpfsValid.ForEach(d =>
            {
                userModel.Cpf = d;
                var result = _userValidation.ValidateModel(userModel);
                Assert.True(result.Success);
            });
        }

        [Fact]
        public void ValidateErrorName()
        {
            var namesInvalid = new[]
            {
                " ",
                " Tiago ",
                "123",
                "123 123",
                "Tiago 123",
                "Tiago Proença 123",
                "123 Tiago"
            }.ToList();

            var userModel = _dataFake.GetUserModel("53911749023");
            namesInvalid.ForEach(d =>
            {
                userModel.Name = d;
                var result = _userValidation.ValidateModel(userModel);
                Assert.False(result.Success);
            });
        }

        [Fact]
        public void ValidateSucessName()
        {
            var namesValid = new[]
            {
                "Tiago Proença",
                "Tiago Proença dos",
                "Tiago Proença dos Santos"
            }.ToList();

            var userModel = _dataFake.GetUserModel("60805186000");
            namesValid.ForEach(d =>
            {
                userModel.Name = d;
                var result = _userValidation.ValidateModel(userModel);
                Assert.True(result.Success);
            });
        }

        [Fact]
        public void ValidateSuccessPassword()
        {
            var passValid = new[]
            {
                "12345678",
                "12345678912345678912",
                "123zxc123",
                "Qwt123tr48",
                "askditfo",
            }.ToList();
            var userModel = _dataFake.GetUserModel("42718766077");
            passValid.ForEach(d =>
            {
                userModel.Password = d;
                var result = _userValidation.ValidateModel(userModel);
                Assert.True(result.Success);
            });

        }
        [Fact]
        public void ValidateErrorPassword()
        {
            var passInvalid = new[]
           {
                "12345678 ",
                "123456789123456789123",
                " 123zxc123 ",
                " Qwt123tr48",
                "ask ditfo",
            }.ToList();
            var userModel = _dataFake.GetUserModel("52006444026");
            passInvalid.ForEach(d =>
            {
                userModel.Password = d;
                var result = _userValidation.ValidateModel(userModel);
                Assert.False(result.Success);
            });
        }

        [Fact]
        public void ValidateExistUser()
        {
            var user = new User
            {
                BirthDate = DateTime.Now.AddYears(-20),
                Cpf = "56602275038",
                Name = "Amilton Nobregas",
                Password = "12345678"
            };
            var context = new ContextFake().CreateContext();
            var repostorio = new UserRepository(context);
            repostorio.AddAsync(user).Wait();
            context.SaveChanges();

            var userModel = new UserModel
            {
                BirthDate = DateTime.Now.AddYears(-20),
                Cpf = "56602275038",
                Name = "Amilton Nobregas",
                Password = "12345678",
            };

            var result = _userValidation.ValidateModel(userModel);
            Assert.False(result.Success);

            repostorio.Delete(user).Wait();
            context.SaveChanges();
        }
    }
}
