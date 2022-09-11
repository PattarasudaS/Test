using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeCare_4_0.ClassLib;
using HomeCare_4_0.Models;
using HomeCare_4_0.DAL;
using HomeCare_4_0.Common;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace HomeCare_4_0.Controllers
{
    public class HomeController : BaseController
    {
        private Int64 User_ID;
        //private Int64 AllPJ;
        private Cls_HC_TM_Lookup cls_hc_tm_lookup = new Cls_HC_TM_Lookup();
        private HomeViewModel homeModel = new HomeViewModel();

        public ActionResult Index2()
        {
            User_ID = UserDetail.UserID;
            //AllPJ = 1;
            ViewBag.Show_Menu = Cls_HC_Constance.HC_Show_Main_Menu;
            ViewBag.Show_Sub_Menu = Cls_HC_Constance.HC_Show_Sub_Menu;
            ViewBag.Show_Sub_Menu_HomeCare = Cls_HC_Constance.HC_Show_Sub_Menu_HomeCare;
            ViewBag.Show_Sub_Menu_Callcenter = Cls_HC_Constance.HC_Hidden_Sub_Menu_HomeCare;

            HomeCareDBEntities HomeCareDB = new HomeCareDBEntities();

            //Receive 1 
            long VendorID = 0;
            if (UserDetail.Role == "VENDOR")
            {
                VendorID = long.Parse(!string.IsNullOrEmpty(UserDetail.VendorCode) ? UserDetail.VendorCode : "0");
            }
            List<HC_SP_Get_RW_Search_Noti_Result> list = HomeCareDB.HC_SP_Get_RW_Search_Noti(User_ID, VendorID).ToList();

            //Work sheet 11
            if (list != null)
            {
                homeModel.HC_Main_Process_1 = list.Where(x => x.Receive_Status_ID == 1).Count();
                homeModel.HC_Main_Process_2 = list.Where(x => x.Receive_Status_ID == 2).Count();
                homeModel.HC_Main_Process_3 = list.Where(x => x.ApproveStatusID == 11).Count();
                homeModel.HC_Main_Process_4 = 5;
                homeModel.HC_Main_Process_5 = 5;
                homeModel.HC_Main_Process_6 = 5;
                homeModel.HC_Main_Process_7 = 5;
                homeModel.HC_Main_Process_8 = 5;
                homeModel.HC_Main_Process_9 = 5;
                homeModel.HC_Main_Process_10 = 5;
                homeModel.HC_Main_Process_11 = 5;
                homeModel.HC_Main_Process_12 = 5;
            }

            return View(homeModel);
        }

        public ActionResult Index()
        {
            User_ID = UserDetail.UserID;
            //AllPJ = 1;
            ViewBag.Show_Menu = Cls_HC_Constance.HC_Show_Main_Menu;
            ViewBag.Show_Sub_Menu = Cls_HC_Constance.HC_Show_Sub_Menu;
            ViewBag.Show_Sub_Menu_HomeCare = Cls_HC_Constance.HC_Show_Sub_Menu_HomeCare;
            ViewBag.Show_Sub_Menu_Callcenter = Cls_HC_Constance.HC_Hidden_Sub_Menu_HomeCare;

            HomeCareDBEntities HomeCareDB = new HomeCareDBEntities();

            //Receive 1 
            long VendorID = 0;
            if (UserDetail.Role == "VENDOR")
            {
                VendorID = long.Parse(!string.IsNullOrEmpty(UserDetail.VendorCode) ? UserDetail.VendorCode : "0");
            }

            ViewBag.VendorId = VendorID;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult _Menu()
        {
            return PartialView();
        }

        public ActionResult SearchInformationByStatus(int MainProcessID)
        {

            ViewBag.Title = "Search Infomation";
            homeModel.HC_Main_Process = MainProcessID;
            return View(homeModel);

        }
        public ActionResult SearchInformation(int ProjectID, int UnitID)
        {
            ViewBag.Title = "Search Infomation";
            ViewBag.message = "Inform";
            User_ID = UserDetail.UserID;
            //AllPJ = 1;
            List<SelectListItem> projectMaster = cls_hc_tm_lookup.Get_PJ_Authen(User_ID, UserDetail.AllPJ);
            homeModel.objCriteriaMaser = new CriteriaMaser();
            homeModel.ProjectID = ProjectID;
            homeModel.UnitID = UnitID;
            HomeCareDBEntities HomeCareDB = new HomeCareDBEntities();

            // Customer Data
            var data = HomeCareDB.TK_SP_Get_Customer_Info_By_Unit(UnitID).ToList();
            CustomerViewModel Data_Customer = new CustomerViewModel();
            UnitViewModel Data_Unit = new UnitViewModel();

            if (data != null && data.Count > 0)
            {
                Data_Customer.FullName = data.FirstOrDefault().FullNameTH_1;
                Data_Customer.VipStatus = HomeCareDB.TK_SP_Get_CheckProspectVipStatus(data.FirstOrDefault().ID_1).FirstOrDefault();
                // Unit Data
                Data_Unit.PJ_Name = data.FirstOrDefault().Project_NameTH;
                Data_Unit.Unit_Code = data.FirstOrDefault().Unit_Code;
                Data_Unit.Unit_Address = data.FirstOrDefault().Unit_Address;
                Data_Unit.Unit_Code_Address = data.FirstOrDefault().Unit_Code_Address;
                Data_Unit.Unit_GuaranteedYield = data.FirstOrDefault().Unit_GuaranteedYield;
                Data_Unit.Unit_Defer = data.FirstOrDefault().Unit_Defer;
                Data_Unit.StartGuaranteeDate = data.FirstOrDefault().StartGuaranteeDate;
                Data_Unit.EndGuaranteeDate = data.FirstOrDefault().EndGuaranteeDate;
                Data_Unit.ExtraGuaranteeDate = data.FirstOrDefault().ExtraGuaranteeDate;
            }

            homeModel.Data_Customer = Data_Customer;
            homeModel.Data_Unit = Data_Unit;

            return View(homeModel);
        }

        [HttpPost]
        public JsonResult GetSearchInformation(long ProjectID, long unitID)
        {
            cb_SearchManagement objSearchManagement = new cb_SearchManagement();
            var list = objSearchManagement.GetSearchInformation(ProjectID, unitID);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetSearchInformationByStatus(int MainProcessID)
        {
            cb_SearchManagement objSearchManagement = new cb_SearchManagement();
            var list = objSearchManagement.GetSearchInformationByStatus(MainProcessID);
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetProejctMaster()
        {
            User_ID = UserDetail.UserID;
            //AllPJ = 1;
            List<SelectListItem> projectMaster = cls_hc_tm_lookup.Get_PJ_Authen(User_ID, UserDetail.AllPJ);
            return Json(projectMaster, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetProejctMasterByCode()
        {
            User_ID = UserDetail.UserID;
            //AllPJ = 1;
            List<SelectListItem> projectMaster = cls_hc_tm_lookup.Get_PJ_Authen_ByCode(User_ID, UserDetail.AllPJ);
            return Json(projectMaster, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetUnitData(RequestModel model)
        {
            if (model != null)
            {
                AJAX_UnitResultViewModel data = new AJAX_UnitResultViewModel();
                List<AJAX_UnitViewModel> lUnit = new List<AJAX_UnitViewModel>();
                List<SelectListItem> list_Of_Unit = new List<SelectListItem>();
                list_Of_Unit = cls_hc_tm_lookup.Get_Unit_By_PJ_ID(Convert.ToInt64(model.ID));

                foreach (var list in list_Of_Unit)
                {
                    AJAX_UnitViewModel obj = new AJAX_UnitViewModel();
                    obj.Unit_ID = Convert.ToInt64(list.Value);
                    obj.Unit_Code = list.Text;
                    lUnit.Add(obj);
                }
                data.data = lUnit;
                return Json(data);
            }
            else
            {
                return Json("An Error Has occoured");
            }

        }

        [HttpPost]
        public JsonResult GetCalendarList(int ProjectID, int UserID)
        {
            cb_Calendar objCalendar = new cb_Calendar();
            List<CalendarEvent> calendarEventList = new List<CalendarEvent>();
            List<HC_SP_Get_Canlendar_Result> list = objCalendar.GetCandarList(ProjectID, UserID);
            foreach (var item in list)
            {
                CalendarEvent calendarEvent = new CalendarEvent();
                calendarEvent.calendarID = item.CalendarID;
                calendarEvent.title = item.CalendarMessage;
                calendarEvent.start = item.StartDate;
                calendarEvent.end = item.EndDate;
                calendarEvent.ReferenceType = item.Type;
                calendarEvent.ReferenceID = item.ReferenceID;
                calendarEvent.FlagAction = item.FlagAction == null ? false : item.FlagAction;
                calendarEvent.isAfterWarranty = item.IsAfterWarranty;
                calendarEvent.CreatedBy = item.CreatedBy;
                calendarEventList.Add(calendarEvent);
            }
            calendarEventList.AddRange(GetLeaveEmployee(ProjectID, UserID));

            return Json(calendarEventList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertCalendar(CalendarEvent requestCalendarEvent)
        {
            cb_Calendar objCalendar = new cb_Calendar();
            int? calendarID;

            calendarID = objCalendar.InsertCalendar(requestCalendarEvent);

            return Json(calendarID, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateCalendar(CalendarEvent requestCalendarEvent)
        {
            cb_Calendar objCalendar = new cb_Calendar();


            objCalendar.UpdateCalendar(requestCalendarEvent);

            return Json(null, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteCalendar(CalendarEvent requestCalendarEvent)
        {
            cb_Calendar objCalendar = new cb_Calendar();
            objCalendar.DeleteCalendar(requestCalendarEvent);
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public List<CalendarEvent> GetLeaveEmployee(long ProjectID, int UserID)
        {
            WebServiceGetLeave.webServiceGetLeave ws = new WebServiceGetLeave.webServiceGetLeave();
            cb_Calendar objCalendar = new cb_Calendar();
            string[] EmailHCList = objCalendar.GetHCEmail(ProjectID, UserID);
            List<string> LeaveEmployeeList = new List<string>();
            List<CalendarEvent> calendarEventList = new List<CalendarEvent>();
            for (int i = -6; i <= 6; i++)
            {
                try
                {
                    string Detail = ws.getWebserviceLeave(DateTime.Now.AddMonths(i).Year.ToString(), DateTime.Now.AddMonths(i).Month.ToString(), EmailHCList);
                    LeaveEmployeeList.Add(Detail);
                    //DeSerializer 
                    //JavaScriptSerializer json_serializer = new JavaScriptSerializer();

                    //Example routes_list = (Example)json_serializer.DeserializeObject(Detail);
                    Root list = JsonConvert.DeserializeObject<Root>(Detail);

                    foreach (RESULTLIST RESULT_LIST in list.RESULT_LIST)
                    {
                        foreach (var LEAVE_LIST in RESULT_LIST.LEAVE_LIST)
                        {
                            CalendarEvent calendarEvent = new CalendarEvent();
                            calendarEvent.calendarID = 0;
                            calendarEvent.title = LEAVE_LIST.LTYPE + ": " + RESULT_LIST.LEMAIL;
                            calendarEvent.start = DateTime.ParseExact(LEAVE_LIST.LDATE, "dd/MM/yyyy", null).ToShortDateString() + " " + LEAVE_LIST.LSTART_TIME;
                            calendarEvent.end = DateTime.ParseExact(LEAVE_LIST.LENDDATE, "dd/MM/yyyy", null).ToShortDateString() + " " + LEAVE_LIST.LEND_TIME;
                            calendarEvent.CreatedBy = 0;
                            calendarEventList.Add(calendarEvent);
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return calendarEventList;
        }
        public List<CalendarEvent> GetCalendarFormOutlook(long projectID)
        {
            JavaScriptSerializer JS = new JavaScriptSerializer();
            com_Response_API RES_API = new com_Response_API();
            JsonResult CalendarFormOutlookResult = new JsonResult();
            List<CalendarEvent> reponse = new List<CalendarEvent>();

            string sw_write = string.Empty;
            string url = HomeCare_4_0.Properties.Settings.Default.Hostapi.ToString() + HomeCare_4_0.Properties.Settings.Default.CareSyncOutlookAPI.ToString();
            var request = (HttpWebRequest)WebRequest.Create(url);



            request.Method = "POST";
            request.ContentType = "application/json";

            // set parameter
            using (StreamWriter sw = new StreamWriter(request.GetRequestStream()))
            {
                var json = new JavaScriptSerializer().Serialize(
                    new
                    {
                        ProjectID = projectID,
                        CalendarStartDate = DateTime.Now.AddMonths(-6).ToShortDateString(),
                        CalendarEndDate = DateTime.Now.AddMonths(6).ToShortDateString()
                    });
                sw_write = json.ToString();
                sw.Write(sw_write);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                var result = sr.ReadToEnd();
                //result = JS.DeserializeObject(result).ToString();
                var json_All = JObject.Parse(result);
                var json_obj_Data = json_All.SelectToken("response");
                RES_API = JS.Deserialize<com_Response_API>(json_All.ToString());

                if (RES_API.ResponseCode == 1)
                {
                    //success
                    List<HCEmail> HCEmailList = JS.Deserialize<List<HCEmail>>(json_obj_Data.ToString());

                }
                else
                {
                    reponse = null;
                }
            }

            return reponse;
        }

    }

    public class LEAVELIST
    {
        public string LDATE { get; set; }
        public string LENDDATE { get; set; }
        public string LSTART_TIME { get; set; }
        public string LEND_TIME { get; set; }
        public string LTYPE { get; set; }
        public string LSTATUS { get; set; }
    }

    public class RESULTLIST
    {
        public string LEMAIL { get; set; }
        public List<LEAVELIST> LEAVE_LIST { get; set; }
    }

    public class Root
    {
        public List<RESULTLIST> RESULT_LIST { get; set; }
    }
    public class com_Response_API
    {
        public int ResponseCode { get; set; }


        public string ResponseMsg { get; set; }

        /// <summary>
        /// ex: 31/12/2017 23:08:53
        /// </summary>
        public string ResponseTime { get; set; }

        /// <summary>
        /// ex: 31/12/2017 23:08:53
        /// </summary>
        public string RequestTime { get; set; }
    }
    public class HCEmail
    {
        public string Subject { get; set; }
        public string MeetingStartDate { get; set; }
        public string MeetingEndDate { get; set; }
        public string MeetingStartTime { get; set; }
        public string MeetingEndTIme { get; set; }
        public string Email { get; set; }


    }
    public class CalendarEvent
    {
        private bool? _FlagAction;
        // Old Color = #f56954
        public CalendarEvent()
        {
            this.backgroundColor = "Navy";
            this.borderColor = "Navy";
        }
        public string backgroundColor { get; set; }
        public string borderColor { get; set; }
        public int calendarID { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string title { get; set; }
        public int? projectID { get; set; }
        public bool? isAfterWarranty { get; set; }
        public int? CreatedBy { get; set; }
        public long? ReferenceID { get; internal set; }
        public int? ReferenceType { get; internal set; }
        public bool? FlagAction
        {
            get
            {
                return this._FlagAction;
            }
            set
            {
                if (value == true)
                {
                    this.backgroundColor = "Green";
                    this.borderColor = "Green";
                }
                else
                {
                    this.backgroundColor = "Crimson";
                    this.borderColor = "Crimson";
                }
                this._FlagAction = value;
            }
        }
    }
}