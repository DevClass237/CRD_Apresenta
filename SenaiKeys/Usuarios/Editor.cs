using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenaiKeys.Usuarios {
    public class Editor : Usuario {
        public Editor(string nome, int matricula, string cargo, string senha) : base(nome, matricula, cargo, senha) { }

        public override bool PodeCadastrarDocente() => true;
        public override bool PodeRemoverDocente() => true;
        public override bool PodeEditarAssociacao() => true;

        public override void CadastrarDocente(string nome, int matricula, string senha, UsuarioManager manager)
        {
            var docente = new Docente(nome, matricula, senha, "Docente");
            manager.AdicionarUsuario(docente);
        }
        public override void RemoverDocente(int matricula, UsuarioManager manager)
        {
            var usuario = manager.BuscarUsuarioPorMatricula(matricula);
            if (usuario != null && usuario.Cargo == "Docente") {
                manager.RemoverUsuario(matricula);
            }
            else if (usuario == null) {
                Console.WriteLine("Usuário não encontrado!");
            }
        }

    }
}

