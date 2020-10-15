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
        public UserValidation _userValidation;
        public UserValidationTest()
        {
            var fakeContext = CreateContext();
            var userRepository = (IUserRepository)new UserRepository(fakeContext);
            var configurationRepository = (IConfigurationRepository)new ConfigurationRepository(fakeContext);
            _userValidation = new UserValidation(userRepository, configurationRepository);
        }

        public Context CreateContext()
        {
            var op = new DbContextOptionsBuilder<Context>();
            op.UseInMemoryDatabase("CarteiraDigital");

            var fakeContext = new Context(op.Options);
            return fakeContext;
        }

        public UserModel GetUserModel()
        {
            return new UserModel
            {
                BirthDate = new DateTime(1996, 04, 18),
                Country = 0,
                Cpf = "07901541962",
                Name = "Tiago Proença",
                Password = "84830063"
            };
        }

        [Fact]
        public void ValidateErrorCpf()
        {
            var userModel = GetUserModel();
            userModel.Cpf = "07901541961";

            var result = _userValidation.ValidateModel(userModel);
            Assert.False(result.Success);
        }

        [Fact]
        public void ValidateSuccessCpf()
        {
            var userModel = GetUserModel();
            userModel.Cpf = "07901541962";

            var result = _userValidation.ValidateModel(userModel);
            var resultTest = Result<UserModel>.BuildSucess(userModel);
            Assert.Equal(resultTest.Success, result.Success);
        }

        [Fact]
        public void ValiteErrorName()
        {
            var userModel = GetUserModel();
            userModel.Name = "Tiago 123";
            var result = _userValidation.ValidateModel(userModel);
            Assert.False(result.Success);

            userModel.Name = "Tiago";
            var result2 = _userValidation.ValidateModel(userModel);
            Assert.False(result2.Success);
        }

        [Fact]
        public void ValiteSucessName()
        {
            var userModel = GetUserModel();
            userModel.Name = "Tiago Proença";

            var result = _userValidation.ValidateModel(userModel);
            Assert.True(result.Success);
        }

    }
}
