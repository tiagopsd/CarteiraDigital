using CarteiraDigital.Domain.Context;
using CarteiraDigital.Infrastructure.Data.Mappings;
using CarteiraDigital.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarteiraDigital.Infrastructure
{
    public class Context : DbContext, IContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountMapping());
            modelBuilder.ApplyConfiguration(new MovementMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new ConfigurationMapping());
        }
    }
}
