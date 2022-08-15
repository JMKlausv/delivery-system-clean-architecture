using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<(Result result , string userId)> CreateUserAsync(string  email, string password);
        Task<(Result result , string tokenString)> AuthenticateUserAsync(string email, string password);
        Task<string> GetUserNameAsync(string userId);
    }
}
