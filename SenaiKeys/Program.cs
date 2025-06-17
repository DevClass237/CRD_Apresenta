using SenaiKeys.Usuarios;
using SenaiKeys.Salas;
using SenaiKeys.Menus;
using System;
using SenaiKeys.Academicos;
using SenaiKeys.Cursos;

namespace SenaiKeys {
    class Program {
        static void Main(string[] args)
        {
            // Instanciando os gerenciadores
            var salaManager = new SalaManager();
            var usuarioManager = new UsuarioManager(null, salaManager);

            // Criando um Editor e adicionando ao sistema
            var editor = new Editor("Maria", 1001, "Editor", "1234");
            usuarioManager.AdicionarUsuario(editor);

            Adm adm = new Adm("dannys", 1234, "Adm", "asdf");
            usuarioManager.AdicionarUsuario(adm);
            Console.Clear();
            // Criando uma sala de exemplo
            // Salas pré-definidas
            salaManager.salas.Add(new Sala(1, true, "Lab 2", 2001, 3001, 1));
            salaManager.salas.Add(new Sala(2, false, "Lab 3", 2002, 3002, 2));
            salaManager.salas.Add(new Sala(3, true, "Lab 4", 2003, 3003, 3));

            // Turmas pré-definidas
            salaManager.turmas.Add(new Turma(1,101, 3001, 2001, 1));
            salaManager.turmas.Add(new Turma(2, 102, 3002, 2002, 2));
            salaManager.turmas.Add(new Turma(3, 103, 3003, 2003, 3));

            // Cursos pré-definidos
            salaManager.cursos.Add(new Curso(3001,"Informática", 4001, 2001, 1));
            salaManager.cursos.Add(new Curso(3002, "Administração", 4002, 2002, 2));
            salaManager.cursos.Add(new Curso(3003, "Eletrotécnica", 4003, 2003, 3));


            // Exemplo: menu de edição de associação
            var menu = new Menu(usuarioManager, salaManager);
            menu.Exibir();

        }
    }
}