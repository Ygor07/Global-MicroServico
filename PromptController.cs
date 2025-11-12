// Controllers/PromptController.cs
using Microsoft.AspNetCore.Mvc;
using PromptApi.Models;
using PromptApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace PromptApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromptController : ControllerBase
    {
        private readonly IPromptService _promptService;

        public PromptController(IPromptService promptService)
        {
            _promptService = promptService;
        }

        // GET: api/Prompt
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prompt>>> Get()
        {
            var prompts = await _promptService.GetAllPromptsAsync();
            return Ok(prompts);
        }

        // GET api/Prompt/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Prompt>> Get(int id)
        {
            var prompt = await _promptService.GetPromptByIdAsync(id);
            if (prompt == null)
            {
                return NotFound();
            }
            return Ok(prompt);
        }

        // POST api/Prompt
        [HttpPost]
        public async Task<ActionResult<Prompt>> Post([FromBody] Prompt prompt)
        {
            try
            {
                // A validação de ArgumentException (título/conteúdo obrigatório) é feita no Service
                var newId = await _promptService.CreatePromptAsync(prompt);
                prompt.Id = newId;
                return CreatedAtAction(nameof(Get), new { id = newId }, prompt);
            }
            catch (ArgumentException ex)
            {
                // Tratamento de exceção de validação de negócio (Etapa 3)
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                // Tratamento de exceção genérica (erro interno do servidor) (Etapa 3)
                return StatusCode(500, "Ocorreu um erro interno ao processar a requisição.");
            }
        }

        // PUT api/Prompt/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Prompt prompt)
        {
            if (id != prompt.Id)
            {
                return BadRequest("ID do Prompt não corresponde.");
            }

            var updated = await _promptService.UpdatePromptAsync(prompt);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE api/Prompt/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _promptService.DeletePromptAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
