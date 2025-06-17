using SenaiKeys.Usuarios;

namespace SenaiKeys.Menus {
    public static class MenuListarUsuarios {
        public static void Executar(UsuarioManager manager)
        {
            Console.WriteLine("\n=== Lista de Usuários ===");
            foreach (var usuario in manager.ListarUsuarios()) {
                Console.WriteLine($"{usuario.Cargo}: {usuario.Nome} (Matrícula: {usuario.Matricula})");
            }
        }
    }
}
