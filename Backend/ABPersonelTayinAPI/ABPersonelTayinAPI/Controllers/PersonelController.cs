using ABPersonelTayinAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace ABPersonelTayinAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonelController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PersonelController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<personel>>> GetPersons()
        {
            return await _context.Kisiler.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<personel>> PostPerson(personel person)
        {
            _context.Kisiler.Add(person);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<personel>> GetPerson(int id)
        {
            var person = await _context.Kisiler.FindAsync(id);
            if (person == null)
                return NotFound();

            return Ok(person);
        }
    }

}
