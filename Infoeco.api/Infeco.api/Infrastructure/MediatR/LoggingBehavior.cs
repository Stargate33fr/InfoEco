using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using System.Diagnostics;

namespace Infeco.api.Infrastructure.MediatR
{
    public class LoggingBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
         where TRequest : IRequest<TResponse>
    {
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoggingBehavior(ILoggerFactory loggerFactory, IHttpContextAccessor httpContextAccessor)
        {
            _logger = loggerFactory.CreateLogger(typeof(LoggingBehavior<,>));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var sw = Stopwatch.StartNew();

            _logger.LogInformation($"Handling {typeof(TRequest).Name}");

            var response = await next();

            sw.Stop();
            _logger.LogInformation($"Handled {typeof(TResponse).Name}");

            var statusCode = httpContext?.Response?.StatusCode;
            var level = statusCode > 499 ? LogLevel.Error : LogLevel.Information;
            _logger.LogInformation($"{level} : {httpContext?.Request?.Method} {httpContext?.Request.GetDisplayUrl()} from '{httpContext?.Connection.RemoteIpAddress}:{httpContext?.Connection.RemotePort}' responded {statusCode} in {sw.Elapsed.TotalMilliseconds:0.0000} ms");

            return response;
        }
    }
}
