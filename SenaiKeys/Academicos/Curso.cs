using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenaiKeys.Cursos {
    public class Curso {

        public int IdCurso { get; set; }
        public string NomeCurso { get; private set; }
        public int IdTurma { get; private set; }
        public int IdDocente { get; private set; }
        public int IdSala { get; private set; }

        public Curso(int idCurso, string nomeCurso, int idTurma, int idDocente, int idSala)
        {
            IdCurso = idCurso;
            NomeCurso = nomeCurso;
            IdTurma = idTurma;
            IdDocente = idDocente;
            IdSala = idSala;
        }
    }
}