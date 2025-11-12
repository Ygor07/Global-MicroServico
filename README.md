# Global-MicroServico

# PromptApi - Sistema de Gestão de Prompts

Este projeto implementa uma API para gestão de prompts, utilizando **ASP.NET Core Web API**, **Dapper** para acesso a dados e **SQLite** para persistência.

## Implementações Realizadas

### 1. Modelagem do Domínio
*   **Classe `Prompt`** modelada em `Models/Prompt.cs`.
*   **Conexão com o Banco de Dados** configurada em `Data/DataContext.cs`, utilizando `Microsoft.Data.Sqlite` e a string de conexão em `appsettings.json`.
*   **Criação da Tabela** `Prompts` garantida na inicialização do `DataContext`.

### 2. Implementação do Core
*   **Camada Repository (`PromptRepository.cs`)** implementada utilizando **Dapper** para todas as operações CRUD (Create, Read, Update, Delete).
*   **Camada Service (`PromptService.cs`)** implementada com a lógica de negócio (validação de campos obrigatórios).
*   **Controller (`PromptController.cs`)** criado com os endpoints HTTP (`GET`, `POST`, `PUT`, `DELETE`).
*   **Injeção de Dependência** configurada em `Program.cs` para `DataContext`, `IPromptRepository` e `IPromptService` (utilizando `AddSingleton` para o Contexto e `AddScoped` para o Repositório/Serviço).

### 3. Validações e Melhorias
*   **Tratamento de Exceções** implementado no `PromptController.cs` para capturar `ArgumentException` (erros de validação de negócio) e retornar `HTTP 400 Bad Request`, além de um tratamento genérico para `HTTP 500 Internal Server Error`.

## Branches de Desenvolvimento

| Etapa | Nome da Branch | Descrição |
| :--- | :--- | :--- |
| **Etapa 1** | `feature/ModelagemDominio` | Contém a modelagem da entidade e a configuração inicial do banco de dados/Dapper. |
| **Etapa 2** | `feature/ImplementacaoCore` | Contém a implementação do Repositório, Service, Controller e Injeção de Dependência. |
| **Etapa 3** | `feature/ValidacaoMelhoria` | Contém o refinamento do tratamento de exceções e a documentação final. |

## Como Executar o Projeto

1.  **Pré-requisitos:** .NET SDK (versão 6.0 ou superior).
2.  **Instalação de Pacotes:**
    ```bash
    dotnet add package Dapper
    dotnet add package Microsoft.Data.Sqlite
    ```
3.  **Execução:**
    ```bash
    dotnet run
    ```
4.  A API estará disponível em `https://localhost:7000` (ou porta similar ). A documentação Swagger estará em `/swagger`.

#Tratamento de exceções

// Controllers/PromptController.cs (Versão final)
using Microsoft.AspNetCore.Mvc;
using PromptApi.Models;
using PromptApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System; // Adicionado para ArgumentException

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

        // ... (Métodos Get, Put, Delete permanecem iguais)

        // POST api/Prompt
        [HttpPost]
        public async Task> Post([FromBody] Prompt prompt)
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
                // Tratamento de exceção de validação de negócio
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                // Tratamento de exceção genérica (erro interno do servidor)
                return StatusCode(500, "Ocorreu um erro interno ao processar a requisição.");
            }
        }


## Exemplos de Requisições

### POST (Criar Prompt)
