using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCare_4_0.Models
{
    public class TrackerViewModel
    {
        public TrackerSearchViewModel TrackerSearch { get; set; }
        public List<HC_SP_Get_TrackerByWorkSheet_Result> TrackerByWorkSheetResult { get; set; }
        public List<HC_SP_Get_TrackerByWorkSheet_Kpi_Result> TrackerByWorkSheetKpiResult { get; set; }
        public List<HC_SP_Get_TrackerByReceive_Result> TrackerByReceivedResult { get; set; }
        public List<HC_SP_Get_TrackerByWorkSheet_Ques_Result> TrackerByWorkSheetQuesResult { get; set; }
    }

    public class TrackerSearchViewModel
    {
        public Array ProjectID { get; set; }
        public string ProjectList { get; set; }
        public string UnitCode { get; set; }
        public string UnitType { get; set; }
        public string Tracker_DateFrom { get; set; }
        public string Tracker_DateTo { get; set; }
		public string Tracker_CloseDateFrom { get; set; }
		public string Tracker_CloseDateTo { get; set; }
        public string Tracker_JobType { get; set; }
		public int EvaluateMedia { get; set; }
		public long MainProcessID { get; set; }
        public string PaymentType { get; set; }
        public long VendorID { get; set; }
        public string QuestionStatus { get; set; }
    }
}