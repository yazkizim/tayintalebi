using System.Data;

namespace ABPersonelTayinAPI.Entities
{
    public class personel
    {
        public int Id { get; set; }
        public string SicilNo { get; set; }
        public string FullName { get; set; }
        public string Gorevyeri { get; set; }
        public int RoleId { get; set; }
        public string Role { get; set; }        
        public string PasswordHash { get; set; }

        public List<TayinTalebi>? Talepler { get; set; }
    }
}
