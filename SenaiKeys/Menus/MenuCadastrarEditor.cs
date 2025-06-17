using SenaiKeys.Usuarios;

namespace SenaiKeys.Menus {
    public static class MenuCadastrarEditor {
        public static void Executar(Usuario usuarioLogado, UsuarioManager manager)
        {
            Console.WriteLine("\n=== Cadastro de Editor ===");
            Console.Write("Nome: ");
            string nomeEditor = Console.ReadLine()!;
            Console.Write("Matrícula: ");
            int matriculaEditor = int.Parse(Console.ReadLine()!);
            Console.Write("Senha: ");
            string senhaEditor = Console.ReadLine()!;
            if (usuarioLogado is Adm admLogado)
                admLogado.CadastrarEditor(nomeEditor, matriculaEditor, senhaEditor, manager);
            else
                Console.WriteLine("Você não tem permissão para cadastrar editor.");
        }
    }
}

