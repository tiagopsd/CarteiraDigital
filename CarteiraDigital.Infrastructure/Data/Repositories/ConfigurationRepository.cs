using CarteiraDigital.Domain.Context;
using CarteiraDigital.Domain.Entities;
using CarteiraDigital.Domain.Repositories;
using CarteiraDigital.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarteiraDigital.Infrastructure
{
    public class ConfigurationRepository : Repository<Configuration>, IConfigurationRepository
    {
        public ConfigurationRepository(IContext context) : base(context)
        {
        }

        public T GetValueByKey<T>(string key)
        {
            if (!Set.Any(d => d.Key == key))
                return default;
            return (T)Convert.ChangeType(Set.FirstOrDefault(d => d.Key == key).Value, typeof(T));
        }
    }
}
