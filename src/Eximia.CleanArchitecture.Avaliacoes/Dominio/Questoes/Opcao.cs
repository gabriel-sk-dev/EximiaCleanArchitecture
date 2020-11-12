using CSharpFunctionalExtensions;
using Eximia.CleanArchitecture.SeedWork;

namespace Eximia.CleanArchitecture.Avaliacoes.Dominio.Questoes
{
    public sealed class Opcao : Entity<int>
    {
        private Opcao(){}
        private Opcao(in int id, in string texto, in bool correta) : base(id)
        {
            Texto = texto;
            Correta = correta;
        }

        public string Texto { get;  }
        public bool Correta { get; }

        public static Result<Opcao> CriarCorreta(in string texto, in int id = 0)
        {
            var (_, isFailure, error) = Result.Combine(
                texto.MustNotBeNullOrEmpty(QuestoesMotivosErro.TextoOpcaoObrigatorio));
            return isFailure
                ? Result.Failure<Opcao>(error)
                : Result.Success(new Opcao(id, texto, true));
        }

        public static Result<Opcao> CriarIncorreta(in string texto, in int id = 0)
        {
            var (_, isFailure, error) = Result.Combine(
                texto.MustNotBeNullOrEmpty(QuestoesMotivosErro.TextoOpcaoObrigatorio));
            return isFailure
                ? Result.Failure<Opcao>(error)
                : Result.Success(new Opcao(id, texto, false));
        }
    }
}