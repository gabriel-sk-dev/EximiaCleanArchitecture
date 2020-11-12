using System.Threading;
using System.Threading.Tasks;
using Eximia.CleanArchitecture.Avaliacoes.Dominio.Disciplinas;
using Eximia.CleanArchitecture.Avaliacoes.Dominio.NiveisEnsino;
using Eximia.CleanArchitecture.Avaliacoes.Dominio.Questoes;
using Eximia.CleanArchitecture.Avaliacoes.Infraestrutura.EntityConfigurations;
using Eximia.CleanArchitecture.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Eximia.CleanArchitecture.Avaliacoes.Infraestrutura
{
    public class AvaliacoesDbContext : DbContext, IUnitOfWork
    {
        public AvaliacoesDbContext(DbContextOptions<AvaliacoesDbContext> options) : base(options) { }

        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<NivelEnsino> NiveisEnsino { get; set; }
        public DbSet<Questao> Questoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DisciplinaEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new NivelEnsinoEntityTypeConfiguration());

            modelBuilder.ApplyConfiguration(new QuestaoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new QuestaoObjetivaEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new QuestaoDissertativaEntityTypeConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            //await _mediator.DispatchDomainEventsAsync(this);

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            await base.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}