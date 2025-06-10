using System;

namespace ABPersonelTayinAPI.Entities
{
    public class Log
    {
        public int Id { get; set; }
        public int? PersonId { get; set; }  // null olabilir (örneğin hatalı login)
        public string Islem { get; set; }   // "Login", "Logout", "Tayin Talebi", "Hata" gibi
        public string Detay { get; set; }
        public DateTime Tarih { get; set; } = DateTime.Now;

        public personel Person { get; set; }
    }

}
