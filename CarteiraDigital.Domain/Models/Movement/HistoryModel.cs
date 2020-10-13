using System;
using System.Collections.Generic;
using System.Text;

namespace CarteiraDigital.Domain.Models.Movement
{
    public class HistoryModel
    {
        public decimal InitialBalance { get; set; }
        public List<MovementModel> MovementModels { get; set; }
        public decimal FinalBalance { get; set; }
        public decimal CurrentBalance { get; set; }
    }
}
