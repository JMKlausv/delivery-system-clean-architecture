using Application.Viechles.Commands.CreateViechle;
using FluentAssertions;
using Tests.Application.Tests.Helpers;
using Tests.Application.Tests.MockData;

namespace Tests.Application.Tests.Veichles.Commands.CreateVeichleCommandTests;
public class CreateVeichleCommandTests : TestDbContextBase 
{
    public CreateVeichleCommandTests()
    {
        
    }
    [Fact]
    public async void CreateViechleCommand_ShouldCreateNewVeichle(){
        //Arrange
        var cmd = new CreateViechleCommand{
            Model = "new model",
            Type = "new type",
            LicenceNumber = "lc39827498"
        };
       _context.Viechles.AddRange(ViechleMockData.GetViechles());
       _context.SaveChanges();
       var expectedCount = _context.Viechles.Count() + 1;
       var sut = new CreateViechleCommanddHandler(_context);

        //Act
       var response = await sut.Handle(cmd,default);
       var actualCount = _context.Viechles.Count();

        //Assert
        actualCount.Should().Be(expectedCount);

    }

       [Fact]
    public async void CreateViechleCommand_ShouldReturnCreatedViechleId(){
        //Arrange
        var cmd = new CreateViechleCommand{
            Model = "new model",
            Type = "new type",
            LicenceNumber = "lc39827498"
        };
       var sut = new CreateViechleCommanddHandler(_context);

        //Act
       var response = await sut.Handle(cmd,default);

        //Assert
        response.Should().NotBe(null);
        response.Should().BeOfType(typeof(int));
        response.Should().Be(1);

    }
}