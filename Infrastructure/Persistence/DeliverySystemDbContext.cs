using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.identity;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class DeliverySystemDbContext : IdentityDbContext<ApplicationUser>, IDeliverySystemDbContext
    {
        private readonly IMediator _mediator;

        public DeliverySystemDbContext(DbContextOptions<DeliverySystemDbContext> options , IMediator mediator): base(options)    
        {
            _mediator = mediator;
        }
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Viechle> Viechles => Set<Viechle>();   
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderAddress> OrderAddresses => Set<OrderAddress>();
        public DbSet<OrderProductItem> OrderProductItems => Set<OrderProductItem>();

        public DatabaseFacade database => Database;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //TODO: dispatching domain events
            return await base.SaveChangesAsync(cancellationToken);
        }
        
    }
}
