using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Eximia.CleanArchitecture.Avaliacoes.Aplicacao.Comandos;
using Eximia.CleanArchitecture.WebAPI.Infraestrutura.ActionResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Eximia.CleanArchitecture.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestoesController : ControllerBase
    {
        private readonly ILogger<QuestoesController> _logger;
        private readonly IMediator _mediator;

        public QuestoesController(
            ILogger<QuestoesController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CriarQuestaoObjetiva([FromBody] CriarQuestaoObjetivaComando comando)
        {
            var (_, isFailure, questaoId, error) = await _mediator.Send(comando, new CancellationToken());
            if (isFailure)
                return BadRequest(ErrorObjectResult.Criar("Não foi possível criar a questão", error));
            return Ok(new { Id = questaoId });
        }
    }
}
