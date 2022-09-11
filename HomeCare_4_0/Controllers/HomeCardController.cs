using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeCare_4_0.Controllers
{
    public class HomeCardController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Member()
        {
            return View();
        }
    }
}