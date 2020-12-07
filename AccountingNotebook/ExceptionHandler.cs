using AccountingNotebook.Domain;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AccountingNotebook
{
    internal class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                if (ex is DomainException)
                    await HandleDomainExceptionAsync(context, ex);
            }
        }

        private async Task HandleDomainExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            await response.WriteAsync(JsonConvert.SerializeObject(new
            {
                // customize as you need
                message = exception.Message,
                code = response.StatusCode
            }));
        }
    }
}