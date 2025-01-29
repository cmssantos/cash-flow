using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Exception.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase
{
    public ResponseRegisteredExpenseJson Execute(RequestRegisterExpenseJson request)
    {
        Validade(request);

        // Business logic here
        return new ResponseRegisteredExpenseJson
        {
            Title = request.Title
        };
    }

    private static void Validade(RequestRegisterExpenseJson request)
    {
        var validator = new RegisterExpenseValidator();
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
