using Core.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Exception
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Request'i bir sonraki middleware'e ilet
                await _next(context);
            }
            catch (System.Exception ex)
            {
                // Exception oluşursa logla ve hata yanıtı gönder
                LogException(context, ex);
                await HandleExceptionAsync(context, ex);
            }
        }

        private void LogException(HttpContext context, System.Exception ex)
        {

        }

        private static Task HandleExceptionAsync(HttpContext context, System.Exception ex)
        { 
            context.Response.ContentType = "application/json";
            var response = new ApiResponse<object>();
            string serverError = "Internal Server Error";

            if (ex is BusinessException businessException)
            {
                context.Response.StatusCode = (int)businessException.StatusCode;
                response.Fail(error: businessException.Message, statusCode: (int)businessException.StatusCode);
            }
            else if (ex is BadRequestException badRequest)
            {
                context.Response.StatusCode = (int)badRequest.StatusCode;
                response.Fail(error: badRequest.Message, statusCode: (int)badRequest.StatusCode);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Fail(error: serverError, StatusCodes.Status500InternalServerError);
            }
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
