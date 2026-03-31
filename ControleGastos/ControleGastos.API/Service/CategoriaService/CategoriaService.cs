using ControleGastos.API.Data;
using ControleGastos.API.Models;
using ControleGastos.API.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.API.Services.CategoriaService
{
    /// <summary>
    /// Implementação do serviço de gerenciamento de categorias.
    /// Contém as regras de negócio e acesso ao banco de dados.
    /// </summary>
    public class CategoriaService : ICategoriaService
    {
        private readonly AppDbContext _context;

        public CategoriaService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna todas as categorias cadastradas.
        /// </summary>
        public async Task<IEnumerable<Categoria>> ListarAsync()
        {
            return await _context.Categorias.ToListAsync();
        }

        /// <summary>
        /// Busca uma categoria pelo seu identificador único.
        /// Retorna null caso não encontrada.
        /// </summary>
        public async Task<Categoria?> BuscarPorIdAsync(Guid id)
        {
            return await _context.Categorias.FindAsync(id);
        }

        /// <summary>
        /// Cria uma nova categoria após validar as regras de negócio.
        /// </summary>
        public async Task<Categoria> CriarAsync(Categoria categoria)
        {
            // Valida o tamanho da descrição
            if (categoria.Descricao.Length > 400)
                throw new ArgumentException("A descrição deve ter no máximo 400 caracteres.");

            // Valida se a finalidade informada é válida
            if (!Enum.IsDefined(typeof(FinalidadeCategoria), categoria.Finalidade))
                throw new ArgumentException("Finalidade inválida. Use 1 (Despesa), 2 (Receita) ou 3 (Ambas).");

            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return categoria;
        }
    }
}