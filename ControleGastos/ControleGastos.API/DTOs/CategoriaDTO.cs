using ControleGastos.API.Models.Enums;

namespace ControleGastos.API.DTOs
{
    /// <summary>
    /// DTO usado para receber os dados de criação de uma categoria.
    /// </summary>
    public class CriarCategoriaDTO
    {
        public string Descricao { get; set; } = string.Empty;
        public FinalidadeCategoria Finalidade { get; set; }
    }
}