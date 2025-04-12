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
            m.SoruKategori = verik.SoruKategoriVeriGetir();
            m.KonuKategori=verik.KonuKategoriVeriGetir();
            return View(m);
        }
        public IActionResult SoruCevap()
        {
            SoruCevapModel m = new SoruCevapModel();
            TblPostVeri veri = new TblPostVeri();

            m.Sorular = veri.SorularSayfaVeriGetir();
            TblKAtegoriVeri verik = new TblKAtegoriVeri();
            m.SoruKategori = verik.SoruKategoriVeriGetir();
            m.KonuKategori = verik.KonuKategoriVeriGetir();


            return View(m);
        }
        public IActionResult Konu()
        {
            KonuSayfaModel m = new KonuSayfaModel();
            TblPostVeri veri = new TblPostVeri();
            m.Konular = veri.KonularSayfaVeriGetir();
            TblKAtegoriVeri verik = new TblKAtegoriVeri();
            m.KonuKategori = verik.KonuKategoriVeriGetir();
            return View(m);
        }
        [HttpGet]
        public IActionResult KonuAc(int id)
        {
            ViewBag.IdKategori = id;
           
            
            return View();
        }
        [HttpPost]
        public IActionResult KonuAc(KonuAcSayfaModel model,int IdKategori)
        {
            TblPostVeri veri = new TblPostVeri();
            TblPost post = new TblPost();
            post.IP = "1";
            post.Durum = 1;
            post.Tip = 1;
            post.GoruntulenmeSayi = 0;
            post.IdKategori = IdKategori;
            post.IdKullanici = 3;
            post.IdUstPost = 0;
            post.Baslik = model.Baslik;
            post.Icerik = model.Icerik;
            post.IdKategori = model.IdKategori;
            post.KayitTarihi = DateTime.Now;
            veri.TblPostKayitEkle(post);
       
            return View();
        }

    }
}
