
using Application.Common.Interfaces;
using Infrastructure.identity;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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
           services.AddScoped<DeliverySystemDbContextInitializer>();
           services.AddIdentity<ApplicationUser, IdentityRole>(options =>
           {
               options.Password.RequireNonAlphanumeric = false;
               options.Password.RequireUppercase = false; 
               options.User.RequireUniqueEmail = true;
           
           })
            .AddEntityFrameworkStores<DeliverySystemDbContext>()
            .AddDefaultTokenProviders();

            services.AddTransient<IIdentityService, IdentityService>();


        services.AddAuthentication(ops =>
        {
            ops.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            ops.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            ops.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateActor = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))


                };
               
            });


        return services;
        }
    }

