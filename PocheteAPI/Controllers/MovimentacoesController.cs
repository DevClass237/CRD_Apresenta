using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PocheteDados.Data;
using PocheteAPI.DTO;
using PocheteModelos.Modelo;

namespace PocheteAPI.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class MovimentacoesController : ControllerBase {
        private readonly AppDbContext _context;

        public MovimentacoesController(AppDbContext context) {
            _context = context;
        }

        // GET: api/movimentacoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovimentacaoDTO>>> GetAll() {
            var movimentacoes = await _context.Movimentacoes
                .Select(m => new MovimentacaoDTO {
                    Id = m.Id,
                    ProfessorMatricula = m.ProfessorMatricula,
                    PocheteId = m.PocheteId,
                    DataRetirada = m.DataRetirada,
                    DataDevolucao = m.DataDevolucao
                })
                .ToListAsync();

            return Ok(movimentacoes);
        }

        // GET: api/movimentacoes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MovimentacaoDTO>> GetById(long id) {
            var m = await _context.Movimentacoes.FindAsync(id);
            if (m == null) return NotFound();

            var dto = new MovimentacaoDTO {
                Id = m.Id,
                ProfessorMatricula = m.ProfessorMatricula,
                PocheteId = m.PocheteId,
                DataRetirada = m.DataRetirada,
                DataDevolucao = m.DataDevolucao
            };

            return Ok(dto);
        }

        // POST: api/movimentacoes/retirada
        [HttpPost("retirada")]
        public async Task<ActionResult<MovimentacaoDTO>> RegistrarRetirada(MovimentacaoDTO dto) {
            var movimentacao = new Movimentacao {
                ProfessorMatricula = dto.ProfessorMatricula,
                PocheteId = dto.PocheteId,
                DataRetirada = DateTime.Now, // <- Hora local do sistema
                DataDevolucao = null
            };

            _context.Movimentacoes.Add(movimentacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = movimentacao.Id }, new MovimentacaoDTO {
                Id = movimentacao.Id,
                ProfessorMatricula = movimentacao.ProfessorMatricula,
                PocheteId = movimentacao.PocheteId,
                DataRetirada = movimentacao.DataRetirada,
                DataDevolucao = null
            });
        }

        // PUT: api/movimentacoes/devolucao
        [HttpPut("devolucao")]
        public async Task<IActionResult> RegistrarDevolucao(MovimentacaoDTO dto) {
            var movimentacao = await _context.Movimentacoes
                .Where(m => m.ProfessorMatricula == dto.ProfessorMatricula
                            && m.PocheteId == dto.PocheteId
                            && m.DataDevolucao == null)
                .FirstOrDefaultAsync();

            if (movimentacao == null) {
                return NotFound("Nenhuma movimentação aberta encontrada para devolução.");
            }

            movimentacao.DataDevolucao = DateTime.Now; // <- Hora local do sistema
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/movimentacoes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id) {
            var movimentacao = await _context.Movimentacoes.FindAsync(id);
            if (movimentacao == null) return NotFound();

            _context.Movimentacoes.Remove(movimentacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
