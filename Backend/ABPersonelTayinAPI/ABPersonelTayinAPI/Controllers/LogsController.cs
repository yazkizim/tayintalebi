namespace ABPersonelTayinAPI.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("api/[controller]")]
    public class LogsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Yönetici")]
        public async Task<IActionResult> GetLogs()
        {
            var logs = await _context.Loglar
                .Include(l => l.Person)
                .OrderByDescending(l => l.Tarih)
                .Take(100)
                .ToListAsync();

            return Ok(logs.Select(l => new
            {
                l.Id,
                l.Islem,
                l.Detay,
                l.Tarih,
                Personel = l.Person != null ? l.Person.FullName : "Sistem"
            }));
        }
    }

}
