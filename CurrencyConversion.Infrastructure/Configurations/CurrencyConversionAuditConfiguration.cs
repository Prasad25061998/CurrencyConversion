using CurrencyConversion.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConversion.Infrastructure.Configurations
{
    public class CurrencyConversionAuditConfiguration : IEntityTypeConfiguration<CurrencyConversionAudit>
    {
        public void Configure(EntityTypeBuilder<CurrencyConversionAudit> builder)
        {
            builder.ToTable("CurrencyConversionAudit")
                .HasKey(x => new { x.Id });

            // Properties
            builder.Property(cd => cd.Id)
                .IsRequired()
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(cd => cd.InputCurrency)
                .HasColumnName("input_currency")
                .HasColumnType("nvarchar(10)")
                .IsRequired(false);

            builder.Property(cd => cd.OutputCurrency)
                .HasColumnName("output_currency")
                .HasColumnType("nvarchar(10)")
                .IsRequired(false);

            builder.Property(cd => cd.ConvertedAmount)
                .HasColumnName("converted_amount")
                .HasColumnType("bigint")
                .IsRequired();

            builder.Property(cd => cd.CreatedDateTime)
                .HasColumnName("createdDateTime")
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Property(cd => cd.InputAmount)
                .HasColumnName("input_amount")
                .HasColumnType("bigint")
                .IsRequired();

        }
    }
    
}
