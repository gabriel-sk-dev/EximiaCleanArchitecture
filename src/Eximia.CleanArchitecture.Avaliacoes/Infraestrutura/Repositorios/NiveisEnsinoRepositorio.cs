using System;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Eximia.CleanArchitecture.Avaliacoes.Dominio.NiveisEnsino;
using Eximia.CleanArchitecture.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Eximia.CleanArchitecture.Avaliacoes.Infraestrutura.Repositorios
{
    public sealed class NiveisEnsinoRepositorio : INiveisEnsinoRepositorio
    {
        private readonly AvaliacoesDbContext _context;

        public NiveisEnsinoRepositorio(AvaliacoesDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Maybe<NivelEnsino>> RecuperarAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.NiveisEnsino.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }
    }
}