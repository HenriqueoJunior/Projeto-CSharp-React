using ControleGastos.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.API.Data
{
    /// <summary>
    /// Contexto principal do banco de dados da aplicação.
    /// Responsável por mapear as entidades para as tabelas do SQLite.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>Tabela de pessoas.</summary>
        public DbSet<Pessoa> Pessoas { get; set; }

        /// <summary>Tabela de categorias.</summary>
        public DbSet<Categoria> Categorias { get; set; }

        /// <summary>Tabela de transações.</summary>
        public DbSet<Transacao> Transacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração da entidade Pessoa
            modelBuilder.Entity<Pessoa>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Nome)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(p => p.Idade)
                    .IsRequired();

                // Ao deletar uma pessoa, todas as transações dela são deletadas em cascata
                entity.HasMany(p => p.Transacoes)
                    .WithOne(t => t.Pessoa)
                    .HasForeignKey(t => t.PessoaId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuração da entidade Categoria
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Descricao)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(c => c.Finalidade)
                    .IsRequired();
            });

            // Configuração da entidade Transacao
            modelBuilder.Entity<Transacao>(entity =>
            {
                entity.HasKey(t => t.Id);

                entity.Property(t => t.Descricao)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(t => t.Valor)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.Property(t => t.Tipo)
                    .IsRequired();

                // Relacionamento com Categoria (sem cascata, categoria não é deletada com a transação)
                entity.HasOne(t => t.Categoria)
                    .WithMany(c => c.Transacoes)
                    .HasForeignKey(t => t.CategoriaId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}