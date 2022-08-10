using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IDeliverySystemDbContext  
    {
        DatabaseFacade database { get; }
       DbSet<Category> Categories { get; }
        DbSet<Viechle> Viechles { get; }
        DbSet<Product> Products { get; }
        DbSet<Order> Orders { get; }
        DbSet<OrderAddress> OrderAddresses { get; }
        DbSet<OrderProductItem> OrderProductItems { get; }

Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        
    }
}
