using CarteiraDigital.Domain.Enumerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarteiraDigital.Domain.Models
{
    public class UserModel
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public DateTime BirthDate { get; set; }
        public string Password { get; set; }
        public Country Country { get; set; }
    }
}
