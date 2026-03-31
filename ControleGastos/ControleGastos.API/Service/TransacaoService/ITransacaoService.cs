using ControleGastos.API.Models;

namespace ControleGastos.API.Services.TransacaoService
{
    /// <summary>
    /// Interface que define as operações disponíveis para gerenciamento de transações.
    /// </summary>
    public interface ITransacaoService
    {
        Task<IEnumerable<Transacao>> ListarAsync();
        Task<Transacao?> BuscarPorIdAsync(Guid id);
        Task<Transacao> CriarAsync(Transacao transacao);
    }
}