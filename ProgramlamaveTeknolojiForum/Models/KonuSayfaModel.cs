

using kutuphane;

namespace ProgramlamaveTeknolojiForum.Models
{
    public class KonuSayfaModel
    {
        public List<VMPost> Konular = new List<VMPost>();
        
        public List<TblKategori> KonuKategori = new List<TblKategori>();
        public TblPostlar AnaPost = new TblPostlar();
        public List<TblPostlar> DigerPostlar = new List<TblPostlar>();
        public string Icerik { get; set; }
    }
}
