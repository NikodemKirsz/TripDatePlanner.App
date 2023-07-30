using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TripDatePlanner.Exceptions;

namespace TripDatePlanner.Filters;

public sealed class ExceptionsFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionsFilter> _logger;

    public ExceptionsFilter(ILogger<ExceptionsFilter> logger)
    {
        _logger = logger;
    }

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
            case Exception e:
                HandleException(context, e);
                break;
        }
    }

    private void HandleEntityNotFoundException(ExceptionContext context, EntityNotFoundException enfe)
    {
        context.Result = new NotFoundObjectResult(enfe.Message);
        
        _logger.LogWarning(enfe, 
            "Entity Not Found occured for request path '{Path}'!",
            context.HttpContext.Request.Path
        );
        Finalize(context);
    }

    private void HandleArgumentException(ExceptionContext context, ArgumentException ae)
    {
        context.Result = new BadRequestObjectResult(ae.Message);
        
        _logger.LogWarning(ae, 
            "Argument Exception occured for request path '{Path}'!",
            context.HttpContext.Request.Path
        );
        Finalize(context);
    }

    private void HandleOperationCancelledException(ExceptionContext context, OperationCanceledException oce)
    {
        context.Result = new OkObjectResult(oce.Message);
        
        _logger.LogInformation(oce, 
            "Operation cancelled occured for request path '{Path}'!",
            context.HttpContext.Request.Path
        );
        Finalize(context);
    }

    private void HandleException(ExceptionContext context, Exception e)
    {
        context.Result = new InternalServerErrorResult();
        
        _logger.LogError(e, "Unhandled error occured for request path '{Path}'!",
            context.HttpContext.Request.Path
        );
        Finalize(context);
    }

    private static void Finalize(ExceptionContext context)
    {
        context.ExceptionHandled = true;
    }
}