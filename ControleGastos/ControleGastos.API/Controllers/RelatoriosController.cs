using System.Threading.Tasks;
using ControleGastos.API.Services.RelatorioService;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelatoriosController : ControllerBase
    {
        private readonly IRelatorioService _relatorioService;

        public RelatoriosController(IRelatorioService relatorioService)
        {
            _relatorioService = relatorioService;
        }

        [HttpGet("pessoas")]
        public async Task<IActionResult> ObterTotaisPorPessoa()
        {
            var relatorio = await _relatorioService.ObterTotaisPorPessoaAsync();
            return Ok(relatorio);
        }

        [HttpGet("categorias")]
        public async Task<IActionResult> ObterTotaisPorCategoria()
        {
            var relatorio = await _relatorioService.ObterTotaisPorCategoriaAsync();
            return Ok(relatorio);
        }
    }
}
