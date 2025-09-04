using BuildingBlocks.Response;
using FluentValidation;
using MassTransit.Internals.GraphValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions.Handler
{
    public class CustomExpectionHandler(ILogger<CustomExpectionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(
                    "错误信息: {exceptionMessage}, 发生时间 {time}",
                    exception.Message, DateTime.Now);
            (int code, int StatusCode) details = exception switch
            {
                InternalSeverExpection =>
                (
                    1001,
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError
                ),
                ValidationException =>
                (
                    1002,
                    context.Response.StatusCode = StatusCodes.Status400BadRequest
                ),
                BadRequestExpection =>
                (
                    1003,
                    context.Response.StatusCode = StatusCodes.Status400BadRequest
                ),
                NotFoundExpection =>
                (
                    1004,
                    context.Response.StatusCode = StatusCodes.Status404NotFound
                ),
                _ =>
                (
                    1005,
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError
                )
            };
            string message = exception.Message;

            object? data = null;

            // 如果是 ValidationException，可以把具体错误放到 data 里
            if (exception is ValidationException validationException)
            {
                data = validationException.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
            }

            var response = new BaseResponse
            {
                code = details.code,
                message = message,
                data = data
            };

            context.Response.StatusCode = details.StatusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(response, cancellationToken: cancellationToken);
            return true;
        }
    }
}
