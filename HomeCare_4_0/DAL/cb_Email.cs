using HomeCare_4_0.ClassLib;
using HomeCare_4_0.Common;
using HomeCare_4_0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mail;

namespace HomeCare_4_0.DAL
{

    public class cb_Email
    {
        private HomeCareDBEntities context;
        public cb_Email()
        {
            this.context = new HomeCareDBEntities();
        }
        private List<string> getlistemail(cEmailContent emailInform)
        {
            List<string> listEmail = context.HC_SP_Get_Email(emailInform.Type, emailInform.ProjectCode, emailInform.VendorID, emailInform.FlagChina).ToList();
            return listEmail;
        }
        private List<string> getlistemailApprove(cEmailResponsible objResponsible)
        {
            List<string> listEmail = context.HC_SP_Get_Email_Approve(objResponsible.TD_Approve_ID, objResponsible.ProjectCode, objResponsible.Role, objResponsible.TypeEmail, objResponsible.TypeBotton).ToList();
            return listEmail;
        }
        private List<string> getlistemail(cEmailSupport emailSupport)
        {
            List<string> listEmail = context.HC_SP_Get_Email(emailSupport.TypeEmail, emailSupport.ProjectCode, 0, false).ToList();
            return listEmail;
        }
        private List<string> getlistemail(string TypeEmail, string ProjectCode)
        {
            List<string> listEmail = context.HC_SP_Get_Email(TypeEmail, ProjectCode, 0, false).ToList();
            return listEmail;
        }


