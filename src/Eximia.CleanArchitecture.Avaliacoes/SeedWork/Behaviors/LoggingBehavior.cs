using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace Eximia.CleanArchitecture.SeedWork.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<LoggingBehavior<TRequest, TResponse>>();
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TResponse resposta;
            if (request.GetType().GetInterfaces().All(c => c != typeof(ICommand)))
                return await next();
            var requisicao = (ICommand)request;
            //LOG IN
            resposta = await next();
            //LOG OUT
            return resposta;
        }
    }
}