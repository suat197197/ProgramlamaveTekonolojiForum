using kutuphane;

namespace ProgramlamaveTeknolojiForum.Models
{
    public class KonuAcSayfaModel
    {
        public int Id { get; set; }
        public int IdKullanici { get; set; }
        public int IdKategori { get; set; }
        public int IdUstPost { get; set; }
        public int Tip { get; set; }
        public string IP { get; set; }
        public string Baslik { get; set; }
        public DateTime KayitTarihi { get; set; }
        public int GoruntulenmeSayi { get; set; }
        public int Durum { get; set; }
        public string Icerik { get; set; }
    }
}
