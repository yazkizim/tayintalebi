using System;

namespace ABPersonelTayinAPI.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<personel> Persons { get; set; }
    }
}