        public string SendEmail(long UnitID, long ReceivedID, int EmailContentType)
        {

            cb_Inform objInfrom = new cb_Inform();
            TK_SP_Get_Customer_Info_By_Unit_Result unitInfo = objInfrom.getcustomerinfobuUnit(UnitID);
            HC_SP_Get_TD_Receive_Result receiveDetial = new cb_Worksheet().getreceiveDetial(ReceivedID);
            List<HC_SP_Get_TD_Receive_Item_Result> receiveItem = objInfrom.getreceiveItem(ReceivedID);
            TK_SP_Get_Address_Result customerContract = objInfrom.getcustomerContract(unitInfo.ID_1);


            //Start Subject
            string strSubject = string.Empty;
            cEmailContent EmailContent = new cEmailContent();

            if (EmailContentType == (int)EnumConfiguration.EmailContentType.Inform)
            {
                strSubject = System.Configuration.ConfigurationManager.AppSettings["SystemName"].ToString() + "[แจ้งซ่อมจาก Call Center] โครงการ : " + unitInfo.Project_NameTH.Trim() + "  ยูนิต : " + unitInfo.Unit_Code.Trim() + "  เลขที่บ้าน : " + unitInfo.Unit_Address.Trim();
                EmailContent.Type = EnumConfiguration.EmailContentType.Inform.ToString();
            }
            else if (EmailContentType == (int)EnumConfiguration.EmailContentType.RecievedHC)
            {
                string strReceiveStatus = string.Empty;
                if (receiveDetial.Receive_Status_ID == 2) { strReceiveStatus = "แจ้ง Home Care ทำการรับเรื่องแจ้งซ่อม"; }
                else if (receiveDetial.Receive_Status_ID == 3 || receiveDetial.Receive_Status_ID == 4) {
                    return string.Empty;
                }
                else if (receiveDetial.Receive_Status_ID == 5) { strReceiveStatus = "แจ้ง Home Care ไม่สามารถทำการรับเรื่องแจ้งซ่อมได้"; }
                else if (receiveDetial.Receive_Status_ID == 6) { strReceiveStatus = "แจ้ง Call Centre ติดต่อลูกค้าได้"; }
                else if (receiveDetial.Receive_Status_ID == 7) { strReceiveStatus = "แจ้ง Call Centre ติดต่อลูกค้าไม่ได้ ยกเลิกใบรับเรื่อง"; }
                strSubject = System.Configuration.ConfigurationManager.AppSettings["SystemName"].ToString() + "["+ strReceiveStatus + "] โครงการ : " + unitInfo.Project_NameTH.Trim() + "  ยูนิต : " + unitInfo.Unit_Code.Trim() + "  เลขที่บ้าน : " + unitInfo.Unit_Address.Trim();
                EmailContent.Type = EnumConfiguration.EmailContentType.RecievedHC.ToString();
            }
            else if (EmailContentType == (int)EnumConfiguration.EmailContentType.ConfirmRecieve)
            {
                strSubject = System.Configuration.ConfigurationManager.AppSettings["SystemName"].ToString() + "[Call Centre ไม่สามารถยืนยันการรับเรื่องของ HC ได้ ] โครงการ : " + unitInfo.Project_NameTH.Trim() + "  ยูนิต : " + unitInfo.Unit_Code.Trim() + "  เลขที่บ้าน : " + unitInfo.Unit_Address.Trim();
                EmailContent.Type = EnumConfiguration.EmailContentType.ConfirmRecieve.ToString();
            }
            else if (EmailContentType == (int)EnumConfiguration.EmailContentType.ConfirmAppointment)
            {
                strSubject = System.Configuration.ConfigurationManager.AppSettings["SystemName"].ToString() + "[แจ้ง Home Care ขอเลื่อนวันที่นัดหมายตรวจสอบ] โครงการ : " + unitInfo.Project_NameTH.Trim() + "  ยูนิต : " + unitInfo.Unit_Code.Trim() + "  เลขที่บ้าน : " + unitInfo.Unit_Address.Trim();
                EmailContent.Type = EnumConfiguration.EmailContentType.ConfirmAppointment.ToString();
            }
            else if (EmailContentType == (int)EnumConfiguration.EmailContentType.ConfirmedAppointment)
            {
                strSubject = System.Configuration.ConfigurationManager.AppSettings["SystemName"].ToString() + "[แจ้ง Call Centre ยืนยันเลื่อนวันที่นัดหมายตรวจสอบ] โครงการ : " + unitInfo.Project_NameTH.Trim() + "  ยูนิต : " + unitInfo.Unit_Code.Trim() + "  เลขที่บ้าน : " + unitInfo.Unit_Address.Trim();
                EmailContent.Type = EnumConfiguration.EmailContentType.ConfirmedAppointment.ToString();
            }
            else if (EmailContentType == (int)EnumConfiguration.EmailContentType.RejectAppointment)
            {
                strSubject = System.Configuration.ConfigurationManager.AppSettings["SystemName"].ToString() + "[แจ้ง Call Centre ไม่ยืนยันเลื่อนวันที่นัดหมายตรวจสอบ] โครงการ : " + unitInfo.Project_NameTH.Trim() + "  ยูนิต : " + unitInfo.Unit_Code.Trim() + "  เลขที่บ้าน : " + unitInfo.Unit_Address.Trim();
                EmailContent.Type = EnumConfiguration.EmailContentType.RejectAppointment.ToString();
            }



            if (receiveDetial.flagAgent == true)
            {
                strSubject = "[งานเร่งด่วน]" + strSubject;
            }
            if (receiveDetial.flagChina == true)
            {
                strSubject = "[ลูกค้าจีน]" + strSubject;
            }
            //End Subject


            //Start Maillist
            string strSender = UserDetail.Email;
            string strReceiver = string.Empty;
            string strCC = string.Empty;
            string strBCC = string.Empty;

            EmailContent.unitInfo = unitInfo;
            EmailContent.receiveDetail = receiveDetial;
            EmailContent.receiveItem = receiveItem;
            EmailContent.customerContract = customerContract;
            EmailContent.ProjectCode = unitInfo.Project_Code;
            EmailContent.FlagChina = receiveDetial.flagChina.Value;

            List<string> listTOMail = new cb_Email().getlistemail(EmailContent);

            for (int i = 0; i <= listTOMail.Count - 1; i++)
            {
                strReceiver += listTOMail[i].ToString() + ";";
            }

            if (System.Configuration.ConfigurationManager.AppSettings["DebugMode"].ToString() == "1")
            { 
                //strReceiver = System.Configuration.ConfigurationManager.AppSettings["EmailTest"].ToString();
                strReceiver = "Kotchakorn.Si@sansiri.com";
                strSender = "HomeCareSystemAdmin@Sansiri.com";
            }
            ////strBCC = "thanawat.se@sansiri.com";
            //End Maillist
            //Start Content
            string content = new EmailContent().GetMailContent(EmailContent);
            //Start Content

            return cCommon.SendEmail(strSender, strReceiver, strCC, strBCC, strSubject, content, MailPriority.High);


        }
        public string sendEmailApprove(cWorksheet objWorksheet)
        {
            cb_Worksheet obj = new cb_Worksheet();
            HC_SP_Get_TD_WorkSheet_Result worksheet = obj.getWorksheet(objWorksheet.WorkSheet_ID);
            HC_SP_Get_TD_Approve_Result approve = obj.getApproveDetail(objWorksheet.WorkSheet_ID);
            TK_SP_Get_Customer_Info_By_Unit_Result customerInfo = obj.getCustomerInfo(objWorksheet.Unit_ID);
            HC_SP_Get_TD_WorkSheet_Item_Result worksheetItem = obj.getReceiveID(objWorksheet.WorkSheet_ID);
            HC_SP_Get_TD_Receive_Result receiveDetial = obj.getreceiveDetial(worksheetItem.Receive_ID);


            List<HC_SP_Get_TD_Approve_Item_Detail_Result> listWorksheetItemDetail = obj.getApproveitemDetail(objWorksheet.WorkSheet_ID);

            cEmailApprove EmailApprove = new cEmailApprove();

            EmailApprove.worksheet = worksheet;
            EmailApprove.listWorksheetDetail = listWorksheetItemDetail;
            EmailApprove.approve = approve;
            EmailApprove.customerInfo = customerInfo;
            EmailApprove.receiveDetail = receiveDetial;
            if (EnumConfiguration.ApproveStatus.APPV.ToString() == approve.Approve_Status || EnumConfiguration.ApproveStatus.REJT.ToString() == approve.Approve_Status)
            {
                EmailApprove.flagFinal = true;
                if (EnumConfiguration.ApproveStatus.APPV.ToString() == approve.Approve_Status)
                {
                    EmailApprove.flagApp = true;
                }
                else
                {
                    EmailApprove.flagApp = false;
                }


            }
            else
            {
                EmailApprove.flagFinal = false;
            }

            string content = new EmailContent().GetMailContentAppv(EmailApprove);

            cEmailResponsible EmailResponsible = new cEmailResponsible();
            EmailResponsible.TD_Approve_ID = approve.Approve_ID;
            EmailResponsible.ProjectCode = customerInfo.Project_Code.Trim();
            EmailResponsible.Role = UserDetail.Role.Trim();
            EmailResponsible.TypeEmail = EnumConfiguration.TypeEmail.TO.ToString();
            EmailResponsible.TypeBotton = objWorksheet.ApproveType;

            List<string> listTOMail = new cb_Email().getlistemailApprove(EmailResponsible);
            string strReceiver = string.Empty;
            string strCC = string.Empty;
            string strBCC = string.Empty;
            for (int i = 0; i <= listTOMail.Count - 1; i++)
            {
                strReceiver += listTOMail[i].ToString() + ";";

                // Call Noti Mobile Approve Only APPV
                if ( approve.PaymentType == "Y" && (objWorksheet.ApproveType == (int)EnumConfiguration.EmailActionType.Approved || objWorksheet.ApproveType == (int)EnumConfiguration.EmailActionType.SendApprove))
                {
                    cNotificationMobileApproveResult NotificationAPI = new cNotificationMobileApproveResult();
                    NotificationAPI.process = EnumConfiguration.ApproveType.WORK_ORDER.ToString();
                    NotificationAPI.intno = worksheet.WorkSheet_JobNoText;
                    NotificationAPI.doctype = "ZHC";
                    NotificationAPI.email = listTOMail[i].ToString();

                    cb_Notification Noti = new cb_Notification();
                    Noti.NotificationMobileApprove(NotificationAPI);
                }
            }

            if (objWorksheet.ApproveType == (int)EnumConfiguration.EmailActionType.Approved || objWorksheet.ApproveType == (int)EnumConfiguration.EmailActionType.NotApproved)
            {
                EmailResponsible.TypeEmail = EnumConfiguration.TypeEmail.CC.ToString();
                EmailResponsible.TypeBotton = objWorksheet.ApproveType;

                List<string> listCCMail = new cb_Email().getlistemailApprove(EmailResponsible);
                for (int i = 0; i <= listCCMail.Count - 1; i++)
                {
                    strCC += listCCMail[i].ToString() + ";";
                }

            }


            string strSubject = System.Configuration.ConfigurationManager.AppSettings["SystemName"].ToString() + "[แจ้งรายการรออนุมัติ] " + objWorksheet.Unit_Code + " " + customerInfo.Project_NameTH;

            if (System.Configuration.ConfigurationManager.AppSettings["DebugMode"].ToString() == "1")
                strReceiver = System.Configuration.ConfigurationManager.AppSettings["EmailTest"].ToString();

            strBCC = System.Configuration.ConfigurationManager.AppSettings["EmailTest"].ToString();


            return cCommon.SendEmail(UserDetail.Email, strReceiver, strCC, strBCC, strSubject, content, MailPriority.High);
        }
        public string sendEmailToSupport(cEmailSupport objEmailSupport, string VendorNameForF2)
        {
            cb_Email oEmail = new cb_Email();
            string strReceiver = string.Empty;
            string strBCC = string.Empty;
            string strCC = string.Empty;
            string strSubject = string.Empty;
            objEmailSupport.ProjectCode = objEmailSupport.DocumentItem != null ? objEmailSupport.DocumentItem.FirstOrDefault().ProjectCode : string.Empty;
            objEmailSupport.TypeEmail = EnumConfiguration.EmailContentType.PrintF.ToString();
            objEmailSupport.VendorNameForF2 = VendorNameForF2;

            string content = new EmailContent().GetMailContentToSupport(objEmailSupport);

            List<string> listTOMail = new cb_Email().getlistemail(objEmailSupport);

            for (int i = 0; i <= listTOMail.Count - 1; i++)
            {
                strReceiver += listTOMail[i].ToString() + ";";
            }

            if (System.Configuration.ConfigurationManager.AppSettings["DebugMode"].ToString() == "1")
                strReceiver = System.Configuration.ConfigurationManager.AppSettings["EmailTest"].ToString();

            strBCC = System.Configuration.ConfigurationManager.AppSettings["EmailTest"].ToString();
            strSubject = System.Configuration.ConfigurationManager.AppSettings["SystemName"].ToString() + "[แจ้งรายการพิมพ์ฟอร์ม HC-" + objEmailSupport.FormType+ " ] โครงการ " + (objEmailSupport.DocumentItem != null ? objEmailSupport.DocumentItem.FirstOrDefault().ProjectName : string.Empty); 

            return cCommon.SendEmail(UserDetail.Email, strReceiver, strCC, strBCC, strSubject, content, MailPriority.High);
        }

