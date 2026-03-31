using ControleGastos.API.Models.Enums;

namespace ControleGastos.API.Models
{
    /// <summary>
    /// Representa uma categoria de transação (ex: Alimentação, Salário).
    /// </summary>
    public class Categoria
    {
        /// <summary>
        /// Identificador único gerado automaticamente.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Descrição da categoria. Máximo de 400 caracteres.
        /// </summary>
        public string Descricao { get; set; } = string.Empty;

        /// <summary>
        /// Define se a categoria é para despesa, receita ou ambas.
        /// </summary>
        public FinalidadeCategoria Finalidade { get; set; }

        /// <summary>
        /// Lista de transações associadas a esta categoria.
        /// </summary>
        public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
    }
}