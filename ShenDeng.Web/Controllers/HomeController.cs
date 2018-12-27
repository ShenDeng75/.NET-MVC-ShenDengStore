using System.Drawing;
using System.Web.Mvc;

namespace ShenDeng.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}