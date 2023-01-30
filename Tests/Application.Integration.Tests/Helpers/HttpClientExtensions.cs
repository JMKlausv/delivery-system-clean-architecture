using System.Dynamic;
using System.Net;
using System.Security.Claims;
using Newtonsoft.Json;

namespace Tests.Application.Integration.Tests.Helpers;
public static class HttpClientExtensions
{
    public async static Task<HttpClient> Authenticate(this HttpClient httpClient)
    {
         var requestBody = new
        {
            email = "admin@gmail.com",
            password = "pass123"
        };
        string json = JsonConvert.SerializeObject(requestBody);

        StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        var authResponse = await httpClient.PostAsync("/api/User/login", httpContent);
        var token = await authResponse.Content.ReadAsStringAsync();
        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer " + token);


        // var claims = new Dictionary<string, object>
        //     {
        //         { ClaimTypes.Name, "test@sample.com" },
        //         { ClaimTypes.Role, "admin" },
        //         { "http://mycompany.com/customClaim", "someValue" },
        //     };


        // var claims = new List<Claim>
        //     {

        //        new Claim(ClaimTypes.NameIdentifier,"testId"),
        //        new Claim(ClaimTypes.Email, "TestEmail"),
        //    };
        // var roles = new string[]{"Admin"};
        // var username = "TestUser";

        // httpClient.SetFakeBearerToken(username:username,roles:roles);

        // dynamic data = new ExpandoObject();
        // data.sub = Guid.NewGuid();
        // data.role = new[] { "sub_role", "admin" };
        // httpClient.SetFakeBearerToken((object)data);


        return httpClient;

    }
}