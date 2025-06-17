using SenaiKeys.Salas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenaiKeys.Usuarios {

    public class UsuarioManager(UsuarioManager manager, SalaManager salaManager) {
        private readonly UsuarioManager _manager = manager;
        private readonly SalaManager _salaManager = salaManager;

        private List<Usuario> usuarios = new List<Usuario>();


        public void AdicionarUsuario(Usuario usuario)
        {
            if (usuario == null) {
                Console.WriteLine("Usuário inválido.");
                return;
            }
            if (string.IsNullOrEmpty(usuario.Nome) || string.IsNullOrEmpty(usuario.Senha)) {
                Console.WriteLine("Nome e senha não podem ser nulos ou vazios!");
                return;
            }
            if (usuario.Matricula <= 0) {
                Console.WriteLine("Matrícula deve ser maior que zero!");
                return;
            }
            if (usuarios.Any(u => u.Matricula == usuario.Matricula)) {
                Console.WriteLine("Usuário já cadastrado com essa matrícula.");
                return;
            }

            usuarios.Add(usuario);
            Console.WriteLine("Usuário cadastrado com sucesso!");
        }


        public IEnumerable<Usuario> ListarUsuarios()
        {
            return usuarios;
        }
        public Usuario BuscarUsuarioPorMatricula(int matricula)
        {
            return usuarios.FirstOrDefault(u => u.Matricula == matricula)!;
        }
        public void RemoverUsuario(int matricula)
        {
            var usuario = BuscarUsuarioPorMatricula(matricula);
            if (usuario != null) {
                usuarios.Remove(usuario);
            }
        }


    }

}