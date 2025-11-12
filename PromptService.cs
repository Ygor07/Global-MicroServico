// Services/PromptService.cs
using PromptApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace PromptApi.Services
{
    public class PromptService : IPromptService
    {
        private readonly IPromptRepository _repository;

        public PromptService(IPromptRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Prompt>> GetAllPromptsAsync()
        {
            // Lógica de negócio adicional pode ser implementada aqui (ex: filtros, cache)
            return await _repository.GetAllAsync();
        }

        public async Task<Prompt> GetPromptByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<int> CreatePromptAsync(Prompt prompt)
        {
            // Lógica de validação de negócio (Etapa 3: Tratamento de Exceções)
            if (string.IsNullOrWhiteSpace(prompt.Titulo) || string.IsNullOrWhiteSpace(prompt.Conteudo))
            {
                throw new ArgumentException("Título e Conteúdo do Prompt são obrigatórios.");
            }
            return await _repository.AddAsync(prompt);
        }

        public async Task<bool> UpdatePromptAsync(Prompt prompt)
        {
            return await _repository.UpdateAsync(prompt);
        }

        public async Task<bool> DeletePromptAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }

    // Interface para a injeção de dependência do Service
    public interface IPromptService
    {
        Task<IEnumerable<Prompt>> GetAllPromptsAsync();
        Task<Prompt> GetPromptByIdAsync(int id);
        Task<int> CreatePromptAsync(Prompt prompt);
        Task<bool> UpdatePromptAsync(Prompt prompt);
        Task<bool> DeletePromptAsync(int id);
    }
}
