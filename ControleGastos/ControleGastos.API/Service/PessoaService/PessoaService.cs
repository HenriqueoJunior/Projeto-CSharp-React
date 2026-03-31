using ControleGastos.API.Data;
using ControleGastos.API.Models;
using ControleGastos.API.Services.PessoaService;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.API.Services.PessoaService
{
    /// <summary>
    /// Implementação do serviço de gerenciamento de pessoas.
    /// Contém as regras de negócio e acesso ao banco de dados.
    /// </summary>
    public class PessoaService : IPessoaService
    {
        private readonly AppDbContext _context;

        public PessoaService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna todas as pessoas cadastradas.
        /// </summary>
        public async Task<IEnumerable<Pessoa>> ListarAsync()
        {
            return await _context.Pessoas.ToListAsync();
        }

        /// <summary>
        /// Busca uma pessoa pelo seu identificador único.
        /// Retorna null caso não encontrada.
        /// </summary>
        public async Task<Pessoa?> BuscarPorIdAsync(Guid id)
        {
            return await _context.Pessoas.FindAsync(id);
        }

        /// <summary>
        /// Cria uma nova pessoa após validar as regras de negócio.
        /// </summary>
        public async Task<Pessoa> CriarAsync(Pessoa pessoa)
        {
            // Valida o tamanho do nome
            if (pessoa.Nome.Length > 200)
                throw new ArgumentException("O nome deve ter no máximo 200 caracteres.");

            // Valida se a idade é válida
            if (pessoa.Idade < 0)
                throw new ArgumentException("A idade não pode ser negativa.");

            _context.Pessoas.Add(pessoa);
            await _context.SaveChangesAsync();

            return pessoa;
        }

        /// <summary>
        /// Atualiza os dados de uma pessoa existente.
        /// </summary>
        public async Task<Pessoa> AtualizarAsync(Guid id, Pessoa pessoaAtualizada)
        {
            var pessoa = await _context.Pessoas.FindAsync(id)
                ?? throw new KeyNotFoundException("Pessoa não encontrada.");

            if (pessoaAtualizada.Nome.Length > 200)
                throw new ArgumentException("O nome deve ter no máximo 200 caracteres.");

            if (pessoaAtualizada.Idade < 0)
                throw new ArgumentException("A idade não pode ser negativa.");

            // Atualiza apenas os campos editáveis
            pessoa.Nome = pessoaAtualizada.Nome;
            pessoa.Idade = pessoaAtualizada.Idade;

            await _context.SaveChangesAsync();

            return pessoa;
        }

        /// <summary>
        /// Deleta uma pessoa e todas as suas transações via exclusão em cascata.
        /// </summary>
        public async Task DeletarAsync(Guid id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id)
                ?? throw new KeyNotFoundException("Pessoa não encontrada.");

            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();
        }
    }
}