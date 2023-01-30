using Application.Common.Interfaces;
using Application.Common.Models;
using Application.User.Commands.CreateUser;
using FluentAssertions;
using Moq;

namespace Tests.Application.Tests.User.Commands.CreateUserCommandTests;
public class CreateUserCommandTests {
    private readonly Mock<IIdentityService> _identityServiceMock;
    public CreateUserCommandTests()
    {
        _identityServiceMock = new Mock<IIdentityService>();
    }
    [Fact]
    public async void CreateUserCommand_ShouldReturnUserId(){
        //Arrange
        var cmd = new CreateUserCommand{
            email = "test@example.com",
            password = "password"
        };
        _identityServiceMock.Setup(x => x.CreateUserAsync(cmd.email,cmd.password))
                                .ReturnsAsync((Result.Success(), "testUserId" ));
        
        //Act
        var sut = new CreateUserCommandHandler(_identityServiceMock.Object);
        var response = await sut.Handle(cmd, default);
        
        //Assert
        await sut.Invoking(_ => _.Handle(cmd , default))
                .Should().NotThrowAsync();
        response.Should().BeOfType(typeof(string));
        response.Should().Be("testUserId");
        
    }
}