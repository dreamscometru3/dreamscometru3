using System.Web.Mvc;

namespace Magisterka.Controllers
{
    using System;

    using Magisterka.Database;
    using Magisterka.ViewModels;

    public class HomeController : Controller
    {
        private Repository repository = new Repository();

        public ActionResult Index()
        {
            repository.ClientCodeRepository.GetAll();

            return View(new HomeIndexViewModel());
        }

        [HttpPost]
        public ActionResult Index(HomeIndexViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Message))
            {
                return this.View(new HomeIndexViewModel() { Message = "Kod jest wymagany" });
            }

            var code = this.repository.ClientCodeRepository.GetByCode(model.Code);

            if (code == null)
            {
                 return this.View(new HomeIndexViewModel() { Message = "Kod nie istnieje" });
            }

            if (code.StartDate > DateTime.Now)
            {
                return this.View(new HomeIndexViewModel() { Message = "Możesz rozpocząc grę po godzinie " + code.StartDate.Hour });
            }

            if (code.EndDate < DateTime.Now)
            {
                return this.View(new HomeIndexViewModel() { Message = "Gra juz zakonczona - " + code.EndDate.Hour });
            }

            return RedirectToAction("Index", "Game", new {id = code.Id});
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
