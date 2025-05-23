using Ecommerce_BE.Middleware.Base;
using Ecommerce_BE.Middleware.Enum;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_BE.Middleware.Middlewares
{
    public class ApiResponseWrapperMiddleware
    {
        private readonly RequestDelegate _next;
        public ApiResponseWrapperMiddleware(RequestDelegate next) => _next = next;
        public async Task Invoke(HttpContext context)
        {
            if (IsSwagger(context))
            {
                await _next.Invoke(context);
                return;
            }
            else
            {
                var originalBodyStream = context.Response.Body;

                using (var responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;

                    try
                    {
                        await _next.Invoke(context);

                        responseBody.Seek(0, SeekOrigin.Begin);
                        var bodyText = await new StreamReader(responseBody).ReadToEndAsync();

                        context.Response.Body = originalBodyStream;
                        context.Response.ContentType = "application/json";

                        if (context.Response.StatusCode == (int)HttpStatusCode.OK)
                        {
                            await HandleSuccessRequestAsync(context, bodyText, context.Response.StatusCode);
                        }
                        else
                        {
                            await HandleNotSuccessRequestAsync(context, context.Response.StatusCode);
                        }
                    }
                    catch (Exception ex)
                    {
                        context.Response.Body = originalBodyStream;
                        await HandleExceptionAsync(context, ex);
                    }
                }
            }
        }


        private static Task HandleExceptionAsync(HttpContext context, System.Exception exception)
        {
            BaseApiError? apiError = null;
            BaseApiResponse? apiResponse = null;
            int code = 0;

            if (exception is BaseApiException)
            {
                var ex = exception as BaseApiException;
                apiError = new BaseApiError(ex.Message);
                apiError.ValidationErrors = ex.Errors;
                apiError.ReferenceErrorCode = ex.ReferenceErrorCode;
                apiError.ReferenceDocumentLink = ex.ReferenceDocumentLink;
                code = ex.StatusCode;
                context.Response.StatusCode = code;
            }
            else if (exception is UnauthorizedAccessException)
            {
                apiError = new BaseApiError("Unauthorized Access");
                code = (int)HttpStatusCode.Unauthorized;
                context.Response.StatusCode = code;
            }
            else
            {

                var msg = exception.GetBaseException().Message;
                string stack = exception.StackTrace;

                apiError = new BaseApiError(msg);
                apiError.Details = stack;
                code = (int)HttpStatusCode.InternalServerError;
                context.Response.StatusCode = code;
            }

            context.Response.ContentType = "application/json";

            apiResponse = new BaseApiResponse(ResponseMessageEnum.Exception.GetDescription(), apiError, false, code, context.Request.Path);

            var json = JsonConvert.SerializeObject(apiResponse);

            return context.Response.WriteAsync(json);
        }

        private static async Task HandleNotSuccessRequestAsync(HttpContext context, int code)
        {

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = code;

            if (code == (int)HttpStatusCode.NoContent)
            {
                return;
            }

            BaseApiError apiError = code switch
            {
                (int)HttpStatusCode.NotFound => new BaseApiError("The specified URI does not exist. Please verify and try again."),
                _ => new BaseApiError("Your request cannot be processed. Please contact a support.")
            };

            var apiResponse = new BaseApiResponse(ResponseMessageEnum.Failure.GetDescription(), apiError, false, code, context.Request.Path);

            var json = JsonConvert.SerializeObject(apiResponse);

            await context.Response.WriteAsync(json);
        }

        private static Task HandleSuccessRequestAsync(HttpContext context, object body, int code)
        {
            context.Response.ContentType = "application/json";
            BaseApiResponse? apiResponse = null;

            if (body is string bodyText && bodyText.IsValidJson())
            {
                var data = JsonConvert.DeserializeObject<object>(bodyText);
                apiResponse = new BaseApiResponse(
                    ResponseMessageEnum.Success.GetDescription(),
                    data ?? new object(), 
                    true,
                    code,
                    context.Request.Path
                );
            }
            else
            {
                apiResponse = new BaseApiResponse(
                    ResponseMessageEnum.Success.GetDescription(),
                    body ?? new object(),
                    true,
                    code,
                    context.Request.Path
                );
            }

            var json = JsonConvert.SerializeObject(apiResponse);
            return context.Response.WriteAsync(json);
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var plainBodyText = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return plainBodyText;
        }

        private bool IsSwagger(HttpContext context)
        {
            return context.Request.Path.StartsWithSegments("/swagger");
        }

    }
    public static class JsonExtensions
    {
        public static bool IsValidJson(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            try
            {
                JsonConvert.DeserializeObject<object>(str);
                return true;
            }
            catch (JsonException)
            {
                return false;
            }
        }
    }
}
