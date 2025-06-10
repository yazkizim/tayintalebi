using ABPersonelTayinAPI.Entities;

namespace ABPersonelTayinAPI.Services
{
    public class LogService
    {
        private readonly ApplicationDbContext _context;

        public LogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task LogAsync(string islem, string detay, int? personId = null)
        {
            var log = new Log
            {
                Islem = islem,
                Detay = detay,
                PersonId = personId
            };

            _context.Loglar.Add(log);
            await _context.SaveChangesAsync();
        }
    }

}
