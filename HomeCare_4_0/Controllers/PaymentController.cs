using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
/// <summary>
/// Controller สำหรับ งานจ่าย
/// </summary>
namespace HomeCare_4_0.Controllers
{
    public class PaymentController : BaseController
    {
        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PaymentLogin()
        {
            return View();
        }
    }
}