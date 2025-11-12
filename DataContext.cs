// Data/DataContext.cs
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace PromptApi.Data
{
    public class DataContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
            
            // Garante que o banco de dados e a tabela são criados na inicialização
            InitializeDatabase();
        }

        public IDbConnection CreateConnection() => new SqliteConnection(_connectionString);

        private void InitializeDatabase()
        {
            using var connection = CreateConnection();
            connection.Open();

            // Cria a tabela Prompt se ela não existir
            var sql = @"
                CREATE TABLE IF NOT EXISTS Prompts (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Titulo TEXT NOT NULL,
                    Conteudo TEXT NOT NULL,
                    DataCriacao TEXT NOT NULL
                );";
            
            using var command = new SqliteCommand(sql, (SqliteConnection)connection);
            command.ExecuteNonQuery();
        }
    }
}
