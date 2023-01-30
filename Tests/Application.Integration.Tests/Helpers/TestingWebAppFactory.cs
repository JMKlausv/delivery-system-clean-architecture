using System.Text;
using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.identity;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using WebMotions.Fake.Authentication.JwtBearer;

namespace Tests.Application.Integration.Tests.Helpers;
public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {

        builder.ConfigureTestServices(async Services =>
        {

            var descriptor = Services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<DeliverySystemDbContext>));
            if (descriptor != null)
            {
                Services.Remove(descriptor);
            }

            Services.AddDbContext<DeliverySystemDbContext>(options =>
            {
                options.UseInMemoryDatabase("inmemoryDeliveryDb");
            });

            Services.AddTransient<IIdentityService, IdentityService>();

            // //setting up fake jwt authentication
            // Services.AddAuthentication(FakeJwtBearerDefaults.AuthenticationScheme).AddFakeJwtBearer();
            // Services.AddAuthentication(options =>
            //     {
            //         options.DefaultAuthenticateScheme = "Test";
            //         options.DefaultChallengeScheme = "Test";
            //         options.DefaultScheme = "Test";
            //     }).AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
            //            "Test", options => { });
            var sp = Services.BuildServiceProvider();
            using (var scope = sp.CreateScope())
            using (var appDbContext = scope.ServiceProvider.GetRequiredService<DeliverySystemDbContext>())
            {

                var _logger = scope.ServiceProvider.GetRequiredService<ILogger<WebApplicationFactory<Program>>>();
                try
                {
                    appDbContext.Database.EnsureCreated();
                }
                catch (Exception)
                {

                    throw;
                }
            }

        });
    }

}