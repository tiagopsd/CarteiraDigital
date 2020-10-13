using CarteiraDigital.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarteiraDigital.Infrastructure.Mappings
{
    public class MovementMapping : IEntityTypeConfiguration<Movement>
    {
        public void Configure(EntityTypeBuilder<Movement> builder)
        {
            builder.ToTable("Movement", "dbo");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Amount).HasColumnName("Amount").HasColumnType("decimal").IsRequired();
            builder.Property(d => d.Tax).HasColumnName("Tax").HasColumnType("decimal").IsRequired();
            builder.Property(d => d.Bonus).HasColumnName("Bonus").HasColumnType("decimal").IsRequired();
            builder.Property(d => d.DateTime).HasColumnName("DateTime").HasColumnType("datetime").IsRequired();
            builder.Property(d => d.Type).HasColumnName("Type").HasColumnType("smallint").IsRequired();
            builder.HasOne(d => d.Account).WithMany().HasForeignKey(d => d.AccountId);
        }
    }
}
