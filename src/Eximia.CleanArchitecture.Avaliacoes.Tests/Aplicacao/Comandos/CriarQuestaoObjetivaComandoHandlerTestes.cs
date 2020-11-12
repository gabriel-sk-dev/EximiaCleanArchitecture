using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Eximia.CleanArchitecture.Avaliacoes.Aplicacao.Comandos;
using Eximia.CleanArchitecture.Avaliacoes.Dominio.Disciplinas;
using Eximia.CleanArchitecture.Avaliacoes.Dominio.NiveisEnsino;
using Eximia.CleanArchitecture.Avaliacoes.Dominio.Questoes;
using Eximia.CleanArchitecture.Avaliacoes.Infraestrutura;
using Eximia.CleanArchitecture.Avaliacoes.Infraestrutura.Repositorios;
using Eximia.CleanArchitecture.Avaliacoes.Tests.Fixture;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace Eximia.CleanArchitecture.Avaliacoes.Tests.Aplicacao.Comandos
{
    [Collection(nameof(AvaliacoesDbContext))]
    public class CriarQuestaoObjetivaComandoHandlerTestes : IAsyncLifetime
    {
        private readonly SqlLiteDbContextFactory _contextFactory;
        private AvaliacoesDbContext _context;
        private IDisciplinasRepositorio _disciplinasRepositorio;
        private INiveisEnsinoRepositorio _nivelEnsinoRepositorio;
        private IQuestoesRepositorio _questoesRepositorio;
        private CriarQuestaoObjetivaComandoHandler _handler;

        private int _nivelEnsinoId = 0;
        private int _disciplinaId = 0;
        private string _descricao = "Teste questão de objetiva";
        private CriarQuestaoObjetivaComando.Opcao _opcaoCorreta = new CriarQuestaoObjetivaComando.Opcao("Opcao Correta", true);
        private CriarQuestaoObjetivaComando.Opcao _opcaoIncorreta = new CriarQuestaoObjetivaComando.Opcao("Opcao Incorreta", false);

        public CriarQuestaoObjetivaComandoHandlerTestes(SqlLiteDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        [Fact]
        public async Task DadoDisciplinaInexistente_QuandoCriarQuestaoObjetiva_DevoReceberErro()
        {
            var comando = new CriarQuestaoObjetivaComando(_nivelEnsinoId, 75546, _descricao, new CriarQuestaoObjetivaComando.Opcao[]
            {
                _opcaoCorreta, _opcaoIncorreta
            });

            var (_, isFailure, _, error) = await _handler.Handle(comando, new CancellationToken());

            isFailure.ShouldBeTrue();
            error.ShouldBe(QuestoesMotivosErro.DisciplinaNaoFoiLocalizada);
        }

        [Fact]
        public async Task DadoNivelEnsinoInexistente_QuandoCriarQuestaoObjetiva_DevoReceberErro()
        {
            var comando = new CriarQuestaoObjetivaComando(75546, _disciplinaId, _descricao, new CriarQuestaoObjetivaComando.Opcao[]
            {
                _opcaoCorreta, _opcaoIncorreta
            });

            var (_, isFailure, _, error) = await _handler.Handle(comando, new CancellationToken());

            isFailure.ShouldBeTrue();
            error.ShouldBe(QuestoesMotivosErro.NivelEnsinoNaoLocalizado);
        }

        [Fact]
        public async Task DadoOpcaoComDescricaoInvalida_QuandoCriarQuestaoObjetiva_DevoReceberErro()
        {
            var comando = new CriarQuestaoObjetivaComando(_nivelEnsinoId, _disciplinaId, _descricao, new CriarQuestaoObjetivaComando.Opcao[]
            {
                new CriarQuestaoObjetivaComando.Opcao("", true)
            });

            var (_, isFailure, _, error) = await _handler.Handle(comando, new CancellationToken());

            isFailure.ShouldBeTrue();
            error.ShouldBe(QuestoesMotivosErro.TextoOpcaoObrigatorio);
        }

        [Fact]
        public async Task DadoDescricaoInvalida_QuandoCriarQuestaoObjetiva_DevoReceberErro()
        {
            var comando = new CriarQuestaoObjetivaComando(_nivelEnsinoId, _disciplinaId, "", new CriarQuestaoObjetivaComando.Opcao[]
            {
                _opcaoCorreta, _opcaoIncorreta
            });

            var (_, isFailure, _, error) = await _handler.Handle(comando, new CancellationToken());

            isFailure.ShouldBeTrue();
            error.ShouldBe(QuestoesMotivosErro.DescricaoObrigatoria);
        }

        [Fact]
        public async Task DadoParametrosValidos_QuandoCriarQuestaoObjetiva_DevoSalvarAlteracoesCorretamente()
        {
            var comando = new CriarQuestaoObjetivaComando(_nivelEnsinoId, _disciplinaId, _descricao, new CriarQuestaoObjetivaComando.Opcao[]
            {
                _opcaoCorreta, _opcaoIncorreta
            });

            var (isSuccess, _, id, _) = await _handler.Handle(comando, new CancellationToken());

            isSuccess.ShouldBeTrue();
            id.ShouldBeGreaterThan(0);
            var questao = await _context.Questoes.FirstOrDefaultAsync(c => c.Id == id);
            questao.ShouldNotBeNull();
            questao.Descricao.ShouldBe(_descricao);
            questao.DisciplinaId.ShouldBe(_disciplinaId);
            questao.NivelEnsinoId.ShouldBe(_nivelEnsinoId);
            questao.ShouldBeOfType(typeof(QuestaoObjetiva));
            var objetiva = questao as QuestaoObjetiva;
            objetiva?.Opcoes.FirstOrDefault(c=> c.Correta)?.Texto.ShouldBe(_opcaoCorreta.Texto);
            objetiva?.Opcoes.FirstOrDefault(c => !c.Correta)?.Texto.ShouldBe(_opcaoIncorreta.Texto);
        }

        public async Task InitializeAsync()
        {
            _context = await _contextFactory.CriarAsync();

            var disciplina = new Disciplina(_disciplinaId, "Português");
            var nivelDeEnsino = new NivelEnsino(_nivelEnsinoId, "Ensino Médio");

            await _context.Disciplinas.AddAsync(disciplina);
            await _context.NiveisEnsino.AddAsync(nivelDeEnsino);
            await _context.SaveChangesAsync();
            _disciplinaId = disciplina.Id;
            _nivelEnsinoId = nivelDeEnsino.Id;

            _disciplinasRepositorio = new DisciplinasRepositorio(_context);
            _nivelEnsinoRepositorio = new NiveisEnsinoRepositorio(_context);
            _questoesRepositorio = new QuestoesRepositorio(_context);
            _handler = new CriarQuestaoObjetivaComandoHandler(_disciplinasRepositorio, _nivelEnsinoRepositorio, _questoesRepositorio);
        }

        public async Task DisposeAsync()
        {
            var disciplinas = await _context.Disciplinas.ToListAsync();
            _context.RemoveRange(disciplinas);
            var niveisEnsino = await _context.NiveisEnsino.ToListAsync();
            _context.RemoveRange(niveisEnsino);
            var questoes = await _context.Questoes.ToListAsync();
            _context.RemoveRange(questoes);
            await _context.SaveChangesAsync();
        }
    }
}