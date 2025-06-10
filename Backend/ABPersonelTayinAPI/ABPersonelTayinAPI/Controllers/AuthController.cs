namespace ABPersonelTayinAPI.Controllers
{
    using ABPersonelTayinAPI.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtService _jwtService;

        private readonly LogService _logService;

        public AuthController(ApplicationDbContext context, JwtService jwt, LogService logService)
        {
            _context = context;
            _jwtService = jwt;
            _logService = logService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = _context.Kisiler.SingleOrDefault(x => x.SicilNo == dto.SicilNo);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                await _logService.LogAsync("Hatalı Giriş", $"Sicil No: {dto.SicilNo}", null);
                return Unauthorized("Hatalı sicil no veya şifre.");
            }

            var role = _context.Roles.Find(user.RoleId)?.Name ?? "Personel";
            var token = _jwtService.GenerateToken(user.SicilNo, role);

            await _logService.LogAsync("Login", "Başarılı giriş yapıldı.", user.Id);

            return Ok(new { token });
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var sicilNo = User.Identity.Name;
            var user = _context.Kisiler.FirstOrDefault(p => p.SicilNo == sicilNo);

            await _logService.LogAsync("Logout", "Çıkış yapıldı.", user?.Id);

            return Ok(new { message = "Çıkış başarılı." });
        }
    }

    public class LoginDto
    {
        public string SicilNo { get; set; }
        public string Password { get; set; }
    }

}
