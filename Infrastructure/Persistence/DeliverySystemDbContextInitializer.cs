using Domain.Entities;
using Infrastructure.identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class DeliverySystemDbContextInitializer
    {
        private readonly ILogger<DeliverySystemDbContextInitializer> _logger;
        private readonly DeliverySystemDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DeliverySystemDbContextInitializer(ILogger<DeliverySystemDbContextInitializer> logger, DeliverySystemDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsMySql() 
                // && _context.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory"
                )
                {
                    await _context.Database.MigrateAsync();
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
                await TrySeedViechle();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            // Default roles      
            var adminRole = new IdentityRole("admin");
            var systemUserRole = new IdentityRole("system-user");

            if (_roleManager.Roles.All(r => r.Name != adminRole.Name))
            {
                await _roleManager.CreateAsync(adminRole);
            }
            if (_roleManager.Roles.All(r => r.Name != systemUserRole.Name))
            {
                await _roleManager.CreateAsync(systemUserRole);
            }

            // Default users
            ApplicationUser administrator = new ApplicationUser
            {
                Email = "admin@gmail.com",
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "admin"
            };

            var defaultPassword = "pass123";
            // _logger.LogCritical($"userrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr : {administrator} ");
            if (_userManager.Users.All(u => u.Email != administrator.Email))
            {
                var result = await _userManager.CreateAsync(administrator, defaultPassword);
                if (!result.Succeeded)
                {
                    _logger.LogError($"could not create user : {administrator} ");
                }
                await _context.SaveChangesAsync();

                await _userManager.AddToRolesAsync(administrator, new[] { adminRole.Name });
            }



            await _context.SaveChangesAsync();
        }
        public async Task TrySeedViechle()
        {
            _logger.LogCritical("sedding viechle.........");
            IList<Viechle> viechles = new List<Viechle>{
            new Viechle {

                Model = "model1",
                LicenceNumber = "DD438975983",
                Type = "type1",
            },
              new Viechle {

                Model = "model2",
                LicenceNumber = "hg7487398298",
                Type = "type2",
            },
              new Viechle {

                Model = "model3",
                LicenceNumber = "hg7487398287",
                Type = "type2",
            }
        };
            _context.Viechles.AddRange(viechles);
            await _context.SaveChangesAsync();

        }

    }
}
