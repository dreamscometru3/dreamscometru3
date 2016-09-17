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

            if (clientCode.Status == CodeStatus.Completed)
            {
                return RedirectToAction("Expired");
            }

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
            if (!IsClientCode())
            {
                return RedirectToAction("Expired");
            }

            return this.View();
        }

        public ActionResult GameStep3(int game)
        {
            if (!IsClientCode())
            {
                return RedirectToAction("Expired");
            }

            ViewBag.game = game;
            return this.View();
        }

        public ActionResult GameStep4(string result)
        {
            if (!IsClientCode())
            {
                return RedirectToAction("Expired");
            }

            ViewBag.result = result;

            var clientCodeId = this.Load("ClientCode");
            var lotteryTicket = new LotteryTicket()
                                    {
                                        UserTickets = 0,
                                        ClientCode =
                                            new ClientCodeModel() { Id = int.Parse(clientCodeId), }
                                    };

            return this.View(lotteryTicket);
        }

        [HttpPost]
        public ActionResult GameStep4(LotteryTicket model)
        {
            if (ModelState.IsValid)
            {
                var clientCode = this.repository.ClientCodeRepository.GetByID(model.ClientCode.Id);
                model.ClientCode = clientCode;

                clientCode.Status = CodeStatus.Completed;
                this.repository.LotteryRepository.UpdateCreate(model);

                this.Save("ClientCode", string.Empty);

                return RedirectToAction("Finish");
            }

            return this.View(model);
        }

        public ActionResult Finish()
        {
            return this.View();
        }

        public ActionResult Expired()
        {
            return this.View();
        }
    }
}
