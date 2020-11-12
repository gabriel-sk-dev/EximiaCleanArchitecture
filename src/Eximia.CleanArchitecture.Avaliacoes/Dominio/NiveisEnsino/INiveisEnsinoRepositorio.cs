using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Eximia.CleanArchitecture.SeedWork;

namespace Eximia.CleanArchitecture.Avaliacoes.Dominio.NiveisEnsino
{
    public interface INiveisEnsinoRepositorio : IRepository<NivelEnsino>
    {
        Task<Maybe<NivelEnsino>> RecuperarAsync(int id, CancellationToken cancellationToken = default);
    }
}