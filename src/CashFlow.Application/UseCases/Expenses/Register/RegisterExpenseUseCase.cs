using CashFlow.Application.Services;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register;

internal class RegisterExpenseUseCase(IExpensesRepository repository, ILocalizer localizer) : IRegisterExpenseUseCase
{
    private readonly IExpensesRepository _repository = repository;
    private readonly ILocalizer _localizer = localizer;

    public ResponseRegisteredExpenseJson Execute(RequestRegisterExpenseJson request)
    {
        Validade(request);

        var entity = new Expense
        {
            Title = request.Title,
            Description = request.Description,
            Amount = request.Amount,
            Date = request.Date,
            PaymentType = (Domain.Enums.PaymentType)request.PaymentType,
        };

        _repository.Add(entity);

        return new ResponseRegisteredExpenseJson();
    }

    private void Validade(RequestRegisterExpenseJson request)
    {
        var validator = new RegisterExpenseValidator(_localizer);
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors
                .Select(error => error.ErrorMessage)
                .ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
