using HomeCare_4_0.ClassLib;
using HomeCare_4_0.Common;
using HomeCare_4_0.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeCare_4_0.DAL
{
    public class cb_Guarantee
    {
        private HomeCareDBEntities context;
        public cb_Guarantee()
        {
            this.context = new HomeCareDBEntities();
        }

        public List<HC_SP_Get_Search_Guarantee_Result> GetSearchGuarantee(long ProjectID)
        {
            return context.HC_SP_Get_Search_Guarantee(ProjectID).ToList();
        }

        public List<HC_SP_Get_TD_Guarantee_Result> GetGuarantee(long DOCUMENTID)
        {
            return context.HC_SP_Get_TD_Guarantee(DOCUMENTID).ToList();
        }

        public List<TK_SP_Get_Customer_Info_By_Unit_Result> GetCustomerInfoByUnit(long UnitID)
        {
            return context.TK_SP_Get_Customer_Info_By_Unit(UnitID).ToList();
        }

        public bool UpdateGuarantee(cApproveGuarantee approve)
        {
            try
            {


                context.HC_SP_Update_TD_Guarantee(approve.ApproveID, approve.Status, approve.Role, approve.Remark, approve.User);
                int sendApprove;
                if (approve.Status.Value)
                {
                    sendApprove = (int)EnumConfiguration.EmailActionType.Approved;
                }
                else
                {
                    sendApprove = (int)EnumConfiguration.EmailActionType.NotApproved;
                }

                sendNoti(approve.ApproveID, sendApprove, approve.JobNo);

                return true;
            }
            catch
            {
                return false;
            }

        }

        public void sendNoti(long? approveID,int sendApprove,string JobNo)
        {
            List<HC_SP_Get_TD_Guarantee_Result> detail = context.HC_SP_Get_TD_Guarantee(approveID).ToList();
            if (detail.Count() > 0)
            {
             
                List<string> EmailList = context.HC_SP_Get_Email_Approve(approveID, detail.FirstOrDefault().PROJECT_CODE, detail.FirstOrDefault().USER_ROLE, EnumConfiguration.TypeEmail.TO.ToString(), sendApprove).ToList();
                string strTO = string.Empty;

                if (EmailList.Count() > 0)
                {
                    foreach (string i in EmailList)
                    {
                        if(string.IsNullOrEmpty(strTO))
                        {
                            strTO = i;
                        }
                        else
                        {
                            strTO += i + ";";
                        }
                        
                    }

                }

                if (!string.IsNullOrEmpty(strTO))
                {
                    cNotificationMobileApproveResult NotificationAPI = new cNotificationMobileApproveResult();
                    NotificationAPI.process = EnumConfiguration.ApproveType.INSURANCE_EXTEND.ToString();
                    NotificationAPI.intno = JobNo;
                    NotificationAPI.doctype = "ZHC";
                    NotificationAPI.email = strTO;

                    cb_Notification Noti = new cb_Notification();
                    Noti.NotificationMobileApprove(NotificationAPI);
                } 
            }
        }


    }



    public class cApproveGuarantee
    {
        public long? ApproveID { get; set; }
        public string Remark { get; set; }
        public string Role { get; set; }
        public bool? Status { get; set; }
        public string User { get; set; }
        public string JobNo { get; set; }


    }
}