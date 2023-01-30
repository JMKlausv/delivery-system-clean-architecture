using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Tests.Application.Integration.Tests.Helpers;
public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
    : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {

        //     var claims = new List<Claim>
        //     {

        //        new Claim(ClaimTypes.NameIdentifier,"testId"),
        //        new Claim(ClaimTypes.Email, "TestEmail")
        //    };

        // var token = new JwtSecurityToken(
        //     issuer: "https://localhost:4200",
        //     audience: "https://localhost:7247",
        //     claims: claims,
        //     expires: DateTime.UtcNow.AddDays(1),
        //     notBefore: DateTime.UtcNow,
        //     signingCredentials: new SigningCredentials(
        //         key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AewMR9W16q7bvRANHqgmSASimYHe1bEg")),
        //         algorithm: SecurityAlgorithms.HmacSha256)
        //     );
        // var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        var claims = new[] { new Claim(ClaimTypes.Name, "Test user") };
        var identity = new ClaimsIdentity(claims, "Test");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "Test");

        var result = AuthenticateResult.Success(ticket);
        return Task.FromResult(result);
    }
}