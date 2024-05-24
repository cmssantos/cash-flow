using Cms.CashFlow.Communication.Requests;
using Cms.CashFlow.Communication.Responses;
using Cms.CashFlow.Exception.ExceptionsBase;

namespace Cms.CashFlow.Application.UseCases.Expenses.Register;

public class RegisterExpenseUseCase
{
    public ResponseRegisterExpense Execute(RequestRegisterExpense request)
    {
        Validate(request);

        return new ResponseRegisterExpense();
    }

    private static void Validate(RequestRegisterExpense request)
    {
        var validator = new RegisterExpenseValidator();

        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
