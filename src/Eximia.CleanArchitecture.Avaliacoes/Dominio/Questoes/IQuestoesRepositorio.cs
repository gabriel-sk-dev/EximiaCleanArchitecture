using System.Threading;
using System.Threading.Tasks;
using Eximia.CleanArchitecture.SeedWork;

namespace Eximia.CleanArchitecture.Avaliacoes.Dominio.Questoes
{
    public interface IQuestoesRepositorio : IRepository<Questao>
    {
        Task AdicionarAsync(Questao questao, CancellationToken cancellationToken = default);
    }
}