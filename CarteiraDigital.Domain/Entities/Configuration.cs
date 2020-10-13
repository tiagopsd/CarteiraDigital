using CarteiraDigital.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarteiraDigital.Domain.Entities
{
    public class Configuration : IEntity<int>
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
