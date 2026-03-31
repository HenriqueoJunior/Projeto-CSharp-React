using ControleGastos.API.Models;

namespace ControleGastos.API.Services.PessoaService
{
    /// <summary>
    /// Interface que define as operações disponíveis para gerenciamento de pessoas.
    /// </summary>
    public interface IPessoaService
    {
        Task<IEnumerable<Pessoa>> ListarAsync();
        Task<Pessoa?> BuscarPorIdAsync(Guid id);
        Task<Pessoa> CriarAsync(Pessoa pessoa);
        Task<Pessoa> AtualizarAsync(Guid id, Pessoa pessoaAtualizada);
        Task DeletarAsync(Guid id);
    }
}