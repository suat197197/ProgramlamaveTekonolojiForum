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
            return View(m);
        }
        public IActionResult SoruCevap()
        {
            SoruCevapModel m = new SoruCevapModel();
            TblPostVeri veri = new TblPostVeri();

            m.Sorular = veri.SorularSayfaVeriGetir();
            return View(m);
        }
        public IActionResult Konu()
        {
            KonuModel m = new KonuModel();
            TblPostVeri veri = new TblPostVeri();
            m.Konular = veri.KonularSayfaVeriGetir();
            return View(m);
        }
    }
}
