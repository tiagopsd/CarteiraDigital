using CarteiraDigital.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarteiraDigital.Infrastructure.Mappings
{
    public class AccountMapping : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Account", "dbo");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Balance).HasColumnName("Balance").HasColumnType("decimal").IsRequired();
            builder.HasOne(d => d.User).WithMany().HasForeignKey(d => d.UserId);
        }
    }
}
