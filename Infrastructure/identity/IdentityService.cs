using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;
using System.Text;


namespace Infrastructure.identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        //TODO: implement authorization......
        public IdentityService(UserManager<ApplicationUser> userManager , IConfiguration configuration)
        {
           _userManager = userManager;
          _configuration = configuration;
        }
        public async Task<(Result result, string tokenString)> AuthenticateUserAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user != null && await _userManager.CheckPasswordAsync(user,password))
            {

                var tokenString = await generateToken(user);
                return (Result.Success(), tokenString); 


            }
            string[] errors = new string[] { "Invalid login" };
            return (Result.Failure(errors), string.Empty);


        }

        public async Task<(Result result, string userId)> CreateUserAsync(string email, string password)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);
            if(existingUser!= null)
            {
               
                return (Result.Failure(new string[] { "Email already in use" }), string.Empty);
            }
            var user = new ApplicationUser
            {
                Email = email,
                UserName = email
            };
           
           var result = await _userManager.CreateAsync(user,password);
            return (result.ToApplicationResult(),user.Id);  
        }
    

        private async Task<string> generateToken(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
            
               new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
               new Claim(ClaimTypes.Email, user.Email)
           };
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                
            }
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                    algorithm: SecurityAlgorithms.HmacSha256)
                );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }




        //TODO:
        //public bool authorizeUser(user)
    }
}
