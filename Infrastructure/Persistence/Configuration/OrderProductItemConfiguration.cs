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
    public class OrderProductItemConfiguration : IEntityTypeConfiguration<OrderProductItem>
    {
        public void Configure(EntityTypeBuilder<OrderProductItem> builder)
        {
            builder.Property(t => t.OrderId)
                .IsRequired();
            builder.Property(t => t.quantity)
             .IsRequired();
          


        }
    }
}
