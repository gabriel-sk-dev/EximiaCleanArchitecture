using Eximia.CleanArchitecture.Avaliacoes.Dominio.Questoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eximia.CleanArchitecture.Avaliacoes.Infraestrutura.EntityConfigurations
{
    public class QuestaoObjetivaEntityTypeConfiguration : IEntityTypeConfiguration<QuestaoObjetiva>
    {
        public void Configure(EntityTypeBuilder<QuestaoObjetiva> builder)
        {
            builder.HasBaseType<Questao>();
            
            builder
                .HasMany(c=> c.Opcoes)
                .WithOne()
                .HasForeignKey("QuestaoId")
                .OnDelete(DeleteBehavior.Cascade)
                .Metadata
                .PrincipalToDependent
                .SetField("_opcoes");
        }
    }
}