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
    public class CurrencyDataConfiguration : IEntityTypeConfiguration<CurrencyData>
    {
        public void Configure(EntityTypeBuilder<CurrencyData> builder)
        {
            builder.ToTable("CurrencyData");
                
            
            // Properties
            builder.Property(cd => cd.Id)
                .IsRequired()
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(cd => cd.CurrencyCode)
                .HasColumnName("currencyCode")
                .HasMaxLength(10)      
                .IsRequired(false);         

            builder.Property(cd => cd.Description)
                .HasColumnName("description")
                .HasColumnType("nvarchar(max)")
                .IsRequired(false);         

            builder.Property(cd => cd.CurrencyRate)
                .HasColumnName("currencyRate")
                .HasColumnType("decimal(18,6)")
                .IsRequired();

            builder.Property(cd => cd.CreatedDateTime)
                .HasColumnName("createdDateTime")
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Property(cd => cd.UpdatedDateTime)
                .HasColumnName("updatedDateTime")
                .HasColumnType("datetime2");

        }
    }
}
