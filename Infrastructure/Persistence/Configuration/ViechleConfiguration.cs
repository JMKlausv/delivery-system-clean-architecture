using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configuration
{
    public class ViechleConfiguration : IEntityTypeConfiguration<Viechle>
    {
        public void Configure(EntityTypeBuilder<Viechle> builder)
        {
            builder.Property(t => t.Type)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(t => t.Model)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(t => t.LicenceNumber)
                .HasMaxLength(400)
                .IsRequired();
        }
    }
}