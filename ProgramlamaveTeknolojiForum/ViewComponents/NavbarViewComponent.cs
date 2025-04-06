using Microsoft.AspNetCore.Mvc;

namespace ProgramlamaveTeknolojiForum.ViewComponents
{
    public class NavbarViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
