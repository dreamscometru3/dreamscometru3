using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Magisterka.Controllers
{
    using Magisterka.Database;
    using Magisterka.Models;

    public class GameController : BaseController
    {
        private Repository repository = new Repository();

        public ActionResult Index(int id)
        {
            var clientCode = this.repository.ClientCodeRepository.GetByID(id);
            this.repository.ClientCodeRepository.UpdateStatus(clientCode);

            this.Save("ClientCode", clientCode.Id.ToString());

            var survey = new SurveyModel() { ClientCode = clientCode};

            return View(survey);
        }

        [HttpPost]
        public ActionResult SaveSurvey(SurveyModel model)
        {
            this.repository.SurveyRepository.Save(model);

            return RedirectToAction("GameStep2");
        }

        public ActionResult GameStep2()
        {
            return this.View();
        }

        public ActionResult GameStep3(int game)
        {
            ViewBag.game = game;
            return this.View();
        }
    }
}
