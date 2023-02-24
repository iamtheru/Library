using NLog;

namespace Library.Helper
{
	public class RequestLogger
	{
		public static async Task LogRequestAsync(HttpRequest request)
		{
			var logger = LogManager.GetCurrentClassLogger();

			logger.Info($"Request: {request.Method} {request.Path} {request.QueryString}");
			logger.Info($"Headers: {string.Join(", ", request.Headers)}");
			logger.Info($"Query Params: {string.Join(", ", request.Query)}");

			if (request.ContentLength.HasValue && request.ContentLength.Value > 0)
			{
				request.EnableBuffering();
				var body = await new StreamReader(request.Body).ReadToEndAsync();
				logger.Info($"Body: {body}");
				request.Body.Position = 0;
			}
		}
	}
}
