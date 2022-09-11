using HomeCare_4_0.DAL;
using HomeCare_4_0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeCare_4_0.Controllers
{
    public class MasterController : BaseController
    {
        // GET: Master
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OneTimeService()
        {
            return View();
        }

        public ActionResult OneTimeQuotationPaymentStatus()
        {
            return View();
        }

        public ActionResult GrantUserPermission()
        {
            return View();
        }

        public ActionResult CreateNewPermission()
        {
            return View();
        }

        public ActionResult ManageLineApprove()
        {
            return View();
        }
        public ActionResult ManageLineApproveData()
        {
            return View();
        }

        public ActionResult ManageCentralUnit()
        {
            return View();
        }

        public ActionResult ManagePaymentTypes()
        {
            return View();
        }
        public ActionResult ExtendedWarranty()
        {
            return View();
        }        
        public ActionResult ManageMasterCost()
        {
            return View();
        }

        public ActionResult ManageExtentionDay()
        {
            return View();
        }

        public ActionResult ManageManualPO()
        {
            return View();
        }
        public ActionResult ManageRollbackWorksheet()
        {
            return View();
        }

        [HttpPost]
        public JsonResult getProjectDetailByProjectCode(string projectCode)
        {
            cb_MasterData objMasterData = new cb_MasterData();
            HC_SP_Get_ProjectDetail_Result result=objMasterData.getProjectDetailByProjectCode(projectCode);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getListVendormaster(cSAPManagement obj)
        {
            List<cVendorListResult> list = cb_SAPManagement.SAP_Load_Vendor(obj.Vendor, "*" + obj.Name + "*", obj.TaxID).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getHClist()
        {
            cb_MasterData obj = new cb_MasterData(); 
            List<Dropdown> HClist1 = obj.getHCList1();
            
            cHCMaster modal = new cHCMaster();
            modal.HClist1 = HClist1; 

            return Json(modal, JsonRequestBehavior.AllowGet); 
        }
        [HttpPost]
        public JsonResult getList2(int List1ID)
        {
            cb_MasterData obj = new cb_MasterData();  
            List<Dropdown> HClist2 = obj.getHCList2(List1ID);         
            return Json(HClist2, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getList3(int List2ID)
        {
            cb_MasterData obj = new cb_MasterData();
            List<Dropdown> HClist3 = obj.getHCList3(List2ID);
            return Json(HClist3, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getList4(int List3ID)
        {
            cb_MasterData obj = new cb_MasterData();
            List<Dropdown> HClist4 = obj.getHCList4(List3ID);
            return Json(HClist4, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getList4Unit(int List4ID)
        {
            cb_MasterData obj = new cb_MasterData();
            HC_TM_List4 List4Unit = obj.getList4Unit(List4ID);
            return Json(List4Unit, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getListPaymentType(string approvedType, int warrantyExpired)
        {
            cb_MasterData obj = new cb_MasterData();
            List<HC_SP_Get_TM_Payment_Type_Result> list = obj.getPaymentType(approvedType, warrantyExpired);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getListSpec2(int spec1ID)
        {
            cb_MasterData obj = new cb_MasterData();
            List<Dropdown> list = obj.getHCSpec2(spec1ID);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getListSpec3(int spec2ID)
        {
            cb_MasterData obj = new cb_MasterData();
            List<Dropdown> list = obj.getHCSpec3(spec2ID);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getPriority(int spec3ID)
        {
            cb_MasterData obj = new cb_MasterData();
            string priority = obj.getPriority(spec3ID);
            return Json(priority, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getLocation(int spec1ID)
        {
            cb_MasterData obj = new cb_MasterData();
            List<HC_SP_Get_TM_Location_Result> list = obj.getLocation(spec1ID);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getExtendReason(int ReasonType)
        {
            cb_MasterData obj = new cb_MasterData();
            List<HC_SP_Get_ExtendReason_Result> ExtendReason = obj.getExtendReason(ReasonType);
            return Json(ExtendReason, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getUser()
        {
            cb_MasterData obj = new cb_MasterData();
            List<SelectListItem> User = obj.getUser();
            return Json(User, JsonRequestBehavior.AllowGet);
        }

    }
}