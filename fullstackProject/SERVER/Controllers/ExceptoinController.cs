using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BL.Exceptions;
namespace SERVER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExceptionController : ControllerBase
    {
        [HttpGet("/error")]
        [HttpPost("/error")]
        [HttpPut("/error")]
        [HttpDelete("/error")]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerFeature>();
            // כתיבת השגיאה ללוג
            if (exceptionDetails != null)
            {

            }
            if (exceptionDetails?.Error is ClientNotExistException notExistExc)
            {
                ;
                return Problem(
                detail: exceptionDetails?.Error.Message,
                title: "The client is not exist",
                statusCode: notExistExc.StatusCode
                );
            }

            return Problem(
               detail: "Please restart the website agein",
               title: "An error occurred",
               statusCode: 500
           );
        }
    }
}
