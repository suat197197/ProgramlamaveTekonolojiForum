using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kutuphane
{
    public class VMPost
    {
        public int KulaniciId { get; set; }
        public string TakmaAd { get; set; }
        public DateTime KullaniciKayitTarihi { get; set; }
        public int KullaniciTip { get; set; }
        public int KonuId { get; set; }
        public string KategoriBaslik { get; set; }
        public int KategoriId { get; set; }
        public int PostId { get; set; }
        public string PostBaslik { get; set; }
        public int GoruntulenmeSayi { get; set; }
        public DateTime PostKayitTarihi { get; set; }
        public int PostTip { get; set; }
        public int IdUstPost { get; set; }
        public string Icerik { get; set; }
        public int YorumSayi { get; set; }
        public int IdUstKategori { get; set; }
        public string UstKategoriAdi { get; set; }
        public byte[] Resim { get; set; }



    }
}
