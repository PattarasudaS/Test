using HomeCare_4_0.Common;
using HomeCare_4_0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCare_4_0.DAL
{
    public class cb_Tracker
    {
        private HomeCareDBEntities context;
        public cb_Tracker()
        {
            this.context = new HomeCareDBEntities();
        }


        public List<HC_SP_Get_TrackerByWorkSheet_Result> getlistTrackerByWorkSheet(TrackerSearchViewModel Tracker)
        {
            string SqlCmd = $@"
                EXEC HC_SP_Get_TrackerByWorkSheet
                    '{Tracker.ProjectList}',
                    '{Tracker.MainProcessID}',
                    '{(Tracker.PaymentType == "1" ? "Y" : Tracker.PaymentType == "2" ? "N" : "0")}',
                    '{Tracker.Tracker_DateFrom}',
                    '{Tracker.Tracker_DateTo}' ,
                    '{Tracker.Tracker_JobType}'
            ";
            List<HC_SP_Get_TrackerByWorkSheet_Result> result = context.Database.SqlQuery<HC_SP_Get_TrackerByWorkSheet_Result>(SqlCmd).ToList();
            return result;
            //return null;
        }

        public List<HC_SP_Get_TrackerByWorkSheet_Kpi_Result> getListTrackerByWorkSheetKpi(TrackerSearchViewModel Tracker)
        {
            return context.HC_SP_Get_TrackerByWorkSheet_Kpi(Tracker.ProjectList, Tracker.UnitType, Tracker.MainProcessID, Tracker.PaymentType, Tracker.VendorID, string.IsNullOrEmpty(Tracker.Tracker_DateFrom) ? null : cCommon.ddMMyyyyToyyyyMMdd(Tracker.Tracker_DateFrom, '/'), string.IsNullOrEmpty(Tracker.Tracker_DateTo) ? null : cCommon.ddMMyyyyToyyyyMMdd(Tracker.Tracker_DateTo, '/'), Tracker.Tracker_JobType).ToList();
        }

        public List<HC_SP_Get_TrackerByReceive_Result> getlistTrackerByReceive(TrackerSearchViewModel Tracker)
        {
            return context.HC_SP_Get_TrackerByReceive(Tracker.ProjectList, Tracker.UnitType, Tracker.MainProcessID, string.IsNullOrEmpty(Tracker.Tracker_DateFrom) ? null : cCommon.ddMMyyyyToyyyyMMdd(Tracker.Tracker_DateFrom, '/'), string.IsNullOrEmpty(Tracker.Tracker_DateTo) ? null : cCommon.ddMMyyyyToyyyyMMdd(Tracker.Tracker_DateTo, '/'), Tracker.Tracker_JobType).ToList();
            //return null;
        }

        public List<HC_SP_Get_TrackerByWorkSheet_Ques_Result> getListTrackerByWorkSheetQues(TrackerSearchViewModel Tracker)
        {
            using (var context = new HomeCareDBEntities())
            {
                return context.HC_SP_Get_TrackerByWorkSheet_Ques(Tracker.ProjectList, Tracker.UnitCode, Tracker.QuestionStatus, string.IsNullOrEmpty(Tracker.Tracker_DateFrom) ? null : cCommon.ddMMyyyyToyyyyMMdd(Tracker.Tracker_DateFrom, '/'), string.IsNullOrEmpty(Tracker.Tracker_DateTo) ? null : cCommon.ddMMyyyyToyyyyMMdd(Tracker.Tracker_DateTo, '/'), string.IsNullOrEmpty(Tracker.Tracker_CloseDateFrom) ? null : cCommon.ddMMyyyyToyyyyMMdd(Tracker.Tracker_CloseDateFrom, '/'), string.IsNullOrEmpty(Tracker.Tracker_CloseDateTo) ? null : cCommon.ddMMyyyyToyyyyMMdd(Tracker.Tracker_CloseDateTo, '/'), Tracker.EvaluateMedia, Tracker.Tracker_JobType).ToList(); 
            }
        }

    }
}