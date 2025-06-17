using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenaiKeys.Academicos {
    public class Turma {
        public int IdTurma { get; private set; }
        public int NumeroTurma { get; private set; }
        public int IdCurso { get; private set; }
        public int IdDocente { get; private set; }
        public int IdSala { get; private set; }

        public Turma(int idTurma, int numeroTurma, int idCurso, int idDocente, int idSala)
        {
            IdTurma = idTurma;
            NumeroTurma = numeroTurma;
            IdCurso = idCurso;
            IdDocente = idDocente;
            IdSala = idSala;
        }
    }
}
