using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PocheteDados.Data;
using PocheteAPI.DTO;
using PocheteModelos.Modelo;

namespace PocheteAPI.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class SalasController : ControllerBase {
        private readonly AppDbContext _context;

        public SalasController(AppDbContext context) {
            _context = context;
        }

        // GET: api/salas
        // Retorna todas as salas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalasDTO>>> GetSalas() {
            var salas = await _context.Salas
                .Select(s => new SalasDTO {
                    Id = (int)s.Id,
                    Nome = s.Nome
                })
                .ToListAsync();

            return Ok(salas);
        }

        // GET: api/salas/{id}
        // Retorna uma sala específica pelo id
        [HttpGet("{id}")]
        public async Task<ActionResult<SalasDTO>> GetSala(long id) {
            var sala = await _context.Salas.FindAsync(id);

            if (sala == null) return NotFound();

            var salaDTO = new SalasDTO {
                Id = (int)sala.Id,
                Nome = sala.Nome
            };

            return Ok(salaDTO);
        }

        // POST: api/salas
        // Cria uma nova sala
        [HttpPost]
        public async Task<ActionResult<SalasDTO>> PostSala(SalasDTO salaDTO) {
            if (await _context.Salas.AnyAsync(s => s.Id == salaDTO.Id)) {
                return Conflict($"Já existe uma sala com ID {salaDTO.Id}.");
            }

            var sala = new Sala {
                Id = salaDTO.Id,
                Nome = salaDTO.Nome
            };

            _context.Salas.Add(sala);
            await _context.SaveChangesAsync();

            salaDTO.Id = (int)sala.Id;

            return CreatedAtAction(nameof(GetSala), new { id = sala.Id }, salaDTO);
        }

        // PUT: api/salas/{id}
        // Atualiza uma sala existente
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSala(long id, SalasDTO salaDTO) {
            if (id != salaDTO.Id) return BadRequest();

            var sala = await _context.Salas.FindAsync(id);
            if (sala == null) return NotFound();

            sala.Nome = salaDTO.Nome;

            _context.Entry(sala).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/salas/{id}
        // Remove uma sala pelo id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSala(long id) {
            var sala = await _context.Salas.FindAsync(id);
            if (sala == null) return NotFound();

            _context.Salas.Remove(sala);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}