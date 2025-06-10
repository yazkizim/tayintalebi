using System;

namespace ABPersonelTayinAPI.Entities
{
    public class TayinTalebi
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string TalepTuru { get; set; }  // "Aile Birliği", "Sağlık" vs.
        public string IlTercihi { get; set; }
        public string Aciklama { get; set; }
        public DateTime BasvuruTarihi { get; set; }

        public personel personel { get; set; }

        public string Sonuc { get; set; } = "Değerlendirilmedi";
    }
}
