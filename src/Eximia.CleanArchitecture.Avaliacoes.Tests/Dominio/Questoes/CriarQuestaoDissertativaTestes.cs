using System.Collections.Generic;
using Eximia.CleanArchitecture.Avaliacoes.Dominio.Questoes;
using Shouldly;
using Xunit;

namespace Eximia.CleanArchitecture.Avaliacoes.Tests.Dominio.Questoes
{
    public class CriarQuestaoDissertativaTestes
    {
        private int _nivelEnsino = 123;
        private int _disciplina = 11511;
        private string _descricao = "Teste de questão";
            
        [Fact]
        public void DadoNivelDeEnsinoInvalido_QuandoCriarQuestaoDissertativa_DevoReceberFalha()
        {
            var questao = Questao.CriarDissertativa(0, 123, _descricao);

            questao.IsFailure.ShouldBeTrue();
            questao.Error.ShouldBe(QuestoesMotivosErro.NivelEnsinoObrigatorio);
        }

        [Fact]
        public void DadoDisciplinaInvalida_QuandoCriarQuestaoDissertativa_DevoReceberFalha()
        {
            var questao = Questao.CriarDissertativa(_nivelEnsino, 0, _descricao);

            questao.IsFailure.ShouldBeTrue();
            questao.Error.ShouldBe(QuestoesMotivosErro.DisciplinaObrigatoria);
        }

        [Fact]
        public void DadoDesricaoInvalida_QuandoCriarQuestaoDissertativa_DevoReceberFalha()
        {
            var questao = Questao.CriarDissertativa(_nivelEnsino, _disciplina, "");

            questao.IsFailure.ShouldBeTrue();
            questao.Error.ShouldBe(QuestoesMotivosErro.DescricaoObrigatoria);
        }

        [Fact]
        public void DadoValoresValidosParaQuestao_QuandoCriarQuestaoDissertativa_DevoRecebeObjetoValido()
        {
            var questao = Questao.CriarDissertativa(_nivelEnsino, _disciplina, _descricao);

            questao.IsSuccess.ShouldBeTrue();
            questao.Value.ShouldBeOfType(typeof(QuestaoDissertativa));
            questao.Value.Id.ShouldBe(0);
            questao.Value.NivelEnsinoId.ShouldBe(_nivelEnsino);
            questao.Value.DisciplinaId.ShouldBe(_disciplina);
            questao.Value.Descricao.ShouldBe(_descricao);
        }

        [Fact]
        public void DadoUmValorParaId_QuandoCriarQuestaoDissertativa_DevoRecebeObjetoComIdInformado()
        {
            const int id = 87163;

            var questao = Questao.CriarDissertativa(_nivelEnsino, _disciplina, _descricao, id);

            questao.IsSuccess.ShouldBeTrue();
            questao.Value.Id.ShouldBe(id);
        }
    }
}