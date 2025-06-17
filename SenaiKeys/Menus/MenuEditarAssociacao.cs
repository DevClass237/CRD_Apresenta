using SenaiKeys.Salas;
using SenaiKeys.Usuarios;
using System;

namespace SenaiKeys.Menus {
    class MenuEditarAssociacao {
        public void Executar(Usuario usuario, SalaManager salaManager)
        {
            if (!usuario.PodeEditarAssociacao()) {
                Console.WriteLine("Você não tem permissão para editar associações.");
                return;
            }

            Console.WriteLine("1 - Listar Salas");
            Console.WriteLine("2 - Buscar Sala por Chave");
            Console.WriteLine("3 - Editar Associação de Sala");
            Console.WriteLine("4 - Listar Salas Detalhadas");
            Console.WriteLine("0 - Sair");
            string opcao = Console.ReadLine()!;

            switch (opcao) {
                case "1":
                    ListarSalas(salaManager);
                    break;
                case "2":
                    BuscarSala(salaManager);
                    break;
                case "3":
                    EditarAssociacao(salaManager);
                    break;
                case "4":
                    UsuarioManager usuarioManager = new UsuarioManager(null, salaManager);
                    salaManager.MostrarSalasDetalhadas();                  
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
        //private void MostrarSalasDetalhadas(SalaManager salaManager)
        //{
        //    Console.WriteLine("\n--- Salas Detalhadas ---");
        //    UsuarioManager usuarioManager = new UsuarioManager(null, salaManager);
        //    foreach (var sala in salaManager.salas) {
        //        var docente = usuarioManager.BuscarUsuarioPorMatricula(sala.IdDocente);
        //        var curso = salaManager.cursos.FirstOrDefault(c => c.Id == sala.IdCurso);
        //        var turma = salaManager.turmas.FirstOrDefault(t => t.NumeroTurma == sala.IdTurma);
        //        Console.WriteLine($"Chave: {sala.Chave}, Laboratório: {sala.Laboratorio}, Curso: {curso?.NomeCurso ?? "N/A"}, " +
        //                          $"Docente: {docente?.Nome ?? "N/A"}, Turma: {turma?.NumeroTurma ?? 0}");
        //    }
        //}

        private void ListarSalas(SalaManager salaManager)
        {
            Console.WriteLine("\n--- Salas ---");
            foreach (var sala in salaManager.salas)
                Console.WriteLine($"Chave: {sala.Chave}, Lab: {sala.Laboratorio}, Curso: {sala.IdCurso}, Docente: {sala.IdDocente}");
        }

        private void BuscarSala(SalaManager salaManager)
        {
            Console.Write("Informe a chave da sala: ");
            if (int.TryParse(Console.ReadLine(), out int chave)) {
                var sala = salaManager.BuscarSalaPorChave(chave);
                if (sala != null)
                    Console.WriteLine($"Chave: {sala.Chave}, Lab: {sala.Laboratorio}, Curso: {sala.IdCurso}, Docente: {sala.IdDocente}");
                else
                    Console.WriteLine("Sala não encontrada.");
            }
            else {
                Console.WriteLine("Chave inválida.");
            }
        }

        private void EditarAssociacao(SalaManager salaManager)
        {
            Console.Write("Chave da sala: ");
            if (!int.TryParse(Console.ReadLine(), out int chave)) { Console.WriteLine("Chave inválida."); return; }

            Console.Write("Novo docente resposável pela sala (ou Enter para manter): ");
            string nomeDocente = Console.ReadLine()!;


            Console.Write("Novo curso (ou Enter para manter): ");
            string nomeCurso = Console.ReadLine()!;


            Console.Write("Novo Laboratório (ou Enter para manter): ");
            string novoLaboratorio = Console.ReadLine()!;
            Console.Write("Nova Turma (ou Enter para manter): ");
            string turmaInput = Console.ReadLine()!;
            int? novoNumeroTurma = null;
            if (!string.IsNullOrWhiteSpace(turmaInput)) {
                if (int.TryParse(turmaInput, out int turmaNum))
                    novoNumeroTurma = turmaNum;
                else {
                    Console.WriteLine("Número da turma inválido.");
                    return;
                }
            }


            salaManager.EditarAssociacaoPorNome( chave,string.IsNullOrWhiteSpace(nomeDocente) ? null : nomeDocente, string.IsNullOrWhiteSpace(nomeCurso) ? null : nomeCurso,
                string.IsNullOrWhiteSpace(novoLaboratorio) ? null : novoLaboratorio, novoNumeroTurma);
            salaManager.MostrarSalasDetalhadas();
        }
    }
}
