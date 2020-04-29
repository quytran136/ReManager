using System.Web.Mvc;

namespace ResManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Tổng quan hoạt động";
            return View();
        }
    }
}