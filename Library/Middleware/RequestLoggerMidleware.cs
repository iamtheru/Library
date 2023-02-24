using NLog;

namespace Library.Middleware
{
	public class RequestLoggerMidleware
	{
		private readonly RequestDelegate _next;

		public RequestLoggerMidleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			var logger = LogManager.GetCurrentClassLogger();
			logger.Info($"Request: {context.Request.Method} {context.Request.Path} {context.Request.QueryString}");
			logger.Info($"Headers: {string.Join(", ", context.Request.Headers)}");
			logger.Info($"Query Params: {string.Join(", ", context.Request.Query)}");

			if (context.Request.ContentLength.HasValue && context.Request.ContentLength.Value > 0)
			{
				context.Request.EnableBuffering();
				var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
				logger.Info($"Body: {body}");
				context.Request.Body.Position = 0;
			}

			await _next.Invoke(context);

			logger.Info($"Response: {context.Response.StatusCode}");
		}
	}
}
