namespace PocheteModelos.Modelo
{
    public class Movimentacao
    {
        public long Id { get; set; }
        public int ProfessorMatricula { get; set; }
        public string PocheteId { get; set; }

        // Datas da movimentação
        public DateTime DataRetirada { get; set; }
        public DateTime? DataDevolucao { get; set; }

        // Relacionamentos opcionais
        public Professor? Professor { get; set; }
        public Pochete Pochete { get; set; }
    }
}