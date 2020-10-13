using System;
using System.Collections.Generic;
using System.Text;

namespace CarteiraDigital.Domain.Entities.Interfaces
{
    public interface IEntity<T> : IEntity
    {
        T Id { get; set; }
    }

    public interface IEntity
    {
    }
}
