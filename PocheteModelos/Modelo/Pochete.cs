namespace PocheteModelos.Modelo {
    public class Pochete {
        public long Id { get; set; }
        public long SalaId { get; set; }
        public string IdToken { get; set; }

        //Relações

        public Sala? Sala { get; set; }
    }
}
