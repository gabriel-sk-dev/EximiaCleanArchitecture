using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Eximia.CleanArchitecture.Avaliacoes.Dominio.Disciplinas;
using Eximia.CleanArchitecture.Avaliacoes.Dominio.NiveisEnsino;
using Eximia.CleanArchitecture.Avaliacoes.Dominio.Questoes;
using MediatR;
using Result = CSharpFunctionalExtensions.Result;

namespace Eximia.CleanArchitecture.Avaliacoes.Aplicacao.Comandos
{
    public sealed class CriarQuestaoObjetivaComandoHandler : IRequestHandler<CriarQuestaoObjetivaComando, Result<int>>
    {
        private readonly IDisciplinasRepositorio _disciplinasRepositorio;
        private readonly INiveisEnsinoRepositorio _niveisEnsinoRepositorio;
        private readonly IQuestoesRepositorio _opcoesRepositorio;

        public CriarQuestaoObjetivaComandoHandler(
            IDisciplinasRepositorio disciplinasRepositorio,
            INiveisEnsinoRepositorio niveisEnsinoRepositorio,
            IQuestoesRepositorio opcoesRepositorio)
        {
            _disciplinasRepositorio = disciplinasRepositorio;
            _niveisEnsinoRepositorio = niveisEnsinoRepositorio;
            _opcoesRepositorio = opcoesRepositorio;
        }

        public async Task<Result<int>> Handle(CriarQuestaoObjetivaComando request, CancellationToken cancellationToken)
        {
           var disciplina = await _disciplinasRepositorio.RecuperarAsync(request.DisciplinaId, cancellationToken);
            if (disciplina.HasNoValue)
                return Result.Failure<int>(QuestoesMotivosErro.DisciplinaNaoFoiLocalizada);

            var nivelEnsino = await _niveisEnsinoRepositorio.RecuperarAsync(request.NivelEnsinoId, cancellationToken);
            if (nivelEnsino.HasNoValue)
                return Result.Failure<int>(QuestoesMotivosErro.NivelEnsinoNaoLocalizado);

            var opcoes = request.Opcoes.Select(c => c.Correta
                    ? Opcao.CriarCorreta(c.Texto)
                    : Opcao.CriarIncorreta(c.Texto))
                .ToList();
            var (_, opcoesisFailure, opcoesError) = Result.Combine(opcoes);
            if (opcoesisFailure)
                return Result.Failure<int>(opcoesError);

            var (_, isFailure, questao, error) = QuestaoObjetiva.Criar(disciplina.Value.Id, nivelEnsino.Value.Id, request.Descricao,
                opcoes.Select(c => c.Value).ToList());
            if (isFailure)
                return Result.Failure<int>(error);

            await _opcoesRepositorio.AdicionarAsync(questao, cancellationToken);
            await _opcoesRepositorio.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return Result.Success(questao.Id);
        }
    }
}