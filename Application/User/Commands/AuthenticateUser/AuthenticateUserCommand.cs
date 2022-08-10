using Application.common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Commands.AuthenticateUser
{
    public  class AuthenticateUserCommand : IRequest<string>
    {
       public string email { get; init; }
    public string password { get; init; }

}
public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, string>
{
    private readonly IIdentityService _identityService;

    public AuthenticateUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<string> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {


        var response = await _identityService.AuthenticateUserAsync(request.email, request.password);
        if (!response.result.Succeeded)
        {

            throw new CantCreateUserException(string.Join(",", response.result.Errors));
        }
        return response.tokenString;



    }
}
}
