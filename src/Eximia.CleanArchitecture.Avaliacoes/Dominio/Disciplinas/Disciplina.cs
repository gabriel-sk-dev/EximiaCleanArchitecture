using CSharpFunctionalExtensions;
using Eximia.CleanArchitecture.SeedWork;

namespace Eximia.CleanArchitecture.Avaliacoes.Dominio.Disciplinas
{
    public sealed class Disciplina : Entity<int>, IAggregateRoot
    {
        private Disciplina(){}
        public Disciplina(in int id, in string nome) : base(id)
        {
            Nome = nome;
        }
        public string Nome { get; }
    }
}
