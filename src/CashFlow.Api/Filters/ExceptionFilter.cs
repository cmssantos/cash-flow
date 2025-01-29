using CashFlow.Application.Interfaces;
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
        if (context.Exception is ErrorOnValidationException ex)
        {
            var errorResponse = new ResponseErrorJson(ex.Errors);

            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Result = new BadRequestObjectResult(errorResponse);
        }
        else
        {
            var errorResponse = new ResponseErrorJson(context.Exception.Message);

            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Result = new BadRequestObjectResult(errorResponse);
        }
    }

    private void ThrowUnhandledException(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson(_localizer.GetString("Error.ServerError"));

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
