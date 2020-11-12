using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using Eximia.CleanArchitecture.SeedWork;

namespace Eximia.CleanArchitecture.Avaliacoes.Dominio.Questoes
{
    public sealed class QuestaoObjetiva : Questao
    {
        private readonly List<Opcao> _opcoes;

        private QuestaoObjetiva(){}
        private QuestaoObjetiva(in int id, in int nivelEnsinoId, in int disciplinaId, in string descricao, in List<Opcao> opcoes)
            : base(id, nivelEnsinoId, disciplinaId, descricao)
        {
            _opcoes = opcoes ?? new List<Opcao>();
        }

        public IEnumerable<Opcao> Opcoes => _opcoes.AsReadOnly();

        internal static Result<QuestaoObjetiva> Criar(in int nivelEnsinoId, in int disciplinaId, in string descricao, List<Opcao> opcoes, int id = 0)
        {
            var (_, isFailure, error) = Result.Combine(
                nivelEnsinoId.MustBeGratherThanZero(QuestoesMotivosErro.NivelEnsinoObrigatorio),
                disciplinaId.MustBeGratherThanZero(QuestoesMotivosErro.DisciplinaObrigatoria),
                descricao.MustNotBeNullOrEmpty(QuestoesMotivosErro.DescricaoObrigatoria),
                opcoes.MustNotBeEmpty(QuestoesMotivosErro.OpcoesObrigatorio),
                opcoes.MustBeAny(c => c.Correta, QuestoesMotivosErro.AoMinimoUmaOpcaoCorreta));

            return isFailure
                ? Result.Failure<QuestaoObjetiva>(error)
                : Result.Success(new QuestaoObjetiva(id, nivelEnsinoId, disciplinaId, descricao, opcoes));
        }
    }
}