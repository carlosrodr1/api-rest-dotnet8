using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserAuthService.API.Data;
using UserAuthService.API.Dtos;
using UserAuthService.API.Models;
using UserAuthService.API.Utils;

namespace UserAuthService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioDbContext _context;

        public UsuariosController(UsuarioDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Cadastrar(CadastrarUsuarioDto dto)
        {
            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                SenhaHash = SenhaHasher.GerarHash(dto.Senha)
            };

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return Created("", new { usuario.Id, usuario.Nome, usuario.Email });
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequestDto dto, [FromServices] IConfiguration config)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == dto.Email);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.SenhaHash))
                return Unauthorized(new { mensagem = "Email ou senha inválidos." });

            var token = GeradorJwt.GerarToken(usuario.Email, config);

            return Ok(new { token });
        }

        [HttpGet]
        [Authorize]
        public IActionResult ListarUsuarios()
        {
            var usuarios = _context.Usuarios
                .Select(u => new { u.Id, u.Nome, u.Email })
                .ToList();

            return Ok(usuarios);
        }

    }
}
