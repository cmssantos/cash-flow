using CashFlow.Application.Services;
using CashFlow.Communication.Responses;
using CashFlow.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CashFlow.Api.Filters;

public class ExceptionFilter(ILocalizer localizer) : IExceptionFilter
{
    private readonly ILocalizer _localizer = localizer;

    public void OnException(ExceptionContext context)
    {
        if (context.Exception is CashFlowException)
        {
            HandleApplicationException(context);
        }
        else
        {
            ThrowUnhandledException(context);
        }
    }

    private static void HandleApplicationException(ExceptionContext context)
    {
        var cashFlowException = context.Exception as CashFlowException;
        var errorResponse = new ResponseErrorJson(cashFlowException!.GetErrors());

        context.HttpContext.Response.StatusCode = cashFlowException.StatusCode;
        context.Result = new ObjectResult(errorResponse);
    }

    private void ThrowUnhandledException(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson(_localizer.GetString("Error.ServerError"));

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
