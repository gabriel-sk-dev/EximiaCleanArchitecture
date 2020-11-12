using CSharpFunctionalExtensions;
using Eximia.CleanArchitecture.SeedWork;

namespace Eximia.CleanArchitecture.Avaliacoes.Dominio.NiveisEnsino
{
    public sealed class NivelEnsino : Entity<int>, IAggregateRoot
    {
        private NivelEnsino(){}
        public NivelEnsino(in int id, in string nome) : base(id)
        {
            Nome = nome;
        }
        public string Nome { get; }
    }
}
