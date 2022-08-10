using Application.common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Commands.CreateUser
{
    public record CreateUserCommand: IRequest<string>
    {
       public string email { get; init; }
      public  string password { get; init; }

    }
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
    {
       private readonly IIdentityService _identityService;
     
        public CreateUserCommandHandler(IIdentityService identityService)
        {
          _identityService = identityService;
        }
        
        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
       

            var response =await _identityService.CreateUserAsync(request.email, request.password);
              if (!response.result.Succeeded)
              {
             
                  throw new CantCreateUserException(string.Join(",", response.result.Errors));
              }
              return response.userId;
            
            

        }
    }
}
