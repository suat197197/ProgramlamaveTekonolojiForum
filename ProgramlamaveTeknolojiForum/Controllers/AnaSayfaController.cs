using Microsoft.AspNetCore.Mvc;

namespace ProgramlamaveTeknolojiForum.Controllers
{
    public class AnaSayfaController : Controller
    {
        public IActionResult AnaSayfa()
        {
            return View();
        }
    }
}
