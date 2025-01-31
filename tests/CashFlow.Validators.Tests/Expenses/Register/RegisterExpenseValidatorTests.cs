using CashFlow.Application.Services;
using CashFlow.Application.UseCases.Expenses;
using CashFlow.CommonTestUtilities.Requests;
using CashFlow.Communication.Enums;
using Moq;
using Shouldly;

namespace CashFlow.Validators.Tests.Expenses.Register;

public class RegisterExpenseValidatorTests
{
    private readonly Mock<ILocalizer> _localizerMock;

    public RegisterExpenseValidatorTests()
    {
        _localizerMock = new Mock<ILocalizer>();

        _localizerMock.Setup(x => x.GetString("TitleRequired")).Returns("The title is required.");
        _localizerMock.Setup(x => x.GetString("AmountGreaterThanZero")).Returns("The amount must be greater than zero.");
        _localizerMock.Setup(x => x.GetString("DateNotInFuture")).Returns("The date cannot be in the future.");
        _localizerMock.Setup(x => x.GetString("InvalidPaymentType")).Returns("The payment type is invalid.");
    }

    [Fact]
    public void ShouldReturnSuccessWhenExpenseIsValid()
    {
        // Arrange
        var validator = new ExpenseValidator(_localizerMock.Object);
        var request = RequestExpenseJsonBuilder.Build();

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void ShouldReturnErrorWhenTitleIsEmpty(string? title)
    {
        // Arrange
        var validator = new ExpenseValidator(_localizerMock.Object);
        var request = RequestExpenseJsonBuilder.Build();
        request.Title = title!;

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem().ErrorMessage.ShouldBe("The title is required.");
    }

    [Fact]
    public void ShouldReturnErrorWhenDateIsInFuture()
    {
        // Arrange
        var validator = new ExpenseValidator(_localizerMock.Object);
        var request = RequestExpenseJsonBuilder.Build();
        request.Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1));

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem().ErrorMessage.ShouldBe("The date cannot be in the future.");
    }

    [Fact]
    public void ShouldReturnErrorWhenPaymentTypeIsInvalid()
    {
        // Arrange
        var validator = new ExpenseValidator(_localizerMock.Object);
        var request = RequestExpenseJsonBuilder.Build();
        request.PaymentType = (PaymentType)99;

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem().ErrorMessage.ShouldBe("The payment type is invalid.");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void ShouldReturnErrorWhenAmountIsLessOrEqualToZero(decimal amount)
    {
        // Arrange
        var validator = new ExpenseValidator(_localizerMock.Object);
        var request = RequestExpenseJsonBuilder.Build();
        request.Amount = amount;

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldHaveSingleItem().ErrorMessage.ShouldBe("The amount must be greater than zero.");
    }
}
