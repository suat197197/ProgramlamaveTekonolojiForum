using kutuphane;
using Microsoft.AspNetCore.Mvc;
using ProgramlamaveTeknolojiForum.Models;
using static System.Net.Mime.MediaTypeNames;

namespace ProgramlamaveTeknolojiForum.Controllers
{
    public class KullaniciController : Controller
    {
        public void Cookiesil()
        {
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
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
            Response.Cookies.Append("TakmaAd", yetki.KullaniciTip.ToString(), options);

            HttpContext.Session.SetString("IdKullanici", yetki.IdKullanici.ToString());
            HttpContext.Session.SetString("KullaniciTip", yetki.KullaniciTip.ToString());
            HttpContext.Session.SetString("TakmaAd", yetki.TakmaAd.ToString());

        }
        public IActionResult Giris()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Giris(string username, string password)
        {
            TblKullaniciVeri veri = new TblKullaniciVeri();
            KullaniciYetki yetki = veri.KullaniciGiris(username, password);
            KullaniciYetkiEkle(yetki);
            if (yetki.IdKullanici == 0)
            {
                string mesaj = "Kullanıcı Adı ya da Şifre Uyuşmuyor";
                ViewBag.mesaj = mesaj;
                ViewBag.Dogrumu = 0;
                return View();
            }
            else
            {
                ViewBag.Dogrumu = 1;
                return RedirectToAction("AnaSayfa", "Forum");
            }

        }
        public IActionResult Kayit()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Kayit(string fullname, string username, string email, string password, IFormFile Resim)
        {



            TblKullaniciVeri veri = new TblKullaniciVeri();
            TblKullanici k = new TblKullanici();
            k.IP = null;
            k.KayitTarihi = DateTime.Now;
            k.Sifre = password;
            k.Tip = 3;
            k.Durum = 1;
            k.EPosta = email;
            if (Resim.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    Resim.CopyTo(ms);
                    k.Resim = ms.ToArray();
                }
            }
            k.AdSoyad = fullname;
            veri.TblKullaniciKayitEkle(k);
            return View();

        }
    }
}