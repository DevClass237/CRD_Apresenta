using Microsoft.EntityFrameworkCore;
using PocheteDados.Data;
using PocheteModelos.Modelo;

namespace PocheteAPI.Services
{
    public class DadosService
    {
        private readonly AppDbContext _context;

        public DadosService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Monta a tabela: mostra TODAS as salas fixas,
        /// com docente esperado, curso, turma e quem retirou (se tiver).
        /// </summary>
        public async Task<List<LinhaTabela>> ObterDadosAsync()
        {
            // Buscar todas as salas fixas
            var salas = await _context.Salas.ToListAsync();

            // Buscar pochetes (com sala relacionada)
            var pochetes = await _context.Pochetes.Include(p => p.Sala).ToListAsync();

            // Buscar movimentações (com professor e pochete e sala)
            var movimentacoes = await _context.Movimentacoes
                .Include(m => m.Professor)
                .Include(m => m.Pochete)
                    .ThenInclude(p => p.Sala)
                .ToListAsync();

            var resultado = new List<LinhaTabela>();

            foreach (var sala in salas)
            {
                // Pochete vinculada à sala atual
                var pochete = pochetes.FirstOrDefault(p => p.SalaId == sala.Id);

                // Última movimentação dessa pochete
                Movimentacao? ultimaMov = null;
                if (pochete != null)
                {
                    ultimaMov = movimentacoes
                        .Where(m => m.PocheteId == pochete.IdToken)
                        .OrderByDescending(m => m.DataRetirada)
                        .FirstOrDefault();
                }


                // Montar valores padrão caso dados não existam
                string docente = ultimaMov?.Professor?.Nome ?? "Sem docente";
                string nomeCurso = "Sem curso";  // Não existe mais a relação direta com Curso
                string turma = "Sem turma";     // Não existe mais a relação direta com Turma
                string retiradaTexto = ultimaMov != null ? ultimaMov.DataRetirada.ToString("HH:mm") : "Não Retirada";

                resultado.Add(new LinhaTabela(
                    sala: sala.Id.ToString(),
                    laboratorio: sala.Nome,
                    docente: docente,
                    curso: nomeCurso,
                    turma: turma,
                    retirada: retiradaTexto
                ));
            }

            return resultado;
        }

        /// <summary>
        /// Retorna o status real da movimentação para a coluna "Retirada".
        /// </summary>
        public async Task<List<Login>> ObterLoginsAsync()
        {
            // 1️⃣ Pochetes e salas
            var pochetes = await _context.Pochetes
                .Include(p => p.Sala)
                .ToListAsync();

            // 2️⃣ Movimentações mais recentes por pochete
            var logins = pochetes.Select(pochete =>
            {
                var ultimaMov = _context.Movimentacoes
                    .Where(m => m.PocheteId == pochete.IdToken)
                    .OrderByDescending(m => m.DataRetirada)
                    .FirstOrDefault();

                return new Login
                {
                    Sala = pochete.SalaId.ToString(),
                    Laboratorio = pochete.Sala?.Nome ?? "Sala de aula",
                    DocenteChave = ultimaMov?.Professor?.Nome ?? "Disponível",
                    NomeDocenteRetirada = ultimaMov?.Professor?.Nome ?? null,
                    DataHoraRetirada = ultimaMov?.DataRetirada ?? default
                };
            }).ToList();

            return logins;
        }
    }
}
