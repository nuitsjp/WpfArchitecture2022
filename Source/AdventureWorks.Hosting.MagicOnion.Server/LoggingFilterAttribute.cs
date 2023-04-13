﻿using AdventureWorks.Authentication;
using MagicOnion.Server;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;

namespace AdventureWorks.Hosting.MagicOnion.Server
{
    public class LoggingFilterAttribute : MagicOnionFilterAttribute
    {
        private readonly ILogger<LoggingFilterAttribute> _logger;
        private readonly IAuthenticationContext _authenticationContext;

        public LoggingFilterAttribute(
            ILogger<LoggingFilterAttribute> logger, 
            IAuthenticationContext authenticationContext)
        {
            _logger = logger;
            _authenticationContext = authenticationContext;
        }

        public override ValueTask Invoke(ServiceContext context, Func<ServiceContext, ValueTask> next)
        {
            try
            {
                _logger.LogInformation("Method:{Method} Peer:{Peer} EmployeeId:{EmployeeId}", context.CallContext.Method, context.CallContext.Peer, _authenticationContext.CurrentUser.EmployeeId);

                return next(context);
            }
            catch (Exception e)
            {
                _logger.LogInformation("Method:{Method} Peer:{Peer}", context.CallContext.Method, context.CallContext.Peer);

                _logger.LogError(e.Message, e);
                throw;
            }
        }
    }
}