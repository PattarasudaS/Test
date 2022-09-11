using HomeCare_4_0.DAL;
using HomeCare_4_0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeCare_4_0.Controllers
{
    public class EditDataController : BaseController
    {
        // GET: Master
        public ActionResult Index()
        {
            return View();
        }

        

        //[HttpPost]
        //public JsonResult getProjectDetailByProjectCode(string projectCode)
        //{
        //    cb_MasterData objMasterData = new cb_MasterData();
        //    HC_SP_Get_ProjectDetail_Result result=objMasterData.getProjectDetailByProjectCode(projectCode);

        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

       

    }
}