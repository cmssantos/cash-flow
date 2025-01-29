using CashFlow.Application.Interfaces;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController(ILocalizer localizer) : ControllerBase
{
    private readonly ILocalizer _localizer = localizer;

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public IActionResult Register([FromBody] RequestRegisterExpenseJson request)
    {
        var useCase = new RegisterExpenseUseCase(_localizer).Execute(request);

        return Created(string.Empty, useCase);
    }
}
