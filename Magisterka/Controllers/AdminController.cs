using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Magisterka.Controllers
{
    using Magisterka.Database;
    using Magisterka.Models;

    public class AdminController : Controller
    {
        private Repository repository = new Repository();

        public ActionResult Index()
        {
            var codes = repository.ClientCodeRepository.GetAll();

            return View(codes);
        }

        public ActionResult Upsert(int id = 0)
        {
            if (id == 0)
            {
                return this.View(new ClientCodeModel()
                                     {
                                         StartDate = DateTime.Now.Date,
                                         EndDate = DateTime.Now.Date.AddDays(1)
                                     });
            }

            return this.View(this.repository.ClientCodeRepository.GetByID(id));
        }

        [HttpPost]
        public ActionResult Upsert(ClientCodeModel model)
        {
            if (ModelState.IsValid)
            {
                this.repository.ClientCodeRepository.UpdateCreate(model);

                return RedirectToAction("Index");
            }

            return this.View(model);
        }
    }
}
