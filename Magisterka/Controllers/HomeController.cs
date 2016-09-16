using System.Web.Mvc;

namespace Magisterka.Controllers
{
    using Magisterka.Database;

    public class HomeController : Controller
    {
        private Repository repository = new Repository();

        public ActionResult Index()
        {
            repository.ClientCodeRepository.GetAll();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
