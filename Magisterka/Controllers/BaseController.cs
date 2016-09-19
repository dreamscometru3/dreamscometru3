using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Magisterka.Controllers
{
    public class BaseController : Controller
    {
        public void Save(string cookieName, string value)
        {
            Response.Cookies.Add(new HttpCookie(cookieName, Server.UrlEncode(value)));
        }

        public string Load(string cookieName)
        {
            var httpCookie = Request.Cookies[cookieName];

            return httpCookie != null ? Server.UrlDecode(httpCookie.Value) : "";
        }

        public bool IsClientCode()
        {
            return !string.IsNullOrEmpty(this.Load("ClientCode"));
        }
    }
}
