using ControleGastos.API.Models.Enums;

namespace ControleGastos.API.Models
{
    /// <summary>
    /// Representa uma transação financeira vinculada a uma pessoa e uma categoria.
    /// </summary>
    public class Transacao
    {
        /// <summary>
        /// Identificador único gerado automaticamente.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Descrição da transação. Máximo de 400 caracteres.
        /// </summary>
        public string Descricao { get; set; } = string.Empty;

        /// <summary>
        /// Valor da transação. Deve ser positivo.
        /// </summary>
        public decimal Valor { get; set; }

        /// <summary>
        /// Tipo da transação: despesa ou receita.
        /// </summary>
        public TipoTransacao Tipo { get; set; }

        /// <summary>
        /// Chave estrangeira para a categoria desta transação.
        /// </summary>
        public Guid CategoriaId { get; set; }

        /// <summary>
        /// Categoria associada a esta transação.
        /// </summary>
        public Categoria Categoria { get; set; } = null!;

        /// <summary>
        /// Chave estrangeira para a pessoa dona desta transação.
        /// </summary>
        public Guid PessoaId { get; set; }

        /// <summary>
        /// Pessoa associada a esta transação.
        /// </summary>
        public Pessoa Pessoa { get; set; } = null!;
    }
}