using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PocheteDados.Data;
using PocheteAPI.DTO;
using PocheteModelos.Modelo;

namespace PocheteAPI.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessoresController : ControllerBase {
        private readonly AppDbContext _context;

        public ProfessoresController(AppDbContext context) {
            _context = context;
        }

        // GET: api/professores
        // Retorna todos os professores como DTOs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfessoresDTO>>> GetProfessores() {
            var professores = await _context.Professores.ToListAsync();

            var dtoList = professores.Select(p => new ProfessoresDTO {
                Matricula = p.Matricula,
                Nome = p.Nome
            }).ToList();

            return Ok(dtoList);
        }

        // GET: api/professores/{matricula}
        // Retorna um professor específico pelo número de matrícula
        [HttpGet("{matricula}")]
        public async Task<ActionResult<ProfessoresDTO>> GetProfessor(int matricula) {
            var professor = await _context.Professores.FindAsync(matricula);

            if (professor == null)
                return NotFound();

            var dto = new ProfessoresDTO {
                Matricula = professor.Matricula,
                Nome = professor.Nome
            };

            return Ok(dto);
        }

        // POST: api/professores
        // Cria um novo professor a partir do DTO
        [HttpPost]
        public async Task<ActionResult<ProfessoresDTO>> PostProfessor(ProfessoresDTO dto)
        {
            Console.WriteLine($"Recebendo professor - Matrícula: {dto.Matricula}, Nome: {dto.Nome}");

            var professor = new Professor
            {
                Matricula = dto.Matricula,
                Nome = dto.Nome
            };

            _context.Professores.Add(professor);
            await _context.SaveChangesAsync();

            dto.Matricula = professor.Matricula;

            return CreatedAtAction(nameof(GetProfessor), new { matricula = dto.Matricula }, dto);
        }



        // PUT: api/professores/{matricula}
        // Atualiza um professor existente
        [HttpPut("{matricula}")]
        public async Task<IActionResult> PutProfessor(int matricula, ProfessoresDTO dto) {
            if (matricula != dto.Matricula)
                return BadRequest();

            var professor = await _context.Professores.FindAsync(matricula);
            if (professor == null)
                return NotFound();

            professor.Nome = dto.Nome;

            _context.Entry(professor).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/professores/{matricula}
        // Remove um professor pelo número da matrícula
        [HttpDelete("{matricula}")]
        public async Task<IActionResult> DeleteProfessor(int matricula) {
            var professor = await _context.Professores.FindAsync(matricula);
            if (professor == null)
                return NotFound();

            _context.Professores.Remove(professor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}