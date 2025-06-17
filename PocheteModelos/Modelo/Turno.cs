using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PocheteModelos.Modelo {
    public class Turno {
        [Key]
        public int Id { get; private set; }

        [Required]
        public string? Nome { get; private set; }

        public virtual ICollection<Sala> Salas { get; private set; } = new List<Sala>();
        public virtual ICollection<Professor> Professores{ get; private set; } = new List<Professor>();
        public virtual ICollection<Turma> Turmas { get; private set; } = new List<Turma>();
        public virtual ICollection<Movimentacao> Movimentacoes { get; private set; } = new List<Movimentacao>();
    }
}