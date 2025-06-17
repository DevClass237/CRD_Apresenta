using SenaiKeys.Usuarios;

namespace SenaiKeys.Menus {
    public static class MenuRemoverDocente {
        public static void Executar(Usuario usuarioLogado, UsuarioManager manager)
        {
            Console.WriteLine("\n=== Remover Docente ===");

            if (!usuarioLogado.PodeRemoverDocente()) {
                Console.WriteLine("Você não tem permissão para remover docente.");
                return;
            }
            Console.Write("Informe a matrícula do docente a remover: ");
            int matriculaDocenteRemover = int.Parse(Console.ReadLine()!);
            usuarioLogado.RemoverDocente(matriculaDocenteRemover, manager);
            Console.WriteLine("Docente removido com sucesso!");
        }
    }
}
