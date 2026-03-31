namespace ControleGastos.API.DTOs
{
    /// <summary>
    /// DTO usado para receber os dados de criação e atualização de uma pessoa.
    /// </summary>
    public class CriarPessoaDTO
    {
        public string Nome { get; set; } = string.Empty;
        public int Idade { get; set; }
    }
}