using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TripDatePlanner.Exceptions;

namespace TripDatePlanner.Filters;

public sealed class ExceptionsFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case EntityNotFoundException enfe:
                HandleEntityNotFoundException(context, enfe);
                break;
            case ArgumentException ae:
                HandleArgumentException(context, ae);
                break;
            case OperationCanceledException oce:
                HandleOperationCancelledException(context, oce);
                break;
        }
    }

    private static void HandleEntityNotFoundException(ExceptionContext context, EntityNotFoundException enfe)
    {
        context.Result = new NotFoundObjectResult(enfe.Message);
        Finalize(context);
    }

    private static void HandleArgumentException(ExceptionContext context, ArgumentException ae)
    {
        context.Result = new BadRequestObjectResult(ae.Message);
        Finalize(context);
    }

    private static void HandleOperationCancelledException(ExceptionContext context, OperationCanceledException oce)
    {
        context.Result = new OkObjectResult(oce.Message);
        Finalize(context);
    }

    private static void Finalize(ExceptionContext context)
    {
        context.ExceptionHandled = true;
    }
}