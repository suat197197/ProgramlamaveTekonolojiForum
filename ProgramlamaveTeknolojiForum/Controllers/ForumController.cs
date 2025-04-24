using kutuphane;
using Microsoft.AspNetCore.Mvc;
using ProgramlamaveTeknolojiForum.Models;
using System.Globalization;
using System.Text.Json;
namespace ProgramlamaveTeknolojiForum.Controllers
{
    public class ForumController : Controller
    {
        public void Cookiesil()
        {
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
        }
        public FileResult GetFileFromBytes(byte[] bytesIn)
        {
            return File(bytesIn, "image/png");
        }
        public void Sessionsil()
        {
            HttpContext.Session.Clear();
        }
        public KullaniciYetki KullaniciYetkiGetir()
        {
            KullaniciYetki y = new KullaniciYetki();
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("IdKullanici")) == true
                || string.IsNullOrEmpty(HttpContext.Session.GetString("IdKullanici")) == true)
            {
                if (Request.Cookies["IdKullanici"] == null || Request.Cookies["KullaniciTip"] == null)
                {
                    y.IdKullanici = 0;
                    y.KullaniciTip = 0;
                    y.TakmaAd = "";
                }
                else
                {
                    y.IdKullanici = int.Parse(Request.Cookies["IdKullanici"].ToString());
                    y.KullaniciTip = int.Parse(Request.Cookies["KullaniciTip"].ToString());
                    y.TakmaAd = Request.Cookies["TakmaAd"].ToString();
                }
            }
            else
            {
                y.IdKullanici = int.Parse(HttpContext.Session.GetString("IdKullanici").ToString());
                y.KullaniciTip = int.Parse(HttpContext.Session.GetString("KullaniciTip").ToString());
                y.TakmaAd = HttpContext.Session.GetString("TakmaAd");

            }


            return y;
        }
        public void KullaniciYetkiEkle(KullaniciYetki yetki)
        {
            CookieOptions options = new CookieOptions
            {
                Domain = "proteknoforum.com", // Set the domain for the cookie
                Expires = DateTime.Now.AddDays(7), // Set expiration date to 7 days from now
                Path = "/", // Cookie is available within the entire application
                Secure = true, // Ensure the cookie is only sent over HTTPS
                HttpOnly = true, // Prevent client-side scripts from accessing the cookie
                MaxAge = TimeSpan.FromDays(180), // Another way to set the expiration time
                IsEssential = true // Indicates the cookie is essential for the application to function
            };
            Response.Cookies.Append("IdKullanici", yetki.IdKullanici.ToString(), options);
            Response.Cookies.Append("KullaniciTip", yetki.KullaniciTip.ToString(), options);
            Response.Cookies.Append("TakmaAd", yetki.TakmaAd.ToString(), options);

            HttpContext.Session.SetString("IdKullanici", yetki.IdKullanici.ToString());
            HttpContext.Session.SetString("KullaniciTip", yetki.KullaniciTip.ToString());
            HttpContext.Session.SetString("TakmaAd", yetki.TakmaAd.ToString());

        }
        public IActionResult AnaSayfa()
        {


            AnaSayfaModel m = new AnaSayfaModel();
            m.yetki = KullaniciYetkiGetir();
            TblPostVeri veri = new TblPostVeri();
            m.KonularSorular = veri.AnaSayfaVeriGetir();
            TblKAtegoriVeri verik = new TblKAtegoriVeri();
            m.KonuSoruKategori = verik.TumKategoriVeriGetir();
            m.SoruKategori = verik.SoruKategoriVeriGetir();
            m.KonuKategori = verik.KonuKategoriVeriGetir();
            KullaniciYetki yetki = new KullaniciYetki();
            yetki = KullaniciYetkiGetir();
            m.yetki = yetki;
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


        [HttpGet]
        public IActionResult Konu(int id, int AnaPostId)
        {
            KonuSayfaModel m = new KonuSayfaModel();
            TblPostVeri veri = new TblPostVeri();
            m.Konular = veri.KonularSayfaVeriGetir();
            TblKAtegoriVeri verik = new TblKAtegoriVeri();
            m.KonuKategori = verik.KonuKategoriVeriGetir();
            List<TblPostlar> postlar = new List<TblPostlar>();
            if (id!=0)
            {
                postlar = veri.TblTumPostKayitGetir_Id(id);
            }
            else
            {
                postlar= veri.TblTumPostKayitGetir_Id(AnaPostId);
            }
            m.AnaPost = postlar.Single(c => c.IdUstPost == 0);
            ViewBag.AnaPostId = m.AnaPost.Id;
            ViewBag.IdKategori = m.AnaPost.IdKategori;
            m.DigerPostlar = postlar.Where(c => c.IdUstPost != 0).ToList();
            return View(m);
        }
        [HttpPost]
        public IActionResult Konu(KonuSayfaModel model, int IdKategori, int AnaPostId)
        {
            TblPostVeri veri = new TblPostVeri();
            TblPost post = new TblPost();
            post.IP = "1";
            post.Durum = 1;
            post.KayitTarihi = DateTime.Now;
            post.Icerik = model.Icerik;
            post.IdKullanici = KullaniciYetkiGetir().IdKullanici;
            post.GoruntulenmeSayi = 0;
            post.IdKategori = IdKategori;
            post.IdUstPost = AnaPostId;
            post.BegenmeSayi = 0;
            veri.TblPostKayitEkle(post);
            return RedirectToAction("Konu", new { id = AnaPostId });
        }
        [HttpGet]
        public IActionResult KonuAc(int id)
        {
            ViewBag.IdKategori = id;
            return View();
        }
        [HttpPost]
        public IActionResult KonuAc(KonuAcSayfaModel model, int IdKategori)
        {
            KullaniciYetki y = KullaniciYetkiGetir();
            TblPostVeri veri = new TblPostVeri();
            TblPost post = new TblPost();
            post.IP = "1";
            post.Durum = 1;
            post.IdKullanici = KullaniciYetkiGetir().IdKullanici;
            post.GoruntulenmeSayi = 0;
            post.IdKategori = IdKategori;
            post.BegenmeSayi = 0;
            post.IdKullanici = y.IdKullanici;
            post.IdUstPost = 0;
            post.Baslik = model.Baslik;
            post.Icerik = model.Icerik;
            post.KayitTarihi = DateTime.Now;
            veri.TblPostKayitEkle(post);

            return View();
        }
        [HttpPost]
        public JsonResult KullaniciGiris(string KullaniciAdi, string Sifre)
        {
            TblKullaniciVeri kveri = new TblKullaniciVeri();
            KullaniciYetki yetki = new KullaniciYetki();
            yetki = kveri.KullaniciGiris(KullaniciAdi, Sifre);
            KullaniciYetkiEkle(yetki);
            return Json(yetki);
        }
    }
}
