

using Application.common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.User.Commands.AuthenticateUser;
using FluentAssertions;
using Moq;

namespace Tests.Application.Tests.User.Commands.AuthenticateUserCommandTests;
public class AuthenticateUserCommandTests {
    private readonly Mock<IIdentityService> _identityServiceMock; 
    public AuthenticateUserCommandTests()
    {
        _identityServiceMock = new Mock<IIdentityService>();
    }
    [Fact]
    public async void AuthenticateUserCommandHandler_ShouldReturnTokenString(){
        //Arrange
        var request = new AuthenticateUserCommand{
            email ="vvv@example.com",
         password = "password123?A"
        };
        
        _identityServiceMock.Setup(x => x.AuthenticateUserAsync(request.email,request.password))
                                .ReturnsAsync((Result.Success() ,"tokenstring"));
        //Act
        var sut = new AuthenticateUserCommandHandler(_identityServiceMock.Object);
        var response = await sut.Handle(request,default);

        //Assert

        await sut.Invoking(_ => _.Handle(request,default)).Should().NotThrowAsync();
        response.Should().BeOfType(typeof(string));
        response.Should().BeSameAs("tokenstring");


    }
        [Fact]
    public async void AuthenticateUserCommandHandler_ShouldThrowException_forInvalidRequest(){
        //Arrange
        var request = new AuthenticateUserCommand{
            email ="unregistered@example.com",
         password = "password123?A"
        };
        
        _identityServiceMock.Setup(x => x.AuthenticateUserAsync(request.email,request.password))
                                .ReturnsAsync((Result.Failure(new string[]{"invalid login"}),string.Empty));
        
        //Act
        var sut = new AuthenticateUserCommandHandler(_identityServiceMock.Object);

        //Assert
       await sut.Invoking(_=> _.Handle(request,default))
                    .Should().ThrowAsync<CantCreateUserException>();
      

    }

}