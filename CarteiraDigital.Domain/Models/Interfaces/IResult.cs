using System;
using System.Collections.Generic;
using System.Text;

namespace CarteiraDigital.Domain.Models.Interfaces
{
    public interface IResult<T> where T : class
    {
        bool Success { get; }
        List<string> Messages { get; }
        T Model { get; }
    }
}
