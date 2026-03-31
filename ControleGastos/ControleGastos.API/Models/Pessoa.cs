namespace ControleGastos.API.Models
{
    /// <summary>
    /// Representa uma pessoa cadastrada no sistema.
    /// </summary>
    public class Pessoa
    {
        /// <summary>
        /// Identificador único gerado automaticamente.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Nome da pessoa. Máximo de 200 caracteres.
        /// </summary>
        public string Nome { get; set; } = string.Empty;

        /// <summary>
        /// Idade da pessoa.
        /// </summary>
        public int Idade { get; set; }

        /// <summary>
        /// Lista de transações associadas a esta pessoa.
        /// Usada para garantir a exclusão em cascata.
        /// </summary>
        public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
    }
}