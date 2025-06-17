using SenaiKeys.Usuarios;

namespace SenaiKeys.Menus {
    public static class MenuRemoverAdm {
        public static void Executar(Usuario usuarioLogado, UsuarioManager manager)
        {
            Console.WriteLine("\n=== Remover Administrador ===");

            // Verifica permissão
            if (!usuarioLogado.PodeRemoverAdm()) {
                Console.WriteLine("Você não tem permissão para remover administrador.");
                return;
            }

            // Confirmação de identidade do ADM atual
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

            // Matrícula do ADM a ser removido
            Console.Write("Matrícula do Adm a remover: ");
            if (!int.TryParse(Console.ReadLine(), out int matriculaRemover)) {
                Console.WriteLine("Matrícula inválida.");
                return;
            }

            var admRemover = manager.BuscarUsuarioPorMatricula(matriculaRemover);
            if (admRemover == null || admRemover.Cargo != "Adm") {
                Console.WriteLine("Administrador não encontrado.");
                return;
            }

            if (admRemover.Matricula == usuarioLogado.Matricula) {
                Console.WriteLine("Você não pode remover a si mesmo.");
                return;
            }

            manager.RemoverUsuario(matriculaRemover);
            Console.WriteLine("Administrador removido com sucesso!");
        }
    }
}

