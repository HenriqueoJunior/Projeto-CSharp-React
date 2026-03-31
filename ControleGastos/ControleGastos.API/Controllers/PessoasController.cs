using ControleGastos.API.DTOs;
using ControleGastos.API.Models;
using ControleGastos.API.Services.PessoaService;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.API.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento de pessoas.
    /// Delega a lógica de negócio para o PessoaService.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PessoasController : ControllerBase
    {
        private readonly IPessoaService _pessoaService;

        public PessoasController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var pessoas = await _pessoaService.ListarAsync();
            return Ok(pessoas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(Guid id)
        {
            var pessoa = await _pessoaService.BuscarPorIdAsync(id);
            if (pessoa is null)
                return NotFound("Pessoa não encontrada.");
            return Ok(pessoa);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarPessoaDTO dto)
        {
            try
            {
                var pessoa = new Pessoa { Nome = dto.Nome, Idade = dto.Idade };
                var criada = await _pessoaService.CriarAsync(pessoa);
                return CreatedAtAction(nameof(BuscarPorId), new { id = criada.Id }, criada);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] CriarPessoaDTO dto)
        {
            try
            {
                var pessoa = new Pessoa { Nome = dto.Nome, Idade = dto.Idade };
                var atualizada = await _pessoaService.AtualizarAsync(id, pessoa);
                return Ok(atualizada);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            try
            {
                await _pessoaService.DeletarAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}