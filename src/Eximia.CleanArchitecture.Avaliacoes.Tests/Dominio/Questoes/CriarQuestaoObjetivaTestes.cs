using System.Collections.Generic;
using Eximia.CleanArchitecture.Avaliacoes.Dominio.Questoes;
using Shouldly;
using Xunit;

namespace Eximia.CleanArchitecture.Avaliacoes.Tests.Dominio.Questoes
{
    public class CriarQuestaoObjetivaTestes
    {
        private int _nivelEnsino = 123;
        private int _disciplina = 11511;
        private string _descricao = "Teste de questão";
        private List<Opcao> _opcoes = new List<Opcao>()
        {
            Opcao.CriarCorreta("Opcao correta", 1).Value,
            Opcao.CriarIncorreta("Opcao incorreta", 2).Value,
        };
            
        [Fact]
        public void DadoNivelDeEnsinoInvalido_QuandoCriarQuestaoObjetiva_DevoReceberFalha()
        {
            var questao = Questao.CriarObjetiva(0, 123, _descricao, _opcoes);

            questao.IsFailure.ShouldBeTrue();
            questao.Error.ShouldBe(QuestoesMotivosErro.NivelEnsinoObrigatorio);
        }

        [Fact]
        public void DadoDisciplinaInvalida_QuandoCriarQuestaoObjetiva_DevoReceberFalha()
        {
            var questao = Questao.CriarObjetiva(_nivelEnsino, 0, _descricao, _opcoes);

            questao.IsFailure.ShouldBeTrue();
            questao.Error.ShouldBe(QuestoesMotivosErro.DisciplinaObrigatoria);
        }

        [Fact]
        public void DadoDesricaoInvalida_QuandoCriarQuestaoObjetiva_DevoReceberFalha()
        {
            var questao = Questao.CriarObjetiva(_nivelEnsino, _disciplina, "", _opcoes);

            questao.IsFailure.ShouldBeTrue();
            questao.Error.ShouldBe(QuestoesMotivosErro.DescricaoObrigatoria);
        }

        [Fact]
        public void DadoOpcoesVazia_QuandoCriarQuestaoObjetiva_DevoReceberFalha()
        {
            var questao = Questao.CriarObjetiva(_nivelEnsino, _disciplina, _descricao, new List<Opcao>());

            questao.IsFailure.ShouldBeTrue();
            questao.Error.ShouldBe(QuestoesMotivosErro.OpcoesObrigatorio + ", " + QuestoesMotivosErro.AoMinimoUmaOpcaoCorreta);
        }

        [Fact]
        public void DadoOpcoesSemCorreta_QuandoCriarQuestaoObjetiva_DevoReceberFalha()
        {
            _opcoes = new List<Opcao>()
            {
                Opcao.CriarIncorreta("Opcao incorreta", 1).Value,
                Opcao.CriarIncorreta("Opcao incorreta", 2).Value,
            };
            var questao = Questao.CriarObjetiva(_nivelEnsino, _disciplina, _descricao, _opcoes);

            questao.IsFailure.ShouldBeTrue();
            questao.Error.ShouldBe(QuestoesMotivosErro.AoMinimoUmaOpcaoCorreta);
        }

        [Fact]
        public void DadoValoresValidosParaQuestao_QuandoCriarQuestaoObjetiva_DevoRecebeObjetoValido()
        {
            var questao = Questao.CriarObjetiva(_nivelEnsino, _disciplina, _descricao, _opcoes);

            questao.IsSuccess.ShouldBeTrue();
            questao.Value.ShouldBeOfType(typeof(QuestaoObjetiva));
            questao.Value.Id.ShouldBe(0);
            questao.Value.NivelEnsinoId.ShouldBe(_nivelEnsino);
            questao.Value.DisciplinaId.ShouldBe(_disciplina);
            questao.Value.Descricao.ShouldBe(_descricao);
            questao.Value.Opcoes.ShouldBe(_opcoes);
        }

        [Fact]
        public void DadoUmValorParaId_QuandoCriarQuestaoObjetiva_DevoRecebeObjetoComIdInformado()
        {
            const int id = 87163;

            var questao = Questao.CriarObjetiva(_nivelEnsino, _disciplina, _descricao, _opcoes, id);

            questao.IsSuccess.ShouldBeTrue();
            questao.Value.Id.ShouldBe(id);
        }
    }
}