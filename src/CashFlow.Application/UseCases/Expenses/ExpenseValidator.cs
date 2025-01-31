using CashFlow.Application.Services;
using CashFlow.Communication.Requests;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses;

public class ExpenseValidator : AbstractValidator<RequestExpenseJson>
{
    public ExpenseValidator(ILocalizer localizer)
    {
        RuleFor(expense => expense.Title)
            .NotEmpty()
            .WithMessage(localizer.GetString("TitleRequired"));

        RuleFor(expense => expense.Amount)
            .GreaterThan(0)
            .WithMessage(localizer.GetString("AmountGreaterThanZero"));

        RuleFor(expense => expense.Date)
            .Must(date => date <= DateOnly.FromDateTime(DateTime.Now))
            .WithMessage(localizer.GetString("DateNotInFuture"));

        RuleFor(expense => expense.PaymentType)
            .IsInEnum()
            .WithMessage(localizer.GetString("InvalidPaymentType"));
    }
}
