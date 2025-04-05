using System.Net;
using Company.Api.Contracts.Responses;
using Company.Api.Domain.Common.Exceptions;
using Company.Api.Utils.Extensions;
using FluentValidation;

namespace Company.Api.Validation;

public class ValidationExceptionMiddleware(RequestDelegate request)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await request(context);
        }
        catch (UniqueViolationException e)
        {
            await SendErrorAsync(e, HttpStatusCode.BadRequest);
        }
        catch (ValidationException e)
        {
            await SendErrorAsync(e, HttpStatusCode.BadRequest);
        }
        catch (Exception e)
        {
            //todo: log exception details w/ the correlation id so we can troubleshoot without exposing sensitive info
            context.TraceIdentifier = context.TraceIdentifier.IsNullOrEmpty() 
                ? Guid.NewGuid().ToString()
                : context.TraceIdentifier;

            var failureResponse= new ValidationException(
                $"An unexpected error occurred. Please contact the support providing the following identifier: {context.TraceIdentifier}");

            await SendErrorAsync(failureResponse, HttpStatusCode.InternalServerError);
        }

        return;

        async Task SendErrorAsync(ValidationException ex, HttpStatusCode statusCode)
        {
            context.Response.StatusCode = (int)statusCode;
            var messages = ex.Errors
                .Select(x => $"{x.PropertyName}:{x.ErrorMessage}" )
                .ToList();

            var validationFailureResponse = new FailureResponse
            {
                Errors = messages
            };

            await context.Response.WriteAsJsonAsync(validationFailureResponse);
        }
    }
}