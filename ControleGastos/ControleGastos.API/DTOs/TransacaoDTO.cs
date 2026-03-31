using ControleGastos.API.Models.Enums;

namespace ControleGastos.API.DTOs
{
    /// <summary>
    /// DTO usado para receber os dados de criação de uma transação.
    /// Evita expor diretamente a entidade do banco na requisição.
    /// </summary>
    public class CriarTransacaoDTO
    {
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public TipoTransacao Tipo { get; set; }
        public Guid CategoriaId { get; set; }
        public Guid PessoaId { get; set; }
    }
}