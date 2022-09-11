using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
/// <summary>
/// Controller สำหรับ Options ต่าง ๆ
/// </summary>
namespace HomeCare_4_0.Controllers
{
    public class OptionsController : BaseController
    {
        // GET: Options
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Calendar()
        {
            return View();
        }

        public ActionResult Knowlage()
        {
            return View();
        }
    }
}