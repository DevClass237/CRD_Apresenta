using SenaiKeys.Usuarios;

namespace SenaiKeys.Menus {
    public static class MenuCadastrarAdm {
        public static void Executar(Usuario usuarioLogado, UsuarioManager manager)
        {
            Console.WriteLine("\n=== Cadastro de Administrador ===");

            // Verifica permissão
            if (!usuarioLogado.PodeCadastrarAdm()) {
                Console.WriteLine("Você não tem permissão para cadastrar administrador.");
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

            // Dados do novo ADM
            Console.Write("Nome do novo Adm: ");
            string nome = Console.ReadLine()!;
            Console.Write("Matrícula do novo Adm: ");
            if (!int.TryParse(Console.ReadLine(), out int novaMatricula)) {
                Console.WriteLine("Matrícula inválida.");
                return;
            }
            Console.Write("Senha do novo Adm: ");
            string novaSenha = Console.ReadLine()!;

            // Criação e adição do novo ADM
            var novoAdm = new Adm(nome, novaMatricula, "Adm", novaSenha);
            manager.AdicionarUsuario(novoAdm);
            Console.WriteLine("Administrador cadastrado com sucesso!");
        }
    }
}

