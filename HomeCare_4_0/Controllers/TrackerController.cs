using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeCare_4_0.ClassLib;
using HomeCare_4_0.Models;
using System.Data.SqlClient;
using HomeCare_4_0.Common;
using HomeCare_4_0.DAL;

/// <summary>
/// Controller สำหรับหน้าวิเคราะห์ ต่าง ๆ
/// </summary>
namespace HomeCare_4_0.Controllers
{
    public class TrackerController : BaseController
    {
        public ActionResult TrackerByWorkSheet()
        {
            TrackerViewModel Tracker = new TrackerViewModel();
            TrackerSearchViewModel Search = new TrackerSearchViewModel();
            Tracker.TrackerSearch = Search;
            return View(Tracker);
        }

        public ActionResult TrackerByWorkSheetOnetime()
        {
            TrackerViewModel Tracker = new TrackerViewModel();
            TrackerSearchViewModel Search = new TrackerSearchViewModel();
            Tracker.TrackerSearch = Search;
            return View(Tracker);
        }

        [HttpPost]
        public ActionResult TrackerByWorkSheet(TrackerSearchViewModel Data)
        {
            //long VendorID = 0;
            if (UserDetail.Role == "VENDOR")
            {
                Data.VendorID = long.Parse(!string.IsNullOrEmpty(UserDetail.VendorCode) ? UserDetail.VendorCode : "0");
            }

            foreach (var m in Data.ProjectID)
            {
                Data.ProjectList += m + ",";
            }

            cb_Tracker obj = new cb_Tracker();

            TrackerViewModel Tracker = new TrackerViewModel();
            List<HC_SP_Get_TrackerByWorkSheet_Result> result = obj.getlistTrackerByWorkSheet(Data);
            Tracker.TrackerByWorkSheetResult = result;

            TrackerSearchViewModel Search = new TrackerSearchViewModel();
            Search.ProjectList = Data.ProjectList;
            Search.MainProcessID = Data.MainProcessID;
            Search.PaymentType = Data.PaymentType;
            Search.Tracker_DateFrom = Data.Tracker_DateFrom;
            Search.Tracker_DateTo = Data.Tracker_DateTo;
            Search.Tracker_JobType = Data.Tracker_JobType;
            Tracker.TrackerSearch = Search;
            return View(Tracker);
        }

        public ActionResult TrackerByWorkSheetKpi()
        {
            TrackerViewModel Tracker = new TrackerViewModel();
            return View(Tracker);
        }

        [HttpPost]
        public ActionResult TrackerByWorkSheetKpi(TrackerSearchViewModel Data)
        {
            if (UserDetail.Role == "VENDOR")
            {
                Data.VendorID = long.Parse(!string.IsNullOrEmpty(UserDetail.VendorCode) ? UserDetail.VendorCode : "0");
            }
            foreach (var m in Data.ProjectID)
            {
                Data.ProjectList += m + ",";
            }
            TrackerViewModel Tracker = new TrackerViewModel();
            cb_Tracker context = new cb_Tracker();
            List<HC_SP_Get_TrackerByWorkSheet_Kpi_Result> result = context.getListTrackerByWorkSheetKpi(Data);
            Tracker.TrackerByWorkSheetKpiResult = result;

            TrackerSearchViewModel Search = new TrackerSearchViewModel();
            Search.ProjectList = Data.ProjectList;
            Search.MainProcessID = Data.MainProcessID;
            Search.PaymentType = Data.PaymentType;
            Search.Tracker_DateFrom = Data.Tracker_DateFrom;
            Search.Tracker_DateTo = Data.Tracker_DateTo;
            Search.Tracker_JobType = Data.Tracker_JobType;
            Tracker.TrackerSearch = Search;

            return View(Tracker);
        }

        public ActionResult TrackerByReceived()
        {
            TrackerViewModel Tracker = new TrackerViewModel();
            return View(Tracker);
        }

        [HttpPost]
        public ActionResult TrackerByReceived(TrackerSearchViewModel Data)
        {
            foreach (var m in Data.ProjectID)
            {
                Data.ProjectList += m + ",";
            }
            TrackerViewModel Tracker = new TrackerViewModel();
            cb_Tracker obj = new cb_Tracker();
            List<HC_SP_Get_TrackerByReceive_Result> result = obj.getlistTrackerByReceive(Data);
            Tracker.TrackerByReceivedResult = result;

            TrackerSearchViewModel Search = new TrackerSearchViewModel();
            Search.ProjectList = Data.ProjectList;
            Search.MainProcessID = Data.MainProcessID;
            Search.PaymentType = Data.PaymentType;
            Search.Tracker_DateFrom = Data.Tracker_DateFrom;
            Search.Tracker_DateTo = Data.Tracker_DateTo;
            Search.Tracker_JobType = Data.Tracker_JobType;
            Tracker.TrackerSearch = Search;

            return View(Tracker);
        }

        public ActionResult TrackerByWorkSheetWithQuestionnaire()
        {
            TrackerViewModel Tracker = new TrackerViewModel();
            return View(Tracker);
        }

        [HttpPost]
        public ActionResult TrackerByWorkSheetWithQuestionnaire(TrackerSearchViewModel Data)
        {
            //Handle Null
            if (Data.ProjectID == null)
            {
                Data.ProjectID = new string[] { "0" };
            }

            if (UserDetail.Role == "VENDOR")
            {
                Data.VendorID = long.Parse(!string.IsNullOrEmpty(UserDetail.VendorCode) ? UserDetail.VendorCode : "0");
            }
            foreach (var m in Data.ProjectID)
            {
                Data.ProjectList += m + ",";
            }
            TrackerViewModel Tracker = new TrackerViewModel();
            cb_Tracker context = new cb_Tracker();
            List<HC_SP_Get_TrackerByWorkSheet_Ques_Result> result = context.getListTrackerByWorkSheetQues(Data);
            Tracker.TrackerByWorkSheetQuesResult = result;

            TrackerSearchViewModel Search = new TrackerSearchViewModel();
            Search.ProjectList = Data.ProjectList;
            Search.UnitCode = Data.UnitCode;
            Search.Tracker_DateFrom = Data.Tracker_DateFrom;
            Search.Tracker_DateTo = Data.Tracker_DateTo;
			Search.Tracker_CloseDateFrom = Data.Tracker_CloseDateFrom;
            Search.Tracker_CloseDateTo = Data.Tracker_CloseDateTo;
            Search.Tracker_JobType = Data.Tracker_JobType;
            Search.EvaluateMedia = Data.EvaluateMedia;
            Tracker.TrackerSearch = Search;

            return View(Tracker);
        }
        public ActionResult TrackerPOByWorkSheet()
        {
           
            return View();
        }
    }
}