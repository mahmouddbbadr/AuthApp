using AuthApp.Infrastructure.LogExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AuthApp.Infrastructure.Middlewares
{
    public class GlobalException(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext httpContext)
        {
            string title = "Error";
            string message = "Internal server error occurred, try again";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            try
            {
                await next(httpContext);
                if(httpContext.Response.StatusCode == StatusCodes.Status429TooManyRequests)
                {
                    title = "Warning";
                    message = "Too many requests were made, try again later";
                    statusCode = StatusCodes.Status429TooManyRequests;
                    await ModifyHeader(httpContext, title, message, statusCode);
                }

                if(httpContext.Response.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    title = "Alert";
                    message = "You are unauthorized to access";
                    statusCode = StatusCodes.Status401Unauthorized;
                    await ModifyHeader(httpContext, title, message, statusCode);
                }

                if (httpContext.Response.StatusCode == StatusCodes.Status403Forbidden)
                {
                    title = "Out of access";
                    message = "You are not allowed/required to access";
                    statusCode = StatusCodes.Status403Forbidden;
                    await ModifyHeader(httpContext, title, message, statusCode);
                }

            }
            catch(Exception ex) 
            {
                LogException.LogEx(ex);
            }
        }

        private static async Task ModifyHeader(HttpContext httpContext, string title, string message, int statusCode)
        {
            httpContext.Request.ContentType = "application/json";
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails()
            {
                Title = title,
                Detail = message,
                Status = statusCode
            }), CancellationToken.None);
            return;
        }
    }
}
