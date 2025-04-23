using FluentValidation.TestHelper;
using OWT.BoatManager.Application.UseCases.Boats.Commands.Validators;
using OWT.BoatManager.Core.UseCases.Boats;
using Xunit;

namespace OWT.BoatManager.Application.Tests.UseCases.Boats.Validators;

public sealed class BoatModelValidatorTest
{
    private readonly BoatModelFaker _faker = new();
    private readonly BoatModelValidator _sut = new();

    [Fact]
    public void Validate_ForValidData_ShouldNotContainValidationErrors()
    {
        // Arrange
        var model = _faker.Generate();

        // Act
        var result = _sut.TestValidate(model);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_ForEmptyName_ShouldContainValidationErrors()
    {
        // Arrange
        _faker.RuleFor(b => b.Name, () => string.Empty);
        var model = _faker.Generate();

        // Act
        var result = _sut.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(b => b.Name);
    }

    [Fact]
    public void Validate_ForTooLongName_ShouldContainValidationErrors()
    {
        // Arrange
        _faker.RuleFor(b => b.Name, () => new ('a', BoatConstraints.NameMaxLength + 1));
        var model = _faker.Generate();

        // Act
        var result = _sut.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(b => b.Name);
    }

    [Fact]
    public void Validate_ForEmptyDescription_ShouldContainValidationErrors()
    {
        // Arrange
        _faker.RuleFor(b => b.Description, () => string.Empty);
        var model = _faker.Generate();

        // Act
        var result = _sut.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(b => b.Description);
    }

    [Fact]
    public void Validate_ForTooLongDescription_ShouldContainValidationErrors()
    {
        // Arrange
        _faker.RuleFor(b => b.Description, () => new ('a', BoatConstraints.DescriptionMaxLength + 1));
        var model = _faker.Generate();

        // Act
        var result = _sut.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(b => b.Description);
    }
}