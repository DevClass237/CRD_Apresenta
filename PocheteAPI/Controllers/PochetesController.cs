using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PocheteDados.Data;
using PocheteAPI.DTO;
using PocheteModelos.Modelo;

namespace PocheteAPI.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class PochetesController : ControllerBase {
        private readonly AppDbContext _context;

        public PochetesController(AppDbContext context) {
            _context = context;
        }

        // GET: api/pochetes
        // Retorna lista de pochetes em formato DTO
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PochetesDTO>>> GetPochetes() {
            var pochetes = await _context.Pochetes.ToListAsync();

            // Mapeia cada entidade para DTO
            var dtoList = pochetes.Select(p => new PochetesDTO {
                Id = p.Id,
                SalaId = p.SalaId,
                IdToken = p.IdToken
            }).ToList();

            return Ok(dtoList);
        }

        // GET: api/pochetes/{id}
        // Retorna uma pochete específica pelo id
        [HttpGet("{id}")]
        public async Task<ActionResult<PochetesDTO>> GetPochete(long id) {
            var pochete = await _context.Pochetes.FindAsync(id);
            if (pochete == null)
                return NotFound();

            var dto = new PochetesDTO {
                Id = pochete.Id,
                SalaId = pochete.SalaId,
                IdToken = pochete.IdToken
            };

            return Ok(dto);
        }

        // POST: api/pochetes
        // Cria uma nova pochete a partir do DTO recebido
        [HttpPost]
        public async Task<ActionResult<PochetesDTO>> PostPochete(PochetesDTO dto) {
            var pochete = new Pochete {
                SalaId = dto.SalaId,
                IdToken = dto.IdToken
            };

            _context.Pochetes.Add(pochete);
            await _context.SaveChangesAsync();

            // Atualiza o DTO com o Id gerado pelo banco
            dto.Id = pochete.Id;

            return CreatedAtAction(nameof(GetPochete), new { id = dto.Id }, dto);
        }

        // PUT: api/pochetes/{id}
        // Atualiza uma pochete existente
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPochete(long id, PochetesDTO dto) {
            if (id != dto.Id)
                return BadRequest();

            var pochete = await _context.Pochetes.FindAsync(id);
            if (pochete == null)
                return NotFound();

            // Atualiza os campos da entidade com os dados do DTO
            pochete.SalaId = dto.SalaId;
            pochete.IdToken = dto.IdToken;

            _context.Entry(pochete).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/pochetes/{id}
        // Remove uma pochete pelo id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePochete(long id) {
            var pochete = await _context.Pochetes.FindAsync(id);
            if (pochete == null)
                return NotFound();

            _context.Pochetes.Remove(pochete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
