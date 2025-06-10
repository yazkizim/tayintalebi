using ABPersonelTayinAPI.Entities;
using ABPersonelTayinAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace ABPersonelTayinAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TayinTalebiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly LogService _logService;

        public TayinTalebiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize(Roles = "Personel,Yönetici")]
        public async Task<IActionResult> Ekle(TayinTalebi talep)
        {
            var sicilNo = User.Identity.Name;
            var person = await _context.Kisiler.FirstOrDefaultAsync(p => p.SicilNo == sicilNo);

            if (person == null) return Unauthorized();

            talep.PersonId = person.Id;
            talep.BasvuruTarihi = DateTime.Now;
            talep.Sonuc = "Değerlendirilmedi";

            _context.Talepler.Add(talep);
            await _context.SaveChangesAsync();

            await _logService.LogAsync("Talep Ekleme", $"Talep eklendi: {talep.TalepTuru} - {talep.IlTercihi}", person.Id);

            return Ok(talep);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Personel,Yönetici")]
        public async Task<IActionResult> Sil(int id)
        {
            var sicilNo = User.Identity.Name;
            var person = await _context.Kisiler.FirstOrDefaultAsync(p => p.SicilNo == sicilNo);

            var talep = await _context.Talepler.FirstOrDefaultAsync(t => t.Id == id && t.PersonId == person.Id);
            if (talep == null) return NotFound("Talep bulunamadı veya sizin değil.");

            _context.Talepler.Remove(talep);
            await _context.SaveChangesAsync();

            await _logService.LogAsync("Talep Silme", $"Talep silindi: {talep.Id}", person.Id);

            return Ok(new { message = "Talep silindi." });
        }

        [HttpPut("onayla/{id}")]
        [Authorize(Roles = "Yönetici")]
        public async Task<IActionResult> Onayla(int id)
        {
            var talep = await _context.Talepler.Include(t => t.personel).FirstOrDefaultAsync(t => t.Id == id);
            if (talep == null) return NotFound();

            talep.Sonuc = "Onaylandı";
            await _context.SaveChangesAsync();

            await _logService.LogAsync("Talep Onayı", $"Talep onaylandı: {talep.Id}", talep.PersonId);

            return Ok(new { message = "Talep onaylandı." });
        }

        [HttpPut("reddet/{id}")]
        [Authorize(Roles = "Yönetici")]
        public async Task<IActionResult> Reddet(int id)
        {
            var talep = await _context.Talepler.Include(t => t.personel).FirstOrDefaultAsync(t => t.Id == id);
            if (talep == null) return NotFound();

            talep.Sonuc = "Reddedildi";
            await _context.SaveChangesAsync();

            await _logService.LogAsync("Talep Reddetme", $"Talep reddedildi: {talep.Id}", talep.PersonId);

            return Ok(new { message = "Talep reddedildi." });
        }

        [HttpGet("tum")]
        [Authorize(Roles = "Yönetici")]
        public async Task<IActionResult> TumTalepler()
        {
            var sicilNo = User.Identity.Name;
            var yonetici = await _context.Kisiler.FirstOrDefaultAsync(p => p.SicilNo == sicilNo);

            var talepler = await _context.Talepler
                .Include(t => t.personel)
                .OrderByDescending(t => t.BasvuruTarihi)
                .ToListAsync();

            await _logService.LogAsync(
                "Tüm Talepleri Listeleme",
                $"Yönetici {yonetici.FullName} ({yonetici.SicilNo}) talepleri listeledi.",
                yonetici.Id
            );

            return Ok(talepler);
        }


       

        
    }
}
