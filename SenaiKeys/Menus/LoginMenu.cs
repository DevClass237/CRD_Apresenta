using SenaiKeys.Usuarios;

namespace SenaiKeys.Menus {
    public static class LoginMenu {
        public static Usuario? RealizarLogin(UsuarioManager manager)
        {
            Console.WriteLine("\n=== Login ===");
            Console.Write("Matrícula: ");
            if (!int.TryParse(Console.ReadLine(), out int matriculaLogin)) {
                Console.WriteLine("Matrícula inválida.");
                return null;
            }
            Console.Write("Senha: ");
            string senhaLogin = Console.ReadLine()!;

            var usuarioLogado = manager.ListarUsuarios()
                .FirstOrDefault(u => u.Matricula == matriculaLogin && u.Senha == senhaLogin);

            if (usuarioLogado == null) {
                Console.WriteLine("Matrícula ou senha inválidos.");
                return null;
            }
            return usuarioLogado;
        }
    }
}