        public string sendEmailF2ToVendor(cEmailSupport objEmailSupport, string VendorNameForF2, string vendorEmail)
        {
            cb_Email oEmail = new cb_Email();
            string strReceiver = string.Empty;
            string strBCC = string.Empty;
            string strCC = string.Empty;
            string strSubject = string.Empty;
            objEmailSupport.ProjectCode = objEmailSupport.DocumentItem != null ? objEmailSupport.DocumentItem.FirstOrDefault().ProjectCode : string.Empty;
            objEmailSupport.TypeEmail = EnumConfiguration.EmailContentType.PrintF.ToString();
            objEmailSupport.VendorNameForF2 = VendorNameForF2;

            string content = new EmailContent().GetMailContentExportF2ToVendor(objEmailSupport);

            List<string> listTOMail = new cb_Email().getlistemail(objEmailSupport);
            for (int i = 0; i <= listTOMail.Count - 1; i++)
            {
                strReceiver += listTOMail[i].ToString() + ";";
            }

            if (System.Configuration.ConfigurationManager.AppSettings["DebugMode"].ToString() == "1")
                strReceiver = System.Configuration.ConfigurationManager.AppSettings["EmailTest"].ToString();

            strBCC = System.Configuration.ConfigurationManager.AppSettings["EmailTest"].ToString();
            strSubject = System.Configuration.ConfigurationManager.AppSettings["SystemName"].ToString() + "[แจ้งรายการเอกสาร HC-" + objEmailSupport.FormType + " ] โครงการ " + (objEmailSupport.DocumentItem != null ? objEmailSupport.DocumentItem.FirstOrDefault().ProjectName : string.Empty);

            return cCommon.SendNewEmail(UserDetail.Email, strReceiver, strCC, strBCC, strSubject, content, MailPriority.High);
        }

