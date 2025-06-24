using System.Text.Json;
using BL.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace SERVER.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // הרצת הבקשה הרגילה
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex); // טיפול בשגיאות
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int statusCode = 500;
            string title = "Internal Server Error";
            string message = "Something went wrong.";

            // טיפול מותאם לסוגי שגיאות שלך
            switch (exception)
            {
                case ClientNotExistException e:
                    statusCode = e.StatusCode;
                    title = "Client Not Found";
                    message = e.Message;
                    break;
                case ClientAlradyExistException e:
                    statusCode = e.StatusCode;
                    title = "Client Already Exists";
                    message = e.Message;
                    break;
                case specializationNotExistException e:
                    statusCode = e.StatusCode;
                    title = "Specialization Not Found";
                    message = e.Message;
                    break;
                case DoctorNotExistException e:
                    statusCode = e.StatusCode;
                    title = "Doctor Not Found";
                    message = e.Message;
                    break;
                case DoctorAlradyExistException e:
                    statusCode = e.StatusCode;
                    title = "Doctor Already Exists";
                    message = e.Message;
                    break;
                case IncompatibleOrIincompleteValuesException e:
                    statusCode = e.StatusCode;
                    title = "Invalid Values";
                    message = e.Message;
                    break;
                case AvailableQueueNotFoundException e:
                    statusCode = e.StatusCode;
                    title = "No Available Queues";
                    message = e.Message;
                    break;
            }
            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = message,
                Instance = context.Request.Path
            };

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = statusCode;

            var json = JsonSerializer.Serialize(problemDetails);
            return context.Response.WriteAsync(json);
        }
    }
}
