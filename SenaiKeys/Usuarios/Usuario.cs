using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenaiKeys.Usuarios {
    public class Usuario {
        public string Nome { get; private set; }
        public int Matricula { get; private set; }
        public string Cargo { get; private set; }
        public string Senha { get; private set; }

        public Usuario(string nome, int matricula, string cargo, string senha)
        {
            Nome = nome;
            Matricula = matricula;
            Cargo = cargo;
            Senha = senha;
        }
        public virtual bool PodeCadastrarDocente() => false;
        public virtual bool PodeRemoverDocente() => false;
        public virtual bool PodeCadastrarEditor() => false;
        public virtual bool PodeRemoverEditor() => false;
        public virtual bool PodeCadastrarAdm() => false;
        public virtual bool PodeRemoverAdm() => false;
        public virtual bool PodeCadastrarUsuario() => false;
        public virtual bool PodeRemoverUsuario() => false;
        public virtual bool PodeEditarAssociacao() => false;
        public virtual void CadastrarDocente(string nome, int matricula, string senha, UsuarioManager manager)
        {
            Console.WriteLine("Você não tem permissão para cadastrar docente.");
        }

        public virtual void RemoverDocente(int matricula, UsuarioManager manager)
        {
            Console.WriteLine("Você não tem permissão para remover docente.");
        }
    }
}
