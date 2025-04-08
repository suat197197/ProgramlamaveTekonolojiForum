using kutuphane;
using Microsoft.AspNetCore.Mvc;
using ProgramlamaveTeknolojiForum.Models;

namespace ProgramlamaveTeknolojiForum.Controllers
{
    public class ForumController : Controller
    {
        public IActionResult AnaSayfa()
        {
            AnaSayfaModel m = new AnaSayfaModel();
            TblPostVeri veri = new TblPostVeri();
            m.KonularSorular= veri.AnaSayfaVeriGetir();
            TblKAtegoriVeri verik = new TblKAtegoriVeri();
            m.KonuSoruKategori = verik.TumKategoriVeriGetir();
            return View(m);
        }
        public IActionResult SoruCevap()
        {
            SoruCevapModel m = new SoruCevapModel();
            TblPostVeri veri = new TblPostVeri();

            m.Sorular = veri.SorularSayfaVeriGetir();
            TblKAtegoriVeri verik = new TblKAtegoriVeri();
            m.SoruKategori = verik.SoruKategoriVeriGetir();
            return View(m);
        }
        public IActionResult Konu()
        {
            KonuModel m = new KonuModel();
            TblPostVeri veri = new TblPostVeri();
            m.Konular = veri.KonularSayfaVeriGetir();
            TblKAtegoriVeri verik = new TblKAtegoriVeri();
            m.KonuKategori = verik.KonuKategoriVeriGetir();
            return View(m);
        }
    }
}
