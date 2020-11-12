using Eximia.CleanArchitecture.Avaliacoes.Dominio.Disciplinas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eximia.CleanArchitecture.Avaliacoes.Infraestrutura.EntityConfigurations
{
    public class DisciplinaEntityTypeConfiguration : IEntityTypeConfiguration<Disciplina>
    {
        public void Configure(EntityTypeBuilder<Disciplina> builder)
        {
            builder.ToTable("Disciplinas", Ambiente.AppName);
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Nome).HasColumnType("varchar(100)");
        }
    }
}