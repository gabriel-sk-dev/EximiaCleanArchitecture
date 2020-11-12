using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Eximia.CleanArchitecture.SeedWork;

namespace Eximia.CleanArchitecture.WebAPI.Infraestrutura.MediatRBehaviors
{
    public class EnrichLogContextBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<EnrichLogContextBehavior<TRequest, TResponse>> _logger;

        public EnrichLogContextBehavior(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<EnrichLogContextBehavior<TRequest, TResponse>>();
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TResponse resposta;
            if (request.GetType().GetInterfaces().All(c => c != typeof(ICommand)))
                return await next();
            var requisicao = (ICommand)request;
            using (LogContext.PushProperty("CommandId", requisicao.Id.ToString()))
            using (LogContext.PushProperty("ParentCommandId", requisicao.ParentId.ToString()))
            {
                resposta = await next();
            }
            return resposta;
        }
    }
}
