using System.Web.Mvc;

namespace Proyecto_Super_Almendro.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contacto() 
        { 
            return View();  
        }
    }
}