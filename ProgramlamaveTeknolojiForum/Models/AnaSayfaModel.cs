

using kutuphane;

namespace ProgramlamaveTeknolojiForum.Models
{
    public class AnaSayfaModel
    {
        public List<VMPost> KonularSorular { get; set; }
       
        public List<TblKategori> KonuSoruKategori { get; set; }
        public List<TblKategori> SoruKategori  { get; set; }
        public List<TblKategori> KonuKategori { get; set; }
        public KullaniciYetki yetki { get; set; }

    }
}
