using System.Net;

namespace ManageProduct.Helpers
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _requestDel;
		private readonly ILogger<ExceptionMiddleware> _logger;
		private readonly IHostEnvironment _env;

		public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
		{
			_requestDel = next;
			_logger = logger;
			_env = env;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _requestDel(httpContext);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				await HandleExceptionAsync(httpContext, ex);
			}
		}

		private Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			var error = new ErrorDetails
			{
				StatusCode = context.Response.StatusCode,
				Message = _env.IsDevelopment() ? exception.ToString() : "Internal Server Error."
			};
			return context.Response.WriteAsync(error.ToString());
		}
	}
}
