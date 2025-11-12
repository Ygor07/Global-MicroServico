// Services/IPromptRepository.cs
using PromptApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PromptApi.Services
{
    public interface IPromptRepository
    {
        Task<IEnumerable<Prompt>> GetAllAsync();
        Task<Prompt> GetByIdAsync(int id);
        Task<int> AddAsync(Prompt prompt);
        Task<bool> UpdateAsync(Prompt prompt);
        Task<bool> DeleteAsync(int id);
    }
}
