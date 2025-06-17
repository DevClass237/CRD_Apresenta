using SenaiKeys.Usuarios;

namespace SenaiKeys.Menus {
    public static class MenuRemoverEditor {
        public static void Executar(Usuario usuarioLogado, UsuarioManager manager)
        {
            Console.WriteLine("\n=== Remover Editor ===");

            // Verifica permissão
            if (!usuarioLogado.PodeRemoverEditor()) {
                Console.WriteLine("Você não tem permissão para remover editor.");
                return;
            }

            // Confirmação de identidade do usuário atual
            Console.Write("Confirme sua matrícula: ");
            if (!int.TryParse(Console.ReadLine(), out int matriculaConfirmacao) || matriculaConfirmacao != usuarioLogado.Matricula) {
                Console.WriteLine("Matrícula incorreta.");
                return;
            }
            Console.Write("Confirme sua senha: ");
            string senhaConfirmacao = Console.ReadLine()!;
            if (senhaConfirmacao != usuarioLogado.Senha) {
                Console.WriteLine("Senha incorreta.");
                return;
            }

            // Matrícula do editor a ser removido
            Console.Write("Matrícula do Editor a remover: ");
            if (!int.TryParse(Console.ReadLine(), out int matriculaRemover)) {
                Console.WriteLine("Matrícula inválida.");
                return;
            }

            var editorRemover = manager.BuscarUsuarioPorMatricula(matriculaRemover);
            if (editorRemover == null || editorRemover.Cargo != "Editor") {
                Console.WriteLine("Editor não encontrado.");
                return;
            }

            manager.RemoverUsuario(matriculaRemover);
            Console.WriteLine("Editor removido com sucesso!");
        }
    }
}
