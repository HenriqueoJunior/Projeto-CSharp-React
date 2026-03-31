using ControleGastos.API.DTOs;
using ControleGastos.API.Models;
using ControleGastos.API.Services.CategoriaService;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.API.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento de categorias.
    /// Delega a lógica de negócio para o CategoriaService.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var categorias = await _categoriaService.ListarAsync();
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(Guid id)
        {
            var categoria = await _categoriaService.BuscarPorIdAsync(id);
            if (categoria is null)
                return NotFound("Categoria não encontrada.");
            return Ok(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarCategoriaDTO dto)
        {
            try
            {
                var categoria = new Categoria { Descricao = dto.Descricao, Finalidade = dto.Finalidade };
                var criada = await _categoriaService.CriarAsync(categoria);
                return CreatedAtAction(nameof(BuscarPorId), new { id = criada.Id }, criada);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}