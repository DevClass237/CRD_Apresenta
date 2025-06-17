using SenaiKeys.Academicos;
using SenaiKeys.Cursos;
using SenaiKeys.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SenaiKeys.Salas {
    public class SalaManager {

        public List<Sala> salas = new List<Sala>();
        public List<Curso> cursos = new List<Curso>();
        public List<Turma> turmas = new List<Turma>();


        public void EditarAssociacaoPorNome(
        int chave,
        string? nomeDocente = null,
        string? nomeCurso = null,
        string? novoLaboratorio = null,
        int? novoNumeroTurma = null,
        IEnumerable<Usuario>? usuarios = null
    )
        {
            var sala = BuscarSalaPorChave(chave);
            if (sala == null) {
                Console.WriteLine("Sala não encontrada.");
                return;
            }

            int? novoIdDocente = null;
            if (!string.IsNullOrWhiteSpace(nomeDocente) && usuarios != null) {
                var docente = usuarios.FirstOrDefault(u => u.Nome.Equals(nomeDocente, StringComparison.OrdinalIgnoreCase) && u.Cargo == "Docente");
                if (docente == null) {
                    Console.WriteLine("Docente não encontrado.");
                    return;
                }
                novoIdDocente = docente.Matricula;
            }

            int? novoIdCurso = null;
            if (!string.IsNullOrWhiteSpace(nomeCurso)) {
                var curso = cursos.FirstOrDefault(c => c.NomeCurso.Equals(nomeCurso, StringComparison.OrdinalIgnoreCase));
                if (curso == null) {
                    Console.WriteLine("Curso não encontrado.");
                    return;
                }
                novoIdCurso = curso.IdCurso;
            }

            if (novoNumeroTurma.HasValue) {
                var turma = turmas.FirstOrDefault(t => t.NumeroTurma == novoNumeroTurma.Value);
                if (turma == null) {
                    Console.WriteLine("Turma não encontrada.");
                    return;
                }
            }

            if (novoIdDocente.HasValue)
                sala.AlterarDocente(novoIdDocente.Value);
            if (novoIdCurso.HasValue)
                sala.AlterarCurso(novoIdCurso.Value);
            if (!string.IsNullOrWhiteSpace(novoLaboratorio))
                sala.AlterarLaboratorio(novoLaboratorio);
            if (novoNumeroTurma.HasValue)
                sala.AlterarTurma(novoNumeroTurma.Value);

            Console.WriteLine("Associação editada com sucesso!");
        }


        public Sala? BuscarSalaPorChave(int chave)
        {
            return salas.FirstOrDefault(s => s.Chave == chave);
        }
        public IEnumerable<Sala> ListarSalas()
        {
            return salas;
        }
        public void MostrarSalasDetalhadas()
        {
            Console.WriteLine("\n--- Salas Detalhadas ---");
            foreach (var sala in salas) {
                var curso = cursos.FirstOrDefault(c => c.IdCurso == sala.IdCurso);
                var turma = turmas.FirstOrDefault(t => t.IdTurma == sala.IdTurma);
                //var docente = usuarioManager?.BuscarUsuarioPorMatricula(sala.IdDocente);

                string nomeCurso = curso?.NomeCurso ?? "Curso não encontrado";
                string numeroTurma = turma?.NumeroTurma.ToString() ?? "Turma não encontrada";
              //  string nomeDocente = docente?.Nome ?? $"Docente {sala.IdDocente}";

                Console.WriteLine(
                    $"Sala {sala.Chave}, Lab: {sala.Laboratorio}, Docente: {sala.IdDocente}, Curso: {nomeCurso}, Turma: {numeroTurma}"
                );
            }
        }


    }
}