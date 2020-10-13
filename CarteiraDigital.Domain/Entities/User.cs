using CarteiraDigital.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarteiraDigital.Domain.Entities
{
    public class User : IEntity<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public DateTime BirthDate { get; set; }
        public string Password { get; set; }
    }
}
