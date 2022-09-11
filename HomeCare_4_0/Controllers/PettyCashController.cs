using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeCare_4_0.Controllers
{
    public class PettyCashController : Controller
    {
        // GET: PettyCash
        public ActionResult Index()
        {
            HttpCookie hostcookie = new HttpCookie("Hostapi");
            hostcookie.Value = HomeCare_4_0.Properties.Settings.Default.Hostapi;
            hostcookie.Expires= DateTime.Now.AddMinutes(20);
            Response.AppendCookie(hostcookie);
            return View();
        }
        public ActionResult blank()
        {
            return View();
        }
        public ActionResult List()
        {

            return View();
        }

    }
}