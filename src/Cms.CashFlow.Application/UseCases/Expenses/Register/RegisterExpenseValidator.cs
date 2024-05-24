using System.Resources;
using Cms.CashFlow.Communication.Requests;
using Cms.CashFlow.Exception.ExceptionsBase;
using FluentValidation;

namespace Cms.CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseValidator : AbstractValidator<RequestRegisterExpense>
{
    private readonly ResourceManager _resourceManager;

    public RegisterExpenseValidator()
    {
        _resourceManager = new ResourceManager("Cms.CashFlow.Exception.Resources.ResourceErrorMessages", typeof(CashFlowException).Assembly);

        RuleFor(expense => expense.Title)
            .NotEmpty().WithMessage(_resourceManager.GetString("TitleRequired"));

        RuleFor(expense => expense.Amount)
            .GreaterThan(0).WithMessage(_resourceManager.GetString("AmountMustBeGreaterThanZero"));

        RuleFor(expense => expense.Date)
            .NotEmpty().WithMessage("The date is required")
            .Must(BeAValidDate).WithMessage(_resourceManager.GetString("DateInvalid"));

        RuleFor(expense => expense.PaymentMethod)
            .IsInEnum().WithMessage(_resourceManager.GetString("PaymentMethodInvalid"));
    }

    private bool BeAValidDate(string date) => DateOnly.TryParse(date, out _);
}
