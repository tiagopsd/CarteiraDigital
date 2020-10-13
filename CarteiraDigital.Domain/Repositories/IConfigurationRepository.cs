using CarteiraDigital.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarteiraDigital.Domain.Repositories
{
    public interface IConfigurationRepository : IRepository<Configuration>
    {
        T GetValueByKey<T>(string key);
    }
}
