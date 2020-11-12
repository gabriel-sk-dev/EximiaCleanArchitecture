using System.Collections.Generic;
using Eximia.CleanArchitecture.Avaliacoes.Dominio.Questoes;
using Shouldly;
using Xunit;

namespace Eximia.CleanArchitecture.Avaliacoes.Tests.Dominio.Questoes
{
    public class CriarOpcaoTestes
    {
        private string _texto = "Teste de questão";
            
        [Fact]
        public void DadoTextoVazio_QuandoCriarOpcaoCorreta_DevoReceberFalha()
        {
            var opcao = Opcao.CriarCorreta("");

            opcao.IsFailure.ShouldBeTrue();
            opcao.Error.ShouldBe(QuestoesMotivosErro.TextoOpcaoObrigatorio);
        }

        [Fact]
        public void DadoTextoVazio_QuandoCriarOpcaoIncorreta_DevoReceberFalha()
        {
            var opcao = Opcao.CriarIncorreta("");

            opcao.IsFailure.ShouldBeTrue();
            opcao.Error.ShouldBe(QuestoesMotivosErro.TextoOpcaoObrigatorio);
        }

        [Fact]
        public void DadoTextoValido_QuandoCriarOpcaoCorreta_DevoOpcaoCriadaCorretamente()
        {
            var opcao = Opcao.CriarCorreta(_texto);

            opcao.IsSuccess.ShouldBeTrue();
            opcao.Value.Id.ShouldBe(0);
            opcao.Value.Texto.ShouldBe(_texto);
            opcao.Value.Correta.ShouldBeTrue();
        }

        [Fact]
        public void DadoTextoValido_QuandoCriarOpcaoIncorreta_DevoOpcaoCriadaCorretamente()
        {
            var opcao = Opcao.CriarIncorreta(_texto);

            opcao.IsSuccess.ShouldBeTrue();
            opcao.Value.Id.ShouldBe(0);
            opcao.Value.Texto.ShouldBe(_texto);
            opcao.Value.Correta.ShouldBeFalse();
        }

        [Fact]
        public void DadoValorDeId_QuandoCriarOpcaoIncorreta_DevoOpcaoComIdInformado()
        {
            const int id = 1231;

            var opcao = Opcao.CriarIncorreta(_texto, id);

            opcao.IsSuccess.ShouldBeTrue();
            opcao.Value.Id.ShouldBe(id);
        }

        [Fact]
        public void DadoValorDeId_QuandoCriarOpcaoCorreta_DevoOpcaoComIdInformado()
        {
            const int id = 1231;

            var opcao = Opcao.CriarCorreta(_texto, id);

            opcao.IsSuccess.ShouldBeTrue();
            opcao.Value.Id.ShouldBe(id);
        }
    }
}