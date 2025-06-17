using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SenaiKeys.Usuarios
{
    class Adm : Usuario {
        public Adm(string nome, int matricula, string cargo, string senha) : base(nome, matricula, cargo, senha) { }

    
        public override bool PodeCadastrarDocente() => true;
        public override bool PodeRemoverDocente() => true;
        public override bool PodeCadastrarEditor() => true;
        public override bool PodeRemoverEditor() => true;
        public override bool PodeCadastrarAdm() => true;
        public override bool PodeRemoverAdm() => true;
        public override bool PodeEditarAssociacao() => true;


        public override void CadastrarDocente(string nome, int matricula, string senha, UsuarioManager manager)
        {
            var docente = new Docente(nome, matricula, "Docente", senha);
            manager.AdicionarUsuario(docente);           
        }
        public void CadastrarEditor(string nome, int matricula, string senha, UsuarioManager manager )
        {
            var editor = new Editor(nome, matricula, "Editor", senha);
            manager.AdicionarUsuario(editor);
        }
        public void CadastrarAdm(string nome, int matricula, UsuarioManager manager, string senha)
        {
            var adm = new Adm(nome, matricula, "Adm", senha);
            manager.AdicionarUsuario(adm);
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
        public void RemoverEditor(int matricula, UsuarioManager manager)
        {
            var usuario = manager.BuscarUsuarioPorMatricula(matricula);
            if (usuario != null && usuario.Cargo == "Editor") {
                manager.RemoverUsuario(matricula);
                Console.WriteLine("Editor removido com sucesso!");  
            }
            else if (usuario == null) {
                Console.WriteLine("Usuário não encontrado!");
            }
        }
        public void RemoverAdm(int matricula, UsuarioManager manager)
        {
            var usuario = manager.BuscarUsuarioPorMatricula(matricula);
            if (usuario != null && usuario.Cargo == "Adm") {
                manager.RemoverUsuario(matricula);
            }
            else if (usuario == null) {
                Console.WriteLine("Usuário não encontrado!");
            }
        }

    }
}
