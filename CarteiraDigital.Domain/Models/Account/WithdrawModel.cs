using System;
using System.Collections.Generic;
using System.Text;

namespace CarteiraDigital.Domain.Models
{
    public class WithdrawModel
    {
        public string Cpf { get; set; }
        public string Password { get; set; }
        public decimal Amount { get; set; }
    }
}
