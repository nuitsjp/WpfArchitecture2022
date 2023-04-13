using AdventureWorks.Authentication;
using MagicOnion.Server;
using Microsoft.Extensions.Logging;

namespace AdventureWorks.MagicOnion.Server
{
    public class LoggingFilterAttribute : MagicOnionFilterAttribute
    {
        private readonly ILogger<LoggingFilterAttribute> _logger;

        public LoggingFilterAttribute(
            ILogger<LoggingFilterAttribute> logger)
        {
            _logger = logger;
        }

        public override ValueTask Invoke(ServiceContext context, Func<ServiceContext, ValueTask> next)
        {
            try
            {
                _logger.LogInformation($"{context.CallContext.Method} Peer:{context.CallContext.Peer}");

                return next(context);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"{context.CallContext.Method} Peer:{context.CallContext.Peer}");

                _logger.LogError(e.Message, e);
                throw;
            }
        }
    }
}