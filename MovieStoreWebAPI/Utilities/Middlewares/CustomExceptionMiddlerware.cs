﻿using MovieStoreWebAPI.Utilities.Logger.Abstract;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Text.Json.Serialization;

namespace MovieStoreWebAPI.Utilities.Middlewares
{
    public class CustomExceptionMiddlerware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;

        public CustomExceptionMiddlerware(RequestDelegate next, ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }
        public async Task Invoke(HttpContext context)
        {
            var watch=Stopwatch.StartNew();
            try
            {
                string message = $@"[Request] HTTP {context.Request.Method} ""{context.Request.Path}""";
                _loggerService.Write(message);

                await _next(context);

                watch.Stop();
                message = @$"[Response] HTTP {context.Request.Method} ""{context.Request.Path}"" responded. " +
                    $"StatusCode: {context.Response.StatusCode} {(HttpStatusCode)context.Response.StatusCode}. " +
                    $"Time: {watch.Elapsed.TotalSeconds}ms.";
                _loggerService.Write(message);

            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleException(context, ex, watch);
            }
        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {
            var exceptionType = ex.GetType().Name;
            FixResponse(context, exceptionType);
            string message=@$"[Error] HTTP {context.Request.Method} ""{context.Request.Path}"" - "+
                $"StatusCode: {context.Response.StatusCode} {(HttpStatusCode)context.Response.StatusCode}. "+
                $"Error Message: {ex.Message}. "+
                $"Time: {watch.Elapsed.TotalMilliseconds}ms.";
            _loggerService.Write(message);

            var result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);
            return context.Response.WriteAsync(result);
        }

        private void FixResponse(HttpContext context, string exceptionType)
        {
            context.Response.ContentType = "application/json";
            if (exceptionType=="ValidationException")
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

        }
    }
}
