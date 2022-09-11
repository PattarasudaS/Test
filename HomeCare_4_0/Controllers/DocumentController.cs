using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeCare_4_0.Models;
using HomeCare_4_0.ClassLib;
using HomeCare_4_0.Common;
using HomeCare_4_0.DAL;

namespace HomeCare_4_0.Controllers
{
    public class DocumentController : BaseController
    {
        private Cls_HC_TM_Lookup cls_hc_tm_lookup = new Cls_HC_TM_Lookup();
        private cb_MasterData cb_MasterData = new cb_MasterData();
        private Int64 User_ID;
        private static List<HC_SP_GET_PERMISSION_Result> PermisionList;

        public ActionResult DocumentByWorkSheet()
        {
            User_ID = UserDetail.UserID;
            //AllPJ = 1;
            DocumentViewModel Document = new DocumentViewModel();
            List<SelectListItem> projectMaster = cls_hc_tm_lookup.Get_PJ_Authen(User_ID, UserDetail.AllPJ);
            Document.objCriteriaMaser = new CriteriaMaser();
            Document.objCriteriaMaser.projectMaster = new List<SelectListItem>();
            Document.objCriteriaMaser.projectMaster = projectMaster;

            // Get Permission
            cAuthorization obj2 = new cAuthorization();
            PermisionList = obj2.getPermission(UserDetail.UserID);

            return View(Document);
        }

        [HttpPost]
        public ActionResult DocumentByWorkSheet(DocumentViewModel Data)
        {

            HomeCareDBEntities HomeCareDB = new HomeCareDBEntities();
            DocumentViewModel Document = new DocumentViewModel();

            long ProjectID = Data.DocumentSearch.ProjectID;
            long UnitID = Data.DocumentSearch.UnitID;
            String UnitAddress = Data.DocumentSearch.UnitAddress;
            var dateFrom = !String.IsNullOrEmpty(Data.DocumentSearch.WorkSheet_DateFrom) ? Data.DocumentSearch.WorkSheet_DateFrom.Split('/') : null;
            var dateTo = !String.IsNullOrEmpty(Data.DocumentSearch.WorkSheet_DateFrom) ? Data.DocumentSearch.WorkSheet_DateTo.Split('/') : null;
            DateTime? WorkSheet_DateFrom;
            if (dateFrom != null)
            {
                WorkSheet_DateFrom = DateTime.Parse($"{dateFrom[2]}-{dateFrom[1]}-{dateFrom[0]}");
            }
            else
            {
                WorkSheet_DateFrom = null;
            }
            DateTime? WorkSheet_DateTo;
            if (dateTo != null)
            {
                WorkSheet_DateTo = DateTime.Parse($"{dateTo[2]}-{dateTo[1]}-{dateTo[0]}");
            }
            else
            {
                WorkSheet_DateTo = null;
            }
            String WorkSheetIDList = Data.DocumentSearch.WorkSheetIDList;


            var data = HomeCareDB.HC_SP_Get_DocumentByWorkSheet(ProjectID, UnitID, UnitAddress, WorkSheet_DateFrom, WorkSheet_DateTo, WorkSheetIDList).ToList();
            List<DocumentByWorkSheetResultViewModel> DocumentByWorkSheetResultList = new List<DocumentByWorkSheetResultViewModel>();

            foreach (var item in data)
            {
                DocumentByWorkSheetResultViewModel DocumentByWorkSheetResult = new DocumentByWorkSheetResultViewModel();
                DocumentByWorkSheetResult.Check = false;
                DocumentByWorkSheetResult.ProjectName = item.ProjectName;
                DocumentByWorkSheetResult.UnitCode = item.UnitCode;
                DocumentByWorkSheetResult.UnitAddress = item.UnitAddress;
                DocumentByWorkSheetResult.WorkSheet_ID = item.WorkSheet_ID;
                DocumentByWorkSheetResult.WorkSheet_Date = item.WorkSheet_Date;
                DocumentByWorkSheetResult.WorkSheet_JobNoText = item.WorkSheet_JobNoText;
                DocumentByWorkSheetResult.WorkSheet_Status = item.WorkSheet_Status;
                DocumentByWorkSheetResult.Document_F2By = item.Document_F2By;
                DocumentByWorkSheetResult.Document_F2Date = item.Document_F2Date;
                DocumentByWorkSheetResult.Document_F9By = item.Document_F9By;
                DocumentByWorkSheetResult.Document_F9Date = item.Document_F9Date;
                DocumentByWorkSheetResultList.Add(DocumentByWorkSheetResult);
            }

            Document.DocumentByWorkSheetResultList = DocumentByWorkSheetResultList.OrderBy(item => item.WorkSheet_ID).ThenBy(item => item.WorkSheet_Date).ToList();

            // Get Master HC_FormType
            DropDownListModel Data_DLL_HC_FormType = new DropDownListModel();
            Data_DLL_HC_FormType.Items = cls_hc_tm_lookup.Get_Master_DDL(Cls_HC_TM_Lookup_Type.HC_FormType);
            Document.Data_DLL_HC_FormType = Data_DLL_HC_FormType;


            return View(Document);
        }

