using CarteiraDigital.Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarteiraDigital.Domain.Entities
{
    public class Account : IEntity<long>
    {
        public long Id { get; set; }
        public decimal Balance { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
    }
}
