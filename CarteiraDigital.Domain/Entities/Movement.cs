using CarteiraDigital.Domain.Entities.Interfaces;
using CarteiraDigital.Domain.Enumerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarteiraDigital.Domain.Entities
{
    public class Movement : IEntity<long>
    {
        public long Id { get; set; }
        public decimal Amount { get; set; }
        public decimal Tax { get; set; }
        public decimal Bonus { get; set; }
        public decimal Balance { get; set; }
        public MovementType Type { get; set; }
        public DateTime DateTime { get; set; }
        public long AccountId { get; set; }
        public Account Account { get; set; }
    }
}
