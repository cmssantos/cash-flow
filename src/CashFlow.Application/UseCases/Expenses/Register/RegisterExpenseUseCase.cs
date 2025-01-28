using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

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

    private void Validade(RequestRegisterExpenseJson request)
    {
        var titleIsEmpty = string.IsNullOrWhiteSpace(request.Title);
        if (titleIsEmpty)
        {
            throw new ArgumentException("Title is required");
        }

        if (request.Amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than zero");
        }

        if (request.Date > DateOnly.FromDateTime(DateTime.Now))
        {
            throw new ArgumentException("Date cannot be in the future");
        }

        var paymentTypeIsValid = Enum.IsDefined(typeof(PaymentType), request.PaymentType);
        if (!paymentTypeIsValid)
        {
            throw new ArgumentException("Payment type is invalid");
        }
    }
}