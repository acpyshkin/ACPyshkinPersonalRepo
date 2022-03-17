namespace Controllers.Controllers
{
    using System.Net;
    using Domain;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;
        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [Route("/error")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error;
            if (exception is EntityNotFoundException)
            {
                return Problem(exception.Message, null, (int)HttpStatusCode.NotFound);
            }
            else
            {
                return Problem("An unforeseen error");
            }
        }
    }
}