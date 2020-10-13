using CarteiraDigital.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarteiraDigital.Infrastructure.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User", "dbo");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.BirthDate).HasColumnName("BirthDate").HasColumnType("datetime").IsRequired();
            builder.Property(d => d.Cpf).HasColumnName("Cpf").HasColumnType("varchar").HasMaxLength(11).IsRequired();
            builder.Property(d => d.Name).HasColumnName("Name").HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(d => d.Password).HasColumnName("Password").HasColumnType("varchar").HasMaxLength(20).IsRequired();
        }
    }
}
