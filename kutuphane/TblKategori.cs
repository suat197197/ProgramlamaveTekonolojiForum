using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kutuphane
{
   public  class TblKategori
    {
        public int IdKategori { get; set; }
        public int IdUstKategori { get; set; }
        public string KategoriAdi { get; set; }
        public string UstKategoriAdi { get; set; }
        public int IdKonu { get; set; }
        public string KonuAdi { get; set; }


    }
}
