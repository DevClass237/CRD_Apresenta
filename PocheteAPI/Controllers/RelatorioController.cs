/*using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PocheteDados.Data;
using PocheteAPI.DTO;

namespace PocheteAPI.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class RelatoriosController : ControllerBase {
        private readonly AppDbContext _context;

        public RelatoriosController(AppDbContext context) {
            _context = context;
        }

        // GET: api/relatorios
        // Retorna o relatório completo com as informações combinadas das tabelas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RelatorioDTO>>> GetRelatorios() {
            // Query para juntar as tabelas e montar o DTO do relatório
            var relatorios = await (
                from m in _context.Movimentacoes
                join p in _context.Professores on m.ProfessorMatricula equals p.Matricula
                join poc in _context.Pochetes on m.PocheteId equals poc.IdToken
                join s in _context.Salas on poc.SalaId equals s.Id
                join c in _context.Cursos on p.Matricula equals c.ProfessorMatricula
                join t in _context.Turmas on c.TurmaId equals t.Id
                select new RelatorioDTO {
                    Sala = s.Nome,
                    Laboratorio = s.Nome, // Pode ser alterado para outro campo se houver
                    Docente = p.Nome,
                    Curso = c.Nome,
                    Turma = t.Identificador
                }
            ).ToListAsync();

            return Ok(relatorios);
        }
    }
}*/