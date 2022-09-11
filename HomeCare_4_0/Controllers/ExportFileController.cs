using HomeCare_4_0.DAL;
using HomeCare_4_0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeCare_4_0.Controllers
{
    public class ExportFileController : Controller
    {
        // GET: ExportFile
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ExportDocumentF1(string WorkSheetIDList)
        {
            cb_Export objExport = new cb_Export();

            List<HC_SP_Get_DocumentByFormType_Result> listDocType = objExport.getDocumentByFormType(WorkSheetIDList);

            List<string> list = new cb_Export().ExportF1Document(listDocType);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ExportDocumentF2(string WorkSheetIDList, string VendorName, string VendorAddress, long VendorID = 0)
        {
            cb_Export objExport = new cb_Export();

            List<HC_SP_Get_DocumentByFormType_Result> listDocType = objExport.getDocumentByFormType(WorkSheetIDList);

            List<string> list = new cb_Export().ExportF2Document(listDocType, VendorName, VendorAddress, VendorID);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ExportDocumentF5(string WorkSheetIDList)
        {
            cb_Export objExport = new cb_Export();

            List<HC_SP_Get_DocumentByFormType_Result> listDocType = objExport.getDocumentByFormType(WorkSheetIDList);

            List<string> list = new cb_Export().ExportF5Document(listDocType);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ExportDocumentF9(string WorkSheetIDList)
        {
            cb_Export objExport = new cb_Export();

            List<HC_SP_Get_DocumentByFormType_Result> listDocType = objExport.getDocumentByFormType(WorkSheetIDList);

            List<string> list = new cb_Export().ExportF9Document(listDocType);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ExportCombineDocument(string WorkSheetIDList, int doc1, int doc2, int databaseSave1 = 1, int databaseSave2 = 0, string VendorName = null, string VendorAddress = null, long VendorID = 0)
        {
            cb_Export objExport = new cb_Export();

            List<HC_SP_Get_DocumentByFormType_Result> listDocType = objExport.getDocumentByFormType(WorkSheetIDList);

            List<string> list = new cb_Export().ExportCombineDocument(listDocType, doc1, doc2, databaseSave1, databaseSave2, VendorName, VendorAddress, VendorID);

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ExportDocumentQuotation(long WorkSheetID)
        {
            cb_Export objExport = new cb_Export();
            List<string> list = objExport.ExportQuotationDocument(WorkSheetID);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ExportWorkSheetReport(TrackerSearchViewModel m)
        {
            cb_Export objExport = new cb_Export();
            List<string> list = objExport.ExportWorkSheet(m);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ExportWorkSheetSLAReport(TrackerSearchViewModel m)
        {
            cb_Export objExport = new cb_Export();
            List<string> list = objExport.ExportWorkSheetSLA(m);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ExportWorkSheetByReceived(TrackerSearchViewModel m)
        {
            cb_Export objExport = new cb_Export();
            List<string> list = objExport.ExportWorkSheetByReceived(m);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ExportWorkSheetQuestionnaireReport(TrackerSearchViewModel m)
        {
            cb_Export objExport = new cb_Export();
            List<string> list = objExport.ExportWorkSheetQuestionnaire(m);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ExportCombineDocumentF3(string UnitIdList, int doc1, int databaseSave1 = 1)
        {
            cb_Export objExport = new cb_Export();

            List<HC_SP_API_GET_DOCUMENT_F3_Result> listDocType = objExport.getDataExportDocumentF3(UnitIdList);

            List<string> list = new cb_Export().ExportCombineDocumentF3(listDocType, doc1, databaseSave1);

            return Json(list, JsonRequestBehavior.AllowGet);
        }
      
    }
}