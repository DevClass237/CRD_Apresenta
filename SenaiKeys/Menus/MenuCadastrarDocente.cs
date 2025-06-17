using SenaiKeys.Usuarios;
using System.Runtime.ConstrainedExecution;

namespace SenaiKeys.Menus {
    public static class MenuCadastrarDocente {
        public static void Executar(Usuario usuarioLogado, UsuarioManager manager)
        {
            Console.WriteLine("\n=== Cadastro de Docente ===");
            Console.Write("Nome: ");
            string nomeDocente = Console.ReadLine()!;
            Console.Write("Matrícula: ");
            if (!int.TryParse(Console.ReadLine(), out int matriculaDocente)) {
                Console.WriteLine("Matrícula inválida.");
                return;
            }
            Console.Write("Senha: ");
            string senhaDocente = Console.ReadLine()!;

            usuarioLogado.CadastrarDocente(nomeDocente, matriculaDocente, senhaDocente, manager);
        }
    }
}