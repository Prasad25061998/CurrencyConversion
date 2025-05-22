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
    public class UserDetailsConfiguration : IEntityTypeConfiguration<UserDetails>
    {
        public void Configure(EntityTypeBuilder<UserDetails> builder)
        {
          
            builder.ToTable("Users");

            // Primary Key
            builder.HasKey(u => u.Id);

            // Properties configuration
            builder.Property(u => u.Username)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.Password)
                   .IsRequired()
                   .HasMaxLength(256); 

            builder.Property(u => u.Role)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasIndex(u => u.Username)
                   .IsUnique(); 
        }
    }

}
