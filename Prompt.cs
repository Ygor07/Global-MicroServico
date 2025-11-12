// Models/Prompt.cs
using System;

namespace PromptApi.Models
{
    public class Prompt
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }
}
