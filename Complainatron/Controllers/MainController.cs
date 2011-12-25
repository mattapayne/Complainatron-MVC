using System.Web.Mvc;

namespace Complainatron.Controllers
{
    public class MainController : Controller
    {
        [HttpGet]
        public ActionResult About()
        {
            return PartialView("_About");
        }
    }
}
