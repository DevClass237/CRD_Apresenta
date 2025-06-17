using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenaiKeys.Login {
    class LoginConsole {
        public (int matricula, string senha) SolicitarCredenciais()
        {
                Console.Write("Matrícula: ");
                int matricula = int.Parse(Console.ReadLine()!);
                Console.Write("Senha: ");
                string senha = Console.ReadLine()!;
                return (matricula, senha);
        }
    }
}
