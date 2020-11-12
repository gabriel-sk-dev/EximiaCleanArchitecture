using System;
using System.Threading.Tasks;
using Eximia.CleanArchitecture.Avaliacoes.Infraestrutura;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Eximia.CleanArchitecture.Avaliacoes.Tests.Fixture
{
    [CollectionDefinition(nameof(AvaliacoesDbContext))]
    public class SqlLiteDbContextFactoryCollection : ICollectionFixture<SqlLiteDbContextFactory> { }

    public sealed class SqlLiteDbContextFactory :  IDisposable
    {
        private bool _disposed = false;
        private const string InMemoryConnectionString = "DataSource=:memory:";
        private readonly SqliteConnection _connection;

        public SqlLiteDbContextFactory()
        {
            _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();
        }

        public async Task<AvaliacoesDbContext> CriarAsync()
        {
            var options = new DbContextOptionsBuilder<AvaliacoesDbContext>()
                            //.UseLoggerFactory(GetLoggerFactory())
                            //.EnableSensitiveDataLogging()
                            .UseSqlite(_connection)
                            .Options;
            var contexto = new AvaliacoesDbContext(options);
            await contexto.Database.EnsureCreatedAsync();
            return contexto;
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            if (disposing)
            {
                _connection.Close();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}