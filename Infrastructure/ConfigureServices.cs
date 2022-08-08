using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection;

    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<DeliverySystemDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DeliverySystemDbConnectionString"),
                    builder => builder.MigrationsAssembly(typeof(DeliverySystemDbContext).Assembly.FullName)));


            services.AddScoped<IDeliverySystemDbContext>(provider => provider.GetRequiredService<DeliverySystemDbContext>());




            return services;
        }
    }

