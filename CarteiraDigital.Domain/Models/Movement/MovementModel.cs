using CarteiraDigital.Domain.Enumerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarteiraDigital.Domain.Models
{
    public class MovementModel
    {
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public DateTime DateTime { get; set; }
    }
}