        [HttpPost]
        public JsonResult SendEmailToSupport(string WorkSheetIDList, string FormType, string VendorNameForF2)
        {
            cb_Document objDocument = new cb_Document();
            List<HC_SP_Get_DocumentByFormType_Result> list = objDocument.getlistDocumentByFormType(WorkSheetIDList);
            cEmailSupport obj = new cEmailSupport();

            obj.DocumentItem = list;
            obj.FormType = "พิมพ์ฟอร์ม HC-" + FormType;

            new cb_Email().sendEmailToSupport(obj, VendorNameForF2);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SendEmailToVendor(string WorkSheetIDList, string FormType, string VendorNameForF2, string docPathList, string vendorEmail)
        {
            string[] paths = docPathList.Split(',');
            //List<string> splitPaths = new List<string>();
            //foreach (string p in paths) { splitPaths.Add(p); }

            cb_Document objDocument = new cb_Document();
            List<HC_SP_Get_DocumentByFormType_Result> list = objDocument.getlistDocumentByFormType(WorkSheetIDList);
            cEmailSupport obj = new cEmailSupport();
            List<cEmailSupport.F2AttachedFiles> attaches = new List<cEmailSupport.F2AttachedFiles>();

            for (int i = 0; i < list.Count; i++)
            {
                attaches.Add(new cEmailSupport.F2AttachedFiles
                {
                    WorkSheetName = list[i].WorkSheetJobNo,
                    FilePath = paths[i]
                    //FilePath = splitPaths[i]
                });
            }

            obj.DocumentItem = list;
            obj.FormType = FormType;
            obj.AttachedFiles = attaches;

            new cb_Email().sendEmailF2ToVendor(obj, VendorNameForF2, vendorEmail);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExportDocumentF3()
        {
            User_ID = UserDetail.UserID;
            DocumentViewModel Document = new DocumentViewModel();
            return View(Document);
        }

        [HttpPost]
        public ActionResult ExportDocumentF3(DocumentViewModel Data)
        {

            HomeCareDBEntities HomeCareDB = new HomeCareDBEntities();
            DocumentViewModel Document = new DocumentViewModel();

            long ProjectID = Data.DocumentF3Search.ProjectID;
            long UnitID = Data.DocumentF3Search.UnitID;
            String UnitAddress = Data.DocumentF3Search.UnitAddress;
            var dateFrom = !String.IsNullOrEmpty(Data.DocumentF3Search.TransferDateFrom) ? Data.DocumentF3Search.TransferDateFrom.Split('/') : null;
            var dateTo = !String.IsNullOrEmpty(Data.DocumentF3Search.TransferDateFrom) ? Data.DocumentF3Search.TransferDateTo.Split('/') : null;

            DateTime? Transfer_DateFrom;
            if (dateFrom != null)
            {
                Transfer_DateFrom = DateTime.Parse($"{dateFrom[2]}-{dateFrom[1]}-{dateFrom[0]}");
            }
            else
            {
                Transfer_DateFrom = null;
            }

            DateTime? Transfer_DateTo;
            if (dateTo != null)
            {
                Transfer_DateTo = DateTime.Parse($"{dateTo[2]}-{dateTo[1]}-{dateTo[0]}");
            }
            else
            {
                Transfer_DateTo = null;
            }

            String UnitIdList = Data.DocumentF3Search.UnitIdList;

            var data = HomeCareDB.HC_SP_API_GET_DOCUMENT_F3(ProjectID, UnitID, UnitAddress, Transfer_DateFrom, Transfer_DateTo, UnitIdList).ToList();

            List<DocumentByUnitIdResultViewModel> DocumentByUnitIdResultList = new List<DocumentByUnitIdResultViewModel>();

            foreach (var item in data)
            {
                DocumentByUnitIdResultViewModel DocumentByUnitIdResult = new DocumentByUnitIdResultViewModel();
                DocumentByUnitIdResult.Check = false;
                DocumentByUnitIdResult.ProjectName = item.ProjectName;
                DocumentByUnitIdResult.UnitId = item.UnitId;
                DocumentByUnitIdResult.UnitCode = item.UnitCode;
                DocumentByUnitIdResult.UnitAddress = item.UnitAddress;
                DocumentByUnitIdResult.TransferDate = item.TransferDateText;
                DocumentByUnitIdResult.ProspectName = item.ProspectName;
                DocumentByUnitIdResult.PrintF3Date = item.PrintF3Date;
                DocumentByUnitIdResult.PrintF3By = item.PrintF3By;
                DocumentByUnitIdResultList.Add(DocumentByUnitIdResult);
            }

            Document.DocumentByUnitIdResultList = DocumentByUnitIdResultList.OrderBy(item => item.UnitCode).ToList();

            return View(Document);
        }

        [HttpPost]
        public JsonResult CheckPermission(string permission)
        {
            for (int i = 0; i < PermisionList.Count; i++)
            {
                if (PermisionList[i].PermissionDescription.ToString() == permission)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}