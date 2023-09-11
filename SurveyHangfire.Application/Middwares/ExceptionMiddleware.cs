﻿using Microsoft.AspNetCore.Http;
using SurveyApplication.Utility.LogUtils;
using System;
using System.Threading.Tasks;

namespace Hangfire.Application.Middwares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerManager _logger;

        public ExceptionMiddleware(RequestDelegate next, ILoggerManager logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                await Task.Run(() => { HandleExceptionAsync(httpContext, ex); });
            }
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                httpContext.Request.Path = "/Error/Error404";
                await _next(httpContext);
            }
        }

        private void HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.Redirect("/Error/Error500");

        }
    }
}
