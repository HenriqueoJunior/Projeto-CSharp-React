using System;
using System.Collections.Generic;

namespace ControleGastos.API.DTOs
{
    public class TotalPorPessoaDTO
    {
        public Guid PessoaId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal TotalReceitas { get; set; }
        public decimal TotalDespesas { get; set; }
        public decimal Saldo => TotalReceitas - TotalDespesas;
    }

    public class RelatorioTotaisPessoasDTO
    {
        public List<TotalPorPessoaDTO> Pessoas { get; set; } = new();
        public decimal TotalGeralReceitas { get; set; }
        public decimal TotalGeralDespesas { get; set; }
        public decimal SaldoGeralLiquido => TotalGeralReceitas - TotalGeralDespesas;
    }

    public class TotalPorCategoriaDTO
    {
        public Guid CategoriaId { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal TotalReceitas { get; set; }
        public decimal TotalDespesas { get; set; }
        public decimal Saldo => TotalReceitas - TotalDespesas;
    }

    public class RelatorioTotaisCategoriasDTO
    {
        public List<TotalPorCategoriaDTO> Categorias { get; set; } = new();
        public decimal TotalGeralReceitas { get; set; }
        public decimal TotalGeralDespesas { get; set; }
        public decimal SaldoGeralLiquido => TotalGeralReceitas - TotalGeralDespesas;
    }
}
