public class LinhaTabela {
    public string Sala { get; set; }         // Número da sala como string
    public string Laboratorio { get; set; }
    public string Docente { get; set; }
    public string Curso { get; set; }
    public string Turma { get; set; }
    public string Retirada { get; set; }     // Data/hora da retirada (ou texto)

    public LinhaTabela(string sala, string laboratorio, string docente, string curso, string turma, string retirada)
    {
        Sala = sala;
        Laboratorio = laboratorio;
        Docente = docente;
        Curso = curso;
        Turma = turma;
        Retirada = retirada;
    }
}
