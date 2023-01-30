using Application.Viechles.Commands.CreateViechle;
using FluentValidation.TestHelper;

namespace Tests.Application.Tests.Veichles.Commands.CreateVeichleCommandTests;
public class CreateVeichleCommandValidatorTests
{
    private readonly CreateViechleCommandValidator _createViechleCommandValidator;
    public CreateVeichleCommandValidatorTests()
    {
        _createViechleCommandValidator = new CreateViechleCommandValidator();
    }
    [Theory]
    [InlineData("","","")]
    [InlineData(null,null,null)]

    public async void VeichleCreateCommand_HaveValidationError_forEmptyInputValues(string? modelInput ,string? typeInput , string? licenceNumberInput)
    {
        //Arrange
        var cmd = new CreateViechleCommand{
            Model = modelInput,
            Type = typeInput,
            LicenceNumber = licenceNumberInput

        };
        //Act
        var response = _createViechleCommandValidator.TestValidate(cmd);
        //Assert
        response.ShouldHaveValidationErrorFor(x => x.Model); 
        response.ShouldHaveValidationErrorFor(x => x.Type);
        response.ShouldHaveValidationErrorFor(x => x.LicenceNumber);
    }

     [Theory]
    [InlineData("a","a","a")]
    public async void VeichleCreateCommand_HaveNOValidationError_forNonEmptyInputValues(string? modelInput ,string? typeInput , string? licenceNumberInput)
    {
        //Arrange
        var cmd = new CreateViechleCommand{
            Model = modelInput,
            Type = typeInput,
            LicenceNumber = licenceNumberInput

        };
        //Act
        var response = _createViechleCommandValidator.TestValidate(cmd);
        //Assert
        response.ShouldNotHaveValidationErrorFor(x => x.Model); 
        response.ShouldNotHaveValidationErrorFor(x => x.Type);
        response.ShouldNotHaveValidationErrorFor(x => x.LicenceNumber);
    }
}