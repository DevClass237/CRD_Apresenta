using PocheteModelos.Modelo;

public class Turma
{
    public long Id { get; set; }
    public string Identificador { get; set; } = string.Empty;
    public string Turno { get; set; } = string.Empty;  // Campo para armazenar o turno da turma
    public long CursoId { get; set; } // Relacionamento com a tabela Cursos
    public Curso Curso { get; set; }  // Propriedade de navegação para o curso
}