        public string sendEmailWorkSheet(cWorksheet objWorksheet, string TypeEmail, string SubjectEmail)
        {
            string strReceiver = string.Empty;
            string strCC = string.Empty;
            string strBCC = string.Empty;
            string strSubject = string.Empty;

            cb_Worksheet obj = new cb_Worksheet();
            HC_SP_Get_TD_WorkSheet_Result worksheet = obj.getWorksheet(objWorksheet.WorkSheet_ID);
            List<HC_SP_Get_TD_WorkSheet_Item_Result> worksheetItem = obj.getWorksheetItem(objWorksheet.WorkSheet_ID);
            TK_SP_Get_Customer_Info_By_Unit_Result customerInfo = obj.getCustomerInfo(worksheet.Unit_ID);
            HC_SP_Get_TD_Receive_Result receiveDetial = obj.getreceiveDetial(worksheetItem.FirstOrDefault().Receive_ID);

            cEmailWorkSheet EmailWorkSheet = new cEmailWorkSheet();
            EmailWorkSheet.worksheet = worksheet;
            EmailWorkSheet.worksheetItem = worksheetItem;
            EmailWorkSheet.customerInfo = customerInfo;
            EmailWorkSheet.receiveDetail = receiveDetial;
            
            string content = new EmailContent().GetMailContentWorkSheet(EmailWorkSheet);
            
            List<string> listTOMail = new cb_Email().getlistemail(TypeEmail, worksheet.Project_Code);

            for (int i = 0; i <= listTOMail.Count - 1; i++)
            {
                strReceiver += listTOMail[i].ToString() + ";";
            }

            if (System.Configuration.ConfigurationManager.AppSettings["DebugMode"].ToString() == "1")
                strReceiver = System.Configuration.ConfigurationManager.AppSettings["EmailTest"].ToString();

            strBCC = System.Configuration.ConfigurationManager.AppSettings["EmailTest"].ToString();
            strSubject = System.Configuration.ConfigurationManager.AppSettings["SystemName"].ToString() + SubjectEmail + " " + worksheet.Unit_Code + " " + customerInfo.Project_NameTH;

            return cCommon.SendEmail(UserDetail.Email, strReceiver, strCC, strBCC, strSubject, content, MailPriority.High);
        }

    }
}