using System.ComponentModel.DataAnnotations.Schema;

namespace PocheteModelos.Modelo {
    public class Sala {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        public string Nome { get; set; } = string.Empty;
    }
}
