namespace PocheteAPI.DTO
{
    public class TurmasDTO
    {
        public long Id { get; set; }
        public string Identificador { get; set; } = string.Empty;
        public string Turno { get; set; } = string.Empty;
        public long CursoId { get; set; }
    }
}
