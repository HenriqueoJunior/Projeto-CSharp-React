using System.Linq;
using System.Threading.Tasks;
using ControleGastos.API.Data;
using ControleGastos.API.DTOs;
using ControleGastos.API.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.API.Services.RelatorioService
{
    public class RelatorioService : IRelatorioService
    {
        private readonly AppDbContext _context;

        public RelatorioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RelatorioTotaisPessoasDTO> ObterTotaisPorPessoaAsync()
        {
            var pessoas = await _context.Pessoas
                .Include(p => p.Transacoes)
                .ToListAsync();

            var relatorio = new RelatorioTotaisPessoasDTO();

            foreach (var pessoa in pessoas)
            {
                var totalReceitas = pessoa.Transacoes
                    .Where(t => t.Tipo == TipoTransacao.Receita)
                    .Sum(t => t.Valor);

                var totalDespesas = pessoa.Transacoes
                    .Where(t => t.Tipo == TipoTransacao.Despesa)
                    .Sum(t => t.Valor);

                relatorio.Pessoas.Add(new TotalPorPessoaDTO
                {
                    PessoaId = pessoa.Id,
                    Nome = pessoa.Nome,
                    TotalReceitas = totalReceitas,
                    TotalDespesas = totalDespesas
                });

                relatorio.TotalGeralReceitas += totalReceitas;
                relatorio.TotalGeralDespesas += totalDespesas;
            }

            return relatorio;
        }

        public async Task<RelatorioTotaisCategoriasDTO> ObterTotaisPorCategoriaAsync()
        {
            var categorias = await _context.Categorias
                .Include(c => c.Transacoes)
                .ToListAsync();

            var relatorio = new RelatorioTotaisCategoriasDTO();

            foreach (var categoria in categorias)
            {
                var totalReceitas = categoria.Transacoes
                    .Where(t => t.Tipo == TipoTransacao.Receita)
                    .Sum(t => t.Valor);

                var totalDespesas = categoria.Transacoes
                    .Where(t => t.Tipo == TipoTransacao.Despesa)
                    .Sum(t => t.Valor);

                relatorio.Categorias.Add(new TotalPorCategoriaDTO
                {
                    CategoriaId = categoria.Id,
                    Descricao = categoria.Descricao,
                    TotalReceitas = totalReceitas,
                    TotalDespesas = totalDespesas
                });

                relatorio.TotalGeralReceitas += totalReceitas;
                relatorio.TotalGeralDespesas += totalDespesas;
            }

            return relatorio;
        }
    }
}
