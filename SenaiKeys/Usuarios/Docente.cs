using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenaiKeys.Usuarios
{
    public class Docente : Usuario {
        public Docente(string nome, int matricula, string cargo, string senha) : base(nome, matricula, cargo, senha) { }


    }
}
