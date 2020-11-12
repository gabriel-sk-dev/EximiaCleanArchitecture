using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Eximia.CleanArchitecture.SeedWork;

namespace Eximia.CleanArchitecture.Avaliacoes.Dominio.Disciplinas
{
    public interface IDisciplinasRepositorio : IRepository<Disciplina>
    {
        Task<Maybe<Disciplina>> RecuperarAsync(int id, CancellationToken cancellationToken = default);
    }
}