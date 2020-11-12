using Eximia.CleanArchitecture.Avaliacoes.Dominio.Disciplinas;
using Eximia.CleanArchitecture.Avaliacoes.Dominio.NiveisEnsino;
using Eximia.CleanArchitecture.Avaliacoes.Dominio.Questoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eximia.CleanArchitecture.Avaliacoes.Infraestrutura.EntityConfigurations
{
    public class QuestaoEntityTypeConfiguration : IEntityTypeConfiguration<Questao>
    {
        public void Configure(EntityTypeBuilder<Questao> builder)
        {
            builder.ToTable("Questoes", Ambiente.AppName);
            builder.HasKey(c => c.Id);

            builder
                .HasOne<Disciplina>()
                .WithMany()
                .HasForeignKey(c => c.DisciplinaId);

            builder
                .HasOne<NivelEnsino>()
                .WithMany()
                .HasForeignKey(c => c.NivelEnsinoId);

            builder.HasDiscriminator<string>("Tipo")
                .HasValue<QuestaoObjetiva>(nameof(QuestaoObjetiva))
                .HasValue<QuestaoDissertativa>(nameof(QuestaoDissertativa));

            builder.Property(c => c.Descricao).HasColumnType("varchar(100)");
        }
    }
}