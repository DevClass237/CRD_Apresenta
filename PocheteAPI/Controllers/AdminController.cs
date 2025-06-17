using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PocheteDados.Data;
using PocheteAPI.DTO;
using PocheteModelos.Modelo;

namespace PocheteAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminDTO>>> GetAdmins() {
            var admins = await _context.Admins
                .Select(a => new AdminDTO {
                    Id = a.Id,
                    Nome = a.Nome,
                    Senha = a.Senha
                })
                .ToListAsync();

            return Ok(admins);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdminDTO>> GetAdmin(long id) {
            var admin = await _context.Admins
                .Where(a => a.Id == id)
                .Select(a => new AdminDTO {
                    Id = a.Id,
                    Nome = a.Nome,
                    Senha = a.Senha
                })
                .FirstOrDefaultAsync();

            return admin == null ? NotFound() : Ok(admin);
        }

        //[HttpPost]
        //public async Task<ActionResult<AdminDTO>> PostAdmin(AdminDTO dto) {
        //    var admin = new Admin {
        //        Nome = dto.Nome,
        //        Senha = dto.Senha
        //    };

        //    _context.Admins.Add(admin);
        //    await _context.SaveChangesAsync();

        //    dto.Id = admin.Id;

        //    return CreatedAtAction(nameof(GetAdmin), new { id = dto.Id }, dto);
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdmin(long id, AdminDTO dto) {
            if (id != dto.Id) return BadRequest();

            var admin = await _context.Admins.FindAsync(id);
            if (admin == null) return NotFound();

            admin.Nome = dto.Nome;
            admin.Senha = dto.Senha;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(long id) {
            var admin = await _context.Admins.FindAsync(id);
            if (admin == null) return NotFound();

            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AdminDTO admin) {
            if (admin == null || string.IsNullOrEmpty(admin.Nome) || string.IsNullOrEmpty(admin.Senha))
                return BadRequest("Dados inválidos.");

            var entidade = new Admin {
                Nome = admin.Nome,
                Senha = admin.Senha
            };

            _context.Admins.Add(entidade);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Post), new { id = entidade.Id }, admin);
        }

        [HttpPost("login")]
        public ActionResult<AdminDTO> Login([FromBody] LoginRequestDTO loginDto) {
            var admin = _context.Admins
                .FirstOrDefault(a => a.Nome == loginDto.Nome && a.Senha == loginDto.Senha);

            if (admin == null) {
                return Unauthorized("Nome ou senha inválidos.");
            }

            // Retorna o admin encontrado (ou apenas dados essenciais, se preferir)
            return new AdminDTO {
                Id = admin.Id,
                Nome = admin.Nome,
                Senha = admin.Senha // ou remova se não quiser devolver a senha
            };
        }

    }
}
