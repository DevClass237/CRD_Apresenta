using SenaiKeys.Cursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenaiKeys.Academicos {
    public class Sala {


        public int Chave { get; private set; }
        public bool Status { get; private set; }
        public string Laboratorio { get; private set; }
        public int IdDocente { get; private set; }
        public int IdCurso { get; private set; }
        public int IdTurma { get; private set; }

        public Sala(int chave, bool status, string laboratorio, int idDocente, int idCurso, int idTurma)
        {
            Chave = chave;
            Status = status;
            Laboratorio = laboratorio;
            IdDocente = idDocente;
            IdCurso = idCurso;
            IdTurma = idTurma;
        }


        public void AlterarDocente(int novoIdDocente)
        {
            IdDocente = novoIdDocente;
        }
        public void AlterarCurso(int novoIdCurso)
        {
            IdCurso = novoIdCurso;
        }
        public void AlterarLaboratorio(string novoLaboratorio)
        {
            Laboratorio = novoLaboratorio;
        }
        public void AlterarTurma(int novoIdTurma)
        {
            IdTurma = novoIdTurma;
        }
    }
}