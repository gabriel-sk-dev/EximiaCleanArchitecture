using System;
using System.Threading;
using System.Threading.Tasks;
using Eximia.CleanArchitecture.Avaliacoes.Dominio.Questoes;
using Eximia.CleanArchitecture.SeedWork;

namespace Eximia.CleanArchitecture.Avaliacoes.Infraestrutura.Repositorios
{
    public sealed class QuestoesRepositorio : IQuestoesRepositorio
    {
        private readonly AvaliacoesDbContext _context;

        public QuestoesRepositorio(AvaliacoesDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task AdicionarAsync(Questao questao, CancellationToken cancellationToken = default)
        {
            await _context.Questoes.AddAsync(questao, cancellationToken);
        }
    }
}