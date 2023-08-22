using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using VotingSystemBigBrotherBrasil.Publisher.Models.Responses;

namespace VotingSystemBigBrotherBrasil.Publisher.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private const string CONTENT_TYPE = "application/json";

        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleException(httpContext, ex);
            }
        }

        private async Task HandleException(HttpContext context, Exception ex)
        {
            var response = new BaseHttpResponse()
            {
                Erros = new string[]
                {
                    ex.Message
                }
            };

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = CONTENT_TYPE;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
