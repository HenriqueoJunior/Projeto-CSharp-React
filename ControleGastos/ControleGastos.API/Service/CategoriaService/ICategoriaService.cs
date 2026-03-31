using ControleGastos.API.Models;

namespace ControleGastos.API.Services.CategoriaService
{
    /// <summary>
    /// Interface que define as operações disponíveis para gerenciamento de categorias.
    /// </summary>
    public interface ICategoriaService
    {
        Task<IEnumerable<Categoria>> ListarAsync();
        Task<Categoria?> BuscarPorIdAsync(Guid id);
        Task<Categoria> CriarAsync(Categoria categoria);
    }
}