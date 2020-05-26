using ScattergoriesBackend.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static ScattergoriesBackend.Helpers.ApiLogger;

namespace ScattergoriesBackend.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.Log($"{context.Request.Path} resulted in exception {ex}", LogSeverity.ERROR);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var result = JsonConvert.SerializeObject(new { message = ex.Message });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(result);
        }
    }
}