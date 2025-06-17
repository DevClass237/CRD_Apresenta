using System.ComponentModel.DataAnnotations;

namespace PocheteAPI.DTO
{

    public class MovimentacaoDTO
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "A matrícula do professor é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "Informe uma matrícula válida.")]
        public int ProfessorMatricula { get; set; }

        [Required(ErrorMessage = "O ID da pochete é obrigatório.")]
        public string PocheteId { get; set; }
        public DateTime DataRetirada { get; set; }
        public DateTime? DataDevolucao { get; set; }
    }
}