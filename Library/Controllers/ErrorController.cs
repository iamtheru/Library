using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Library.Controllers
{
	[Route("api/")]
	[ApiController]
	public class ErrorController : ControllerBase
	{
		[HttpGet("error")]
		public IActionResult ErrorAsync()
		{
			var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
			if (context != null)
			{
				var exception = context.Error;
				var logger = LogManager.GetCurrentClassLogger();
				logger.Error(exception);
			}

			return Problem();
		}
	}
}
