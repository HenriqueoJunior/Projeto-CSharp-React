using ControleGastos.API.Data;
using ControleGastos.API.Models;
using ControleGastos.API.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.API.Services.TransacaoService
{
    /// <summary>
    /// Implementação do serviço de gerenciamento de transações.
    /// Contém as regras de negócio e acesso ao banco de dados.
    /// </summary>
    public class TransacaoService : ITransacaoService
    {
        private readonly AppDbContext _context;

        public TransacaoService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna todas as transações cadastradas,
        /// incluindo os dados da pessoa e categoria relacionadas.
        /// </summary>
        public async Task<IEnumerable<Transacao>> ListarAsync()
        {
            return await _context.Transacoes
                .Include(t => t.Pessoa)
                .Include(t => t.Categoria)
                .ToListAsync();
        }

        /// <summary>
        /// Busca uma transação pelo seu identificador único.
        /// Retorna null caso não encontrada.
        /// </summary>
        public async Task<Transacao?> BuscarPorIdAsync(Guid id)
        {
            return await _context.Transacoes
                .Include(t => t.Pessoa)
                .Include(t => t.Categoria)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        /// <summary>
        /// Cria uma nova transação aplicando todas as regras de negócio:
        /// - Descrição deve ter no máximo 400 caracteres
        /// - Valor deve ser positivo
        /// - Pessoa deve existir
        /// - Menor de idade só pode registrar despesas
        /// - Categoria deve existir
        /// - Categoria deve ser compatível com o tipo da transação
        /// </summary>
        public async Task<Transacao> CriarAsync(Transacao transacao)
        {
            // Valida o tamanho da descrição
            if (transacao.Descricao.Length > 400)
                throw new ArgumentException("A descrição deve ter no máximo 400 caracteres.");

            // Valida se o valor é positivo
            if (transacao.Valor <= 0)
                throw new ArgumentException("O valor da transação deve ser positivo.");

            // Valida se o tipo é válido
            if (!Enum.IsDefined(typeof(TipoTransacao), transacao.Tipo))
                throw new ArgumentException("Tipo inválido. Use 1 (Despesa) ou 2 (Receita).");

            // Busca a pessoa no banco
            var pessoa = await _context.Pessoas.FindAsync(transacao.PessoaId)
                ?? throw new KeyNotFoundException("Pessoa não encontrada.");

            // Regra: menor de idade só pode registrar despesas
            if (pessoa.Idade < 18 && transacao.Tipo == TipoTransacao.Receita)
                throw new ArgumentException("Pessoas menores de idade só podem registrar despesas.");

            // Busca a categoria no banco
            var categoria = await _context.Categorias.FindAsync(transacao.CategoriaId)
                ?? throw new KeyNotFoundException("Categoria não encontrada.");

            // Verifica se a categoria aceita o tipo da transação
            var categoriaAceitaDespesa = categoria.Finalidade == FinalidadeCategoria.Despesa
                                      || categoria.Finalidade == FinalidadeCategoria.Ambas;

            var categoriaAceitaReceita = categoria.Finalidade == FinalidadeCategoria.Receita
                                      || categoria.Finalidade == FinalidadeCategoria.Ambas;

            // Regra: categoria deve ser compatível com o tipo da transação
            if (transacao.Tipo == TipoTransacao.Despesa && !categoriaAceitaDespesa)
                throw new ArgumentException("Esta categoria não pode ser usada para despesas.");

            if (transacao.Tipo == TipoTransacao.Receita && !categoriaAceitaReceita)
                throw new ArgumentException("Esta categoria não pode ser usada para receitas.");

            _context.Transacoes.Add(transacao);
            await _context.SaveChangesAsync();

            return transacao;
        }
    }
}