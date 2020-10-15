using CarteiraDigital.Domain.Entities;
using CarteiraDigital.Domain.Enumerators;
using CarteiraDigital.Domain.Models;
using CarteiraDigital.Domain.Models.Interfaces;
using CarteiraDigital.Domain.Repositories;
using CarteiraDigital.Domain.Validations;
using CarteiraDigital.Infrastructure;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CarteiraDigital.Service.Validations
{
    public class UserValidation : AbstractValidator<UserModel>, IUserValidation
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfigurationRepository _configurationRepository;



        public UserValidation(IUserRepository userRepository, IConfigurationRepository configurationRepository)
        {
            _userRepository = userRepository;
            _configurationRepository = configurationRepository;

            RuleFor(d => d.BirthDate)
                .NotNull()
                .NotEmpty()
                .WithMessage("Favor inserir sua data nascimento.")
                .Must(ValidateBirthDate)
                .WithMessage("A legislação atual não nos permite ter clientes com menos de 18 anos.");

            RuleFor(d => d.Cpf)
                .NotNull()
                .NotEmpty()
                .WithMessage("Favor inserir seu cpf.")
                .Must(CpfValidation.IsCpf)
                .WithMessage("Favor inserir um cpf válido.")
                .Must(ExistCpf)
                .WithMessage("Cliente já possui uma conta.");

            RuleFor(d => d.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Favor inserir seu nome.")
                .Must(ValidateName)
                .WithMessage("Favor informar o nome completo válido.");

            RuleFor(d => d.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Favor inserir sua senha.")
                .Length(8, 20)
                .WithMessage("Sua senha deve conter entre 8 a 20.")
                .Must(ValidatePassword)
                .WithMessage("Senha não pode possuir espaços em brancos.");
        }

        private bool ExistCpf(string cpf)
        {
            return !_userRepository.ExistCpf(cpf.RemoveMaskCpf()).Result;
        }

        private bool ValidatePassword(string passoword)
        {
            return !passoword.Contains(' ');
        }

        private bool ValidateName(string nameCompleto)
        {
            var names = nameCompleto
                .Split(' ')
                .Where(d => !string.IsNullOrWhiteSpace(d))
                .ToList();

            if (names.Count <= 1)
                return false;

            foreach (var name in names)
            {
                if (!Regex.IsMatch(name, @"^[\p{L} \.\-]+$"))
                    return false;
            }
            return true;
        }

        private bool ValidateBirthDate(UserModel userModel, DateTime birthDate)
        {
            switch (userModel.Country)
            {
                case Country.EUA:
                    var allowedAgeEUA = GetAge("AllowedAgeEUA", 21);
                    return Validate(allowedAgeEUA);
                default:
                    var allowedAgeBrasil = GetAge("AllowedAgeBrasil", 18);
                    return Validate(allowedAgeBrasil);
            }

            bool Validate(int year) => !(birthDate.Date.AddYears(year) >= DateTime.Now.Date);
            int GetAge(string key, int valueDefault) => _configurationRepository.GetValueByKey<int?>(key) ?? valueDefault;
        }

        public IResult<UserModel> ValidateModel(UserModel model)
        {
            var result = this.Validate(model);
            if (!result.IsValid)
            {
                var resultModel = new Result<UserModel>();
                result.Errors.ToList().ForEach(d => resultModel.AddError(d.ErrorMessage));
                return resultModel;
            }
            return Result<UserModel>.BuildSucess(model);
        }
    }
}
