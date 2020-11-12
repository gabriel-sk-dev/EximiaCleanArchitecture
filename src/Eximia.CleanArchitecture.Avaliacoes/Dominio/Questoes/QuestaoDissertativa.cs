using CSharpFunctionalExtensions;
using Eximia.CleanArchitecture.SeedWork;

namespace Eximia.CleanArchitecture.Avaliacoes.Dominio.Questoes
{
    public sealed class QuestaoDissertativa : Questao
    {
        private QuestaoDissertativa(){}
        private QuestaoDissertativa(in int id, in int nivelEnsinoId, in int disciplinaId, in string descricao)
            : base(id, nivelEnsinoId, disciplinaId, descricao)
        {
            
        }

        internal static Result<QuestaoDissertativa> Criar(in int nivelEnsinoId, in int disciplinaId, in string descricao, int id = 0)
        {
            var (_, isFailure, error) = Result.Combine(
                nivelEnsinoId.MustBeGratherThanZero(QuestoesMotivosErro.NivelEnsinoObrigatorio),
                disciplinaId.MustBeGratherThanZero(QuestoesMotivosErro.DisciplinaObrigatoria),
                descricao.MustNotBeNullOrEmpty(QuestoesMotivosErro.DescricaoObrigatoria));
            return isFailure 
                ? Result.Failure<QuestaoDissertativa>(error) 
                : Result.Success(new QuestaoDissertativa(id, nivelEnsinoId, disciplinaId, descricao));
        }
    }
}