using System.Resources;
using Cms.CashFlow.Communication.Responses;
using Cms.CashFlow.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cms.CashFlow.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    private readonly ResourceManager _resourceManager;

    public ExceptionFilter() =>
        _resourceManager = new ResourceManager("Cms.CashFlow.Exception.Resources.ResourceErrorMessages", typeof(CashFlowException).Assembly);

    public void OnException(ExceptionContext context)
    {
        if (context.Exception is CashFlowException)
        {
            HandleProjectException(context);
        }
        else
        {
            ThrowUnknownError(context);
        }

    }

    private static void HandleProjectException(ExceptionContext context)
    {
        if (context.Exception is ErrorOnValidationException ex)
        {
            var errorResponse = new ResponseError(ex.Errors);

            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Result = new BadRequestObjectResult(errorResponse);
        }
        else
        {
            var errorResponse = new ResponseError(context.Exception.Message);

            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Result = new BadRequestObjectResult(errorResponse);
        }
    }

    private void ThrowUnknownError(ExceptionContext context)
    {
        var errorResponse = new ResponseError(_resourceManager.GetString("InternalServerError") ?? "INTERNAL_SERVER_ERROR_");

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
