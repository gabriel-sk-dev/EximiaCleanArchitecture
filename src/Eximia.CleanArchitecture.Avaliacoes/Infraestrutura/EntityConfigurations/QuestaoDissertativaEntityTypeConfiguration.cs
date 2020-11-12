using Eximia.CleanArchitecture.Avaliacoes.Dominio.Questoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eximia.CleanArchitecture.Avaliacoes.Infraestrutura.EntityConfigurations
{
    public class QuestaoDissertativaEntityTypeConfiguration : IEntityTypeConfiguration<QuestaoDissertativa>
    {
        public void Configure(EntityTypeBuilder<QuestaoDissertativa> builder)
        {
            builder.HasBaseType<Questao>();
        }
    }
}