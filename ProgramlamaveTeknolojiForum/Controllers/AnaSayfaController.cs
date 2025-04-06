using kutuphane;
using Microsoft.AspNetCore.Mvc;
using ProgramlamaveTeknolojiForum.Models;

namespace ProgramlamaveTeknolojiForum.Controllers
{
    public class AnaSayfaController : Controller
    {
        public IActionResult AnaSayfa()
        {
            AnaSayfaModel m = new AnaSayfaModel();
            TblPostVeri veri = new TblPostVeri();
            m.Sorular= veri.AnaSayfaVeriGetir();
            return View(m);
        }
    }
}
