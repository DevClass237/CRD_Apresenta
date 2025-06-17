using SenaiKeys.Salas;
using SenaiKeys.Usuarios;
using System.ComponentModel.Design;

namespace SenaiKeys.Menus {
    public class Menu {
        private readonly UsuarioManager _manager;
        private readonly SalaManager _salaManager;

        public Menu(UsuarioManager manager, SalaManager salaManager)
        {
            _manager = manager;
            _salaManager = salaManager;
        }

        public void Exibir()
        {
            bool continuar = true;
            while (continuar) {
                var usuarioLogado = LoginMenu.RealizarLogin(_manager);
                if (usuarioLogado == null)
                    continue;

                Console.WriteLine($"\nBem-vindo, {usuarioLogado.Nome} ({usuarioLogado.Cargo})!");
                bool menuUsuario = true;
                while (menuUsuario) {
                    menuUsuario = ExibirMenuUsuario(usuarioLogado, ref continuar);
                }
            }
            Console.WriteLine("Programa finalizado.");
        }

        private bool ExibirMenuUsuario(Usuario usuarioLogado, ref bool continuar)
        {
            int op = 1;
            Console.WriteLine("\nO que deseja fazer?");
            if (usuarioLogado.PodeCadastrarEditor())
                Console.WriteLine($"{op++} - Cadastrar Editor");
            if (usuarioLogado.PodeCadastrarDocente())
                Console.WriteLine($"{op++} - Cadastrar Docente");
            if (usuarioLogado.PodeCadastrarAdm())
                Console.WriteLine($"{op++} - Cadastrar Adm");
            if (usuarioLogado.PodeRemoverDocente())
                Console.WriteLine($"{op++} - Remover Docente");
            if (usuarioLogado.PodeRemoverEditor())
                Console.WriteLine($"{op++} - Remover Editor");
            if (usuarioLogado.PodeRemoverAdm())
                Console.WriteLine($"{op++} - Remover Adm");
            if (usuarioLogado.PodeEditarAssociacao())
                Console.WriteLine($"{op++} - Info/Edição de Salas");
            Console.WriteLine($"{op++} - Listar Usuários");
            Console.WriteLine($"{op++} - Trocar de usuário");
            Console.WriteLine("0 - Sair");
            Console.Write("Opção: ");
            string opcao = Console.ReadLine()!;

            int menuIndex = 1;

            if (usuarioLogado.PodeCadastrarEditor() && opcao == $"{menuIndex++}")
                MenuCadastrarEditor.Executar(usuarioLogado, _manager);
            else if (usuarioLogado.PodeCadastrarDocente() && opcao == $"{menuIndex++}")
                MenuCadastrarDocente.Executar(usuarioLogado, _manager);
            else if (usuarioLogado.PodeCadastrarAdm() && opcao == $"{menuIndex++}")
                MenuCadastrarAdm.Executar(usuarioLogado, _manager);
            else if (usuarioLogado.PodeRemoverDocente() && opcao == $"{menuIndex++}")
                MenuRemoverDocente.Executar(usuarioLogado, _manager);
            else if (usuarioLogado.PodeRemoverEditor() && opcao == $"{menuIndex++}")
                MenuRemoverEditor.Executar(usuarioLogado, _manager);
            else if (usuarioLogado.PodeRemoverAdm() && opcao == $"{menuIndex++}")
                MenuRemoverAdm.Executar(usuarioLogado, _manager);
            else if (usuarioLogado.PodeEditarAssociacao() && opcao == $"{menuIndex++}") {
                var menuEditarAssociacao = new MenuEditarAssociacao();
                menuEditarAssociacao.Executar(usuarioLogado, _salaManager);
            }
            else if (opcao == $"{menuIndex++}")
                MenuListarUsuarios.Executar(_manager);
            else if (opcao == $"{menuIndex++}")
                return false; // Trocar de usuário
            else if (opcao == "0") {
                continuar = false;
                return false;
            }
            else
                Console.WriteLine("Opção inválida!");

            return true;
        }
    }
}