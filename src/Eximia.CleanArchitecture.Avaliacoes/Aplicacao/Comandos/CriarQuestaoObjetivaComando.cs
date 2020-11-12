using CSharpFunctionalExtensions;
using Eximia.CleanArchitecture.SeedWork;

namespace Eximia.CleanArchitecture.Avaliacoes.Aplicacao.Comandos
{
    public sealed class CriarQuestaoObjetivaComando : ICommand<Result<int>>
    {
        public CriarQuestaoObjetivaComando(in int nivelEnsinoId,in int disciplinaId,in string descricao,in Opcao[] opcoes)
        {
            NivelEnsinoId = nivelEnsinoId;
            DisciplinaId = disciplinaId;
            Descricao = descricao;
            Opcoes = opcoes;
        }

        public int NivelEnsinoId { get; private set; }
        public int DisciplinaId { get; private set; }
        public string Descricao { get; private set; }
        public Opcao[] Opcoes { get; private set; }

        public sealed class Opcao
        {
            public Opcao(string texto, bool correta)
            {
                Texto = texto;
                Correta = correta;
            }

            public string Texto { get; private set; }
            public bool Correta { get; private set; }
        }
    }
}