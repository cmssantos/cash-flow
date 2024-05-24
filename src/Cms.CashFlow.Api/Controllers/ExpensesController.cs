using Cms.CashFlow.Application.UseCases.Expenses.Register;
using Cms.CashFlow.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Cms.CashFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
    [HttpPost]
    public IActionResult Register([FromBody] RequestRegisterExpense request)
    {
        var useCase = new RegisterExpenseUseCase();

        var response = useCase.Execute(request);

        return Created(string.Empty, response);
    }
}
