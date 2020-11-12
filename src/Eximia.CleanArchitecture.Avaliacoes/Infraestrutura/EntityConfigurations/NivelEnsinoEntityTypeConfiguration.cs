using Eximia.CleanArchitecture.Avaliacoes.Dominio.NiveisEnsino;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eximia.CleanArchitecture.Avaliacoes.Infraestrutura.EntityConfigurations
{
    public class NivelEnsinoEntityTypeConfiguration : IEntityTypeConfiguration<NivelEnsino>
    {
        public void Configure(EntityTypeBuilder<NivelEnsino> builder)
        {
            builder.ToTable("NiveisEnsino", Ambiente.AppName);
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Nome).HasColumnType("varchar(100)");
        }
    }
}