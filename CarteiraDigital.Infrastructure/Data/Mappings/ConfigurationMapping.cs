using CarteiraDigital.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarteiraDigital.Infrastructure.Data.Mappings
{
    public class ConfigurationMapping : IEntityTypeConfiguration<Configuration>
    {
        public void Configure(EntityTypeBuilder<Configuration> builder)
        {
            builder.ToTable("Configuration", "dbo");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Key).HasColumnName("Key").HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(d => d.Value).HasColumnName("Value").HasColumnType("varchar").HasMaxLength(50).IsRequired();
        }
    }
}
