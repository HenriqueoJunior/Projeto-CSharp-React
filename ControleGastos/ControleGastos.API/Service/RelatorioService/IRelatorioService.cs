using System.Threading.Tasks;
using ControleGastos.API.DTOs;

namespace ControleGastos.API.Services.RelatorioService
{
    public interface IRelatorioService
    {
        Task<RelatorioTotaisPessoasDTO> ObterTotaisPorPessoaAsync();
        Task<RelatorioTotaisCategoriasDTO> ObterTotaisPorCategoriaAsync();
    }
}
