using System;
using System.Collections.Generic;
using System.Text;

namespace CarteiraDigital.Domain.Enumerators
{
    public enum MovementType : short
    {
        Create,
        Deposit,
        Withdraw,
        TransferDebit,
        TransferCredit
    }
}
