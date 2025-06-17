using Microsoft.EntityFrameworkCore;
using PocheteModelos.Modelo;

namespace PocheteDados.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Movimentacao> Movimentacoes { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Pochete> Pochetes { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<DadosTemporarios> DadosTemporarios { get; set; }
        public DbSet<Turma> Turmas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Chave primária de Professor
            modelBuilder.Entity<Professor>()
                .HasKey(p => p.Matricula);

            // Relacionamento Movimentacao -> Professor (1:N)
            modelBuilder.Entity<Movimentacao>()
                .HasOne(m => m.Professor)
                .WithMany()
                .HasForeignKey(m => m.ProfessorMatricula)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Pochete>()
            .HasAlternateKey(p => p.IdToken); // Define IdToken como chave alternativa
            // Relacionamento Movimentacao -> Pochete (1:N) 
            modelBuilder.Entity<Movimentacao>()
                .HasOne(m => m.Pochete)
                .WithMany()
                .HasForeignKey(m => m.PocheteId)      // FK na Movimentacao
                .HasPrincipalKey(p => p.IdToken)      // PK alternativa na Pochete
                .OnDelete(DeleteBehavior.Restrict);


            // Relacionamento Pochete -> Sala (1:N)
            modelBuilder.Entity<Pochete>()
                .HasOne(p => p.Sala)
                .WithMany()
                .HasForeignKey(p => p.SalaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Curso -> Professor (1:1)
            modelBuilder.Entity<Curso>()
              .HasKey(c => c.Id);

            // Curso -> Turma (1:N)
            modelBuilder.Entity<Turma>()
                .HasOne(t => t.Curso)
                .WithMany()  // O curso pode ter várias turmas
                .HasForeignKey(t => t.CursoId); // Definindo a chave estrangeira

        }
    }
}