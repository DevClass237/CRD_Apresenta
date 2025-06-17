using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PocheteDados.Data;
using PocheteAPI.DTO;
using PocheteModelos.Modelo;

namespace PocheteAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CursosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CursosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/cursos
        // Retorna todos os cursos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CursosDTO>>> GetCursos()
        {
            var cursos = await _context.Cursos
                .Select(c => new CursosDTO
                {
                    Id = c.Id,
                    Nome = c.Nome
                })
                .ToListAsync();

            return Ok(cursos);
        }

        // GET: api/cursos/{id}
        // Retorna um curso específico
        [HttpGet("{id}")]
        public async Task<ActionResult<CursosDTO>> GetCurso(long id)
        {
            var curso = await _context.Cursos
                .Where(c => c.Id == id)
                .Select(c => new CursosDTO
                {
                    Id = c.Id,
                    Nome = c.Nome
                })
                .FirstOrDefaultAsync();

            return curso == null ? NotFound() : Ok(curso);
        }

        // POST: api/cursos
        // Cria um novo curso
        [HttpPost]
        public async Task<ActionResult<CursosDTO>> PostCurso(CursosDTO dto)
        {
            var curso = new Curso
            {
                Nome = dto.Nome
            };

            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();

            dto.Id = curso.Id;

            return CreatedAtAction(nameof(GetCurso), new { id = dto.Id }, dto);
        }

        // PUT: api/cursos/{id}
        // Atualiza um curso existente
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurso(long id, CursosDTO dto)
        {
            if (id != dto.Id) return BadRequest();

            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null) return NotFound();

            curso.Nome = dto.Nome;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/cursos/{id}
        // Remove um curso
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurso(long id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null) return NotFound();

            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
