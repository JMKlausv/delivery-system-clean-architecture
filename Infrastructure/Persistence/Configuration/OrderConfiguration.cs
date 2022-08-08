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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(t => t.OrderDate)
                .IsRequired();
            builder.Property(t => t.DeliveryDate)
             .IsRequired();
            builder.Property(t => t.TotalPrice)
             .IsRequired();
            builder.Property(t => t.ViechleId)
             .IsRequired();
           
        }
    }
}
