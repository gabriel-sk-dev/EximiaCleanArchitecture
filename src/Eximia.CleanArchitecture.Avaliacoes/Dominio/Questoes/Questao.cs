using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Eximia.CleanArchitecture.Avaliacoes.Dominio.NiveisEnsino;
using Eximia.CleanArchitecture.SeedWork;

namespace Eximia.CleanArchitecture.Avaliacoes.Dominio.Questoes
{
    public abstract class Questao : Entity<int>, IAggregateRoot
    {
        protected Questao(){}
        protected Questao(in int id, in int nivelEnsinoId, in int disciplinaId, in string descricao) : base(id)
        {
            NivelEnsinoId = nivelEnsinoId;
            DisciplinaId = disciplinaId;
            Descricao = descricao;
        }
        public int NivelEnsinoId { get; }
        public int DisciplinaId { get; }
        public string Descricao { get; }

        public static Result<QuestaoObjetiva> CriarObjetiva(in int nivelEnsinoId, in int disciplinaId,
            in string descricao, in List<Opcao> opcoes, int id = 0)
            => QuestaoObjetiva.Criar(nivelEnsinoId, disciplinaId, descricao, opcoes, id);

        public static Result<QuestaoDissertativa> CriarDissertativa(in int nivelEnsinoId, in int disciplinaId,
            in string descricao, int id = 0)
            => QuestaoDissertativa.Criar(nivelEnsinoId, disciplinaId, descricao, id);
    }
}
