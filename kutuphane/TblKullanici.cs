using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kutuphane
{
    public class TblKullanici
    {
        public int Id { get; set; }
        public int TakmaAd { get; set; }
        public string Sifre { get; set; }
        public string EPosta { get; set; }
        public string IP { get; set; }
        public string AdSoyad { get; set; }
        public int Tip { get; set; }
        public DateTime KayitTarihi { get; set; }
        public int Durum { get; set; }
        public byte[] Resim { get; set; }
    }
}
