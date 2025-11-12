// Services/PromptRepository.cs
using Dapper;
using PromptApi.Data;
using PromptApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PromptApi.Services
{
    public class PromptRepository : IPromptRepository
    {
        private readonly DataContext _context;

        public PromptRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Prompt>> GetAllAsync()
        {
            var sql = "SELECT Id, Titulo, Conteudo, DataCriacao FROM Prompts";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Prompt>(sql);
        }

        public async Task<Prompt> GetByIdAsync(int id)
        {
            var sql = "SELECT Id, Titulo, Conteudo, DataCriacao FROM Prompts WHERE Id = @Id";
            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Prompt>(sql, new { Id = id });
        }

        public async Task<int> AddAsync(Prompt prompt)
        {
            var sql = "INSERT INTO Prompts (Titulo, Conteudo, DataCriacao) VALUES (@Titulo, @Conteudo, @DataCriacao); SELECT last_insert_rowid();";
            using var connection = _context.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(sql, prompt);
        }

        public async Task<bool> UpdateAsync(Prompt prompt)
        {
            var sql = "UPDATE Prompts SET Titulo = @Titulo, Conteudo = @Conteudo WHERE Id = @Id";
            using var connection = _context.CreateConnection();
            var affectedRows = await connection.ExecuteAsync(sql, prompt);
            return affectedRows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Prompts WHERE Id = @Id";
            using var connection = _context.CreateConnection();
            var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
            return affectedRows > 0;
        }
    }
}
