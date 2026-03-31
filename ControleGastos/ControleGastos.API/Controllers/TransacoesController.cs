using ControleGastos.API.DTOs;
using ControleGastos.API.Models;
using ControleGastos.API.Services.TransacaoService;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.API.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento de transações.
    /// Delega a lógica de negócio para o TransacaoService.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TransacoesController : ControllerBase
    {
        private readonly ITransacaoService _transacaoService;

        public TransacoesController(ITransacaoService transacaoService)
        {
            _transacaoService = transacaoService;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var transacoes = await _transacaoService.ListarAsync();
            return Ok(transacoes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(Guid id)
        {
            var transacao = await _transacaoService.BuscarPorIdAsync(id);
            if (transacao is null)
                return NotFound("Transação não encontrada.");
            return Ok(transacao);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarTransacaoDTO dto)
        {
            try
            {
                var transacao = new Transacao
                {
                    Descricao = dto.Descricao,
                    Valor = dto.Valor,
                    Tipo = dto.Tipo,
                    CategoriaId = dto.CategoriaId,
                    PessoaId = dto.PessoaId
                };
                var criada = await _transacaoService.CriarAsync(transacao);
                return CreatedAtAction(nameof(BuscarPorId), new { id = criada.Id }, criada);
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
    }
}