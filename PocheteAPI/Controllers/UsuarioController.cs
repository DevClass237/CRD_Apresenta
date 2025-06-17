using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PocheteDados.Data;
using PocheteModelos.Modelo;

namespace PocheteAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/usuario
        // Retorna todos os usuários
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios
                .Select(u => new Usuario
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Senha = u.Senha
                })
                .ToListAsync();

            return Ok(usuarios);
        }

        // GET: api/usuario/{id}
        // Retorna um usuário específico
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(long id)
        {
            var usuario = await _context.Usuarios
                .Where(u => u.Id == id)
                .Select(u => new Usuario
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Senha = u.Senha
                })
                .FirstOrDefaultAsync();

            return usuario == null ? NotFound() : Ok(usuario);
        }

        // POST: api/usuario
        // Cria um novo usuário
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario dto)
        {
            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Senha = dto.Senha
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            dto.Id = usuario.Id;

            return CreatedAtAction(nameof(GetUsuario), new { id = dto.Id }, dto);
        }

        // PUT: api/usuario/{id}
        // Atualiza um usuário existente
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(long id, Usuario dto)
        {
            if (id != dto.Id) return BadRequest();

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();

            usuario.Nome = dto.Nome;
            usuario.Senha = dto.Senha;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/usuario/{id}
        // Remove um usuário
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(long id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
