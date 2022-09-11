using HomeCare_4_0.Models;
using HomeCare_4_0.ClassLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeCare_4_0.DAL;
using HomeCare_4_0.Common;
using System.Web.Mail;
/// <summary>
/// Controller สำหรับ ใบงาน
/// </summary>
namespace HomeCare_4_0.Controllers
{
    public class WorkSheetController : BaseController
    {
        private Cls_HC_TM_Lookup cls_hc_tm_lookup = new Cls_HC_TM_Lookup();
		private static List<HC_SP_GET_PERMISSION_Result> PermisionList;
		public ActionResult WorkSheetDetail(long? ID)
		{
            if (ID == null) {
                return RedirectToAction("Index", "Home");
            }
			HomeCareDBEntities HomeCareDB = new HomeCareDBEntities();
			WorkSheetIndexViewModel WoorkSheet_Data = new WorkSheetIndexViewModel();
			cAuthorization obj2 = new cAuthorization();
			// Get Permission
			PermisionList = obj2.getPermission(UserDetail.UserID);

            // WorkSheet Header
            var data4 = HomeCareDB.HC_SP_Get_TD_WorkSheet(ID).FirstOrDefault();
			WorkSheetHeadlViewModel Data_WorkOrder = new WorkSheetHeadlViewModel();
            if (data4 != null) {
                Data_WorkOrder.WorkSheet_ID = data4.WorkSheet_ID;
                Data_WorkOrder.WorkSheet_JobNoText = data4.WorkSheet_JobNoText;
                Data_WorkOrder.WorkSheet_Status = data4.WorkSheet_Status;
                Data_WorkOrder.Approve_Status = data4.Approve_Status;
                Data_WorkOrder.WorkSheet_StatusID = data4.WorkSheet_StatusID;
                Data_WorkOrder.WorkSheet_CloseJobDate = data4.WorkSheet_CloseJobDate;
                Data_WorkOrder.WorkSheet_CreateBy = data4.WorkSheet_CreateBy;
                Data_WorkOrder.WorkSheet_Create_Date = data4.WorkSheet_Create_Date;
                Data_WorkOrder.VendorName = data4.VendorName;
                Data_WorkOrder.VendorID = data4.VendorID;
                Data_WorkOrder.TotalPrice = data4.TotalPrice;
                Data_WorkOrder.OperatingPercent = data4.OperatingPercent;
                Data_WorkOrder.VatPercent = data4.VatPercent;
                Data_WorkOrder.NetPrice = data4.NetPrice;
                Data_WorkOrder.WorkSheet_RepairAppointmentFromDate = data4.WorkSheet_RepairAppointmentFromDate;
                Data_WorkOrder.WorkSheet_RepairAppointmentToDate = data4.WorkSheet_RepairAppointmentToDate;
                Data_WorkOrder.Approve_StatusID = data4.Approve_StatusID;

            }

            // WorkSheet Item
            var data5 = HomeCareDB.HC_SP_Get_TD_WorkSheet_Item(ID).ToList();
			List<WorkSheetitemlViewModel> Data_WorkOrerItem = new List<WorkSheetitemlViewModel>();
			foreach (var item in data5)
			{
				WorkSheetitemlViewModel objItem = new WorkSheetitemlViewModel();
				objItem.WI_ID = item.WI_ID;
				objItem.WI_Status_ID = item.WI_Status_ID;
				objItem.Guarantee_Name = item.Guarantee_Name;
				objItem.SpecL1_Name = item.SpecL1_Name;
				objItem.SpecL2_Name = item.SpecL2_Name;
				objItem.SpecL3_Name = item.SpecL3_Name;
				objItem.Location_Name = item.Location_Name;
				objItem.Cause_Name = item.Cause_Name;
				objItem.Cause_Text = item.Cause_Text;
				objItem.Priority_Name = item.Priority_Name;
				objItem.WI_ExpectDate = item.WI_ExpectDate;
				objItem.WI_ConfirmPriceFlag = item.WI_ConfirmPriceFlag;
				objItem.WI_ConfirmPriceDate = item.WI_ConfirmPriceDate;
				objItem.WI_StartDateFlag = item.WI_StartDateFlag;
				objItem.WI_StartDate = item.WI_StartDate;
				objItem.WI_FinishDateFlag = item.WI_FinishDateFlag;
				objItem.WI_FinishDate = item.WI_FinishDate;
				objItem.RepairAppointmentDate = item.RepairAppointmentDate;
				Data_WorkOrerItem.Add(objItem);
			}

            if (data5.Count > 0) {
                if (data5.FirstOrDefault().Receive_ID != null)
                {
                    // Receive 
                    long? @TD_Receive_ID = data5.FirstOrDefault().Receive_ID;
                    var data2 = HomeCareDB.HC_SP_Get_TD_Receive(@TD_Receive_ID).ToList();
                    ReceiveHeadlViewModel Data_Receive = new ReceiveHeadlViewModel();
                    Data_Receive.Inform_Receive_Head_ID = @TD_Receive_ID;
                    Data_Receive.Receive_Status_ID = data2.FirstOrDefault().Receive_Status_ID;
                    Data_Receive.Receive_Status_Name = data2.FirstOrDefault().Receive_Status_Name;
                    Data_Receive.Receive_HC_User_ID = data2.FirstOrDefault().Receive_HC_User_ID;
                    Data_Receive.Receive_HC_User = data2.FirstOrDefault().Receive_HC_User;
                    Data_Receive.Receive_HC_Date = data2.FirstOrDefault().Receive_HC_Date;
                    Data_Receive.Receive_HC_Remark = data2.FirstOrDefault().Receive_HC_Remark;
                    Data_Receive.Receive_CC_Date = data2.FirstOrDefault().Receive_CC_Date;
                    Data_Receive.Receive_CC_Remark = data2.FirstOrDefault().Receive_CC_Remark;
                    Data_Receive.Receive_Appointment_Date = data2.FirstOrDefault().Receive_Appointment_Date;
                    Data_Receive.Receive_Appointment_Time_From = data2.FirstOrDefault().Receive_Appointment_Time_From;
                    Data_Receive.Receive_Appointment_Time_To = data2.FirstOrDefault().Receive_Appointment_Time_To;
                    Data_Receive.Receive_FlagChina = data2.FirstOrDefault().flagChina;

                    // Inform 
                    var data3 = HomeCareDB.HC_SP_Get_TD_Receive(@TD_Receive_ID).ToList();
                    InformHeadlViewModel Data_Inform = new InformHeadlViewModel();
                    Data_Inform.Inform_JobNoText = data3.FirstOrDefault().Receive_JobNoText;
                    Data_Inform.Main_Process_Name = data3.FirstOrDefault().Main_ProcessName;
                    Data_Inform.Inform_Date = data3.FirstOrDefault().Inform_Date;
                    Data_Inform.Inform_User_ID = data3.FirstOrDefault().Inform_User_ID;
                    Data_Inform.Inform_User = data3.FirstOrDefault().Inform_User;
                    Data_Inform.Inform_Remark = data3.FirstOrDefault().Inform_Remark;
                    Data_Inform.Inform_CustomerType_ID = data3.FirstOrDefault().Inform_CustomerType_ID;
                    Data_Inform.Inform_CustomerType_Name = data3.FirstOrDefault().Inform_CustomerType_Name;
                    Data_Inform.Inform_CustomerName = data3.FirstOrDefault().Inform_CustomerType_Name;
                    Data_Inform.Inform_CustomerName = data3.FirstOrDefault().Inform_CustomerName;
                    Data_Inform.Inform_CustomerTel = data3.FirstOrDefault().Inform_CustomerTel;

                    if (data2.FirstOrDefault().UnitID != null)
                    {
                        // Customer Data
                        var data = HomeCareDB.TK_SP_Get_Customer_Info_By_Unit(data2.FirstOrDefault().UnitID).ToList();
                        CustomerViewModel Data_Customer = new CustomerViewModel();
                        Data_Customer.FullName = data.FirstOrDefault().FullNameTH_1;
                        Data_Customer.FullTelephone = data.FirstOrDefault().Mobile_1 + "," + data.FirstOrDefault().Telephone_1;
                        Data_Customer.VipStatus = HomeCareDB.TK_SP_Get_CheckProspectVipStatus(data.FirstOrDefault().ID_1).FirstOrDefault();

                        // Unit Data
                        UnitViewModel Data_Unit = new UnitViewModel();
                        Data_Unit.PJ_Name = data.FirstOrDefault().Project_NameTH;
                        Data_Unit.PJ_Code = data.FirstOrDefault().Project_Code;
                        Data_Unit.Unit_Code = data2.FirstOrDefault().UnitCode;
                        Data_Unit.Unit_Address = data.FirstOrDefault().Unit_Address;
                        Data_Unit.Unit_Code_Address = data.FirstOrDefault().Unit_Code_Address;
                        Data_Unit.Unit_Model = data.FirstOrDefault().Unit_Model;
                        Data_Unit.StartGuaranteeDate = data.FirstOrDefault().StartGuaranteeDate;
                        Data_Unit.EndGuaranteeDate = data.FirstOrDefault().EndGuaranteeDate;
                        Data_Unit.ExtraGuaranteeDate = data.FirstOrDefault().ExtraGuaranteeDate;
                        Data_Unit.Unit_ID = data.FirstOrDefault().Unit_ID;
                        Data_Unit.Unit_GuaranteedYield = data.FirstOrDefault().Unit_GuaranteedYield;
                        Data_Unit.Unit_Defer = data.FirstOrDefault().Unit_Defer;
                        Data_Unit.PJ_ID = data.FirstOrDefault().Project_ID;

                        WoorkSheet_Data.Data_Customer = Data_Customer;
                        WoorkSheet_Data.Data_Unit = Data_Unit;
                        WoorkSheet_Data.Data_Receive = Data_Receive;
                        WoorkSheet_Data.Data_Inform = Data_Inform;
                        WoorkSheet_Data.Data_WorkSheet = Data_WorkOrder;
                        WoorkSheet_Data.Data_WorkSheetItem = Data_WorkOrerItem;

                    }

                    cb_FileUpload objouploadFile = new cb_FileUpload();
                    List<HC_SP_Get_TD_Attachment_Result> listAttachFile = objouploadFile.getAttachFile(ID, EnumConfiguration.AttachFileRefType.W.ToString(), 0, 0);// 0 IS ALL Attach
                    WoorkSheet_Data.Data_WorkSheetAttachment = listAttachFile;

                    cb_Worksheet objWorjsheet = new cb_Worksheet();
                    WoorkSheet_Data.Data_DocumentF = objWorjsheet.getDocumentFByWorksheet(ID);

                }
            }
			
            
			return View(WoorkSheet_Data);
		}
		public ActionResult WorkSheetRepair()
		{
			return View();
		}
        public ActionResult WorkSheetYearly()
        {
            return View();
        }
        [HttpPost]
        public bool updateWorksheet(cWorksheet objWorksheet)
        {
            cb_Worksheet obj = new cb_Worksheet();
            //edit by Ajjima Change send mail smtp (call construction API)
            //  HC_SP_Get_WO_Status_Result OldStatus = obj.getworkorderCurrentStatus(objWorksheet.WorkSheet_ID);
            bool result = obj.updateWorksheet(objWorksheet);

            ////send Email ปิดใบงาน F1, F9, F5
            //HC_SP_Get_WO_Status_Result NewStatus = obj.getworkorderCurrentStatus(objWorksheet.WorkSheet_ID);
            //if ((OldStatus.ID == 14 && (NewStatus.ID == 15 || NewStatus.ID == 16)) || (OldStatus.ID != NewStatus.ID && NewStatus.ID == 17))
            //{
            //    cb_Email objEmail = new cb_Email();
            //    objEmail.sendEmailWorkSheet(objWorksheet, "WorksheetClose", "[แจ้งปิดใบงาน]");
            //}

            return result;
        }
        [HttpPost]
        public bool updateWorksheetItem(cWorksheetitem objWorksheetitem)
        {
            cb_Worksheet obj = new cb_Worksheet();
            HC_SP_Get_WO_Status_Result OldStatus = obj.getworkorderCurrentStatus(objWorksheetitem.WorkSheet_ID);
            bool result = obj.updateWorksheetitem(objWorksheetitem);

            //send Email ยืนยันงานเสร็จ สำหรับลุกค้า High Value เท่านั้น
            HC_SP_Get_WO_Status_Result NewStatus = obj.getworkorderCurrentStatus(objWorksheetitem.WorkSheet_ID);
            if ((OldStatus.ID == 13 && NewStatus.ID == 14) && (OldStatus.ID != NewStatus.ID))
            {
                HC_SP_Check_HignValueCustomer_Result HignValueCustomer = obj.checkHignValueCustomer(objWorksheetitem.WorkSheet_ID);
                if (HignValueCustomer.HignValueCustomerTypeID != "")
                {
                    cb_Email objEmail = new cb_Email();
                    objEmail.sendEmailWorkSheet(objWorksheetitem, "WorksheetFinish", "[แจ้งซ่อมงานแล้วเสร็จ][High Value Customer]");
                }
            }

            return result;
        }
        [HttpPost]
        public JsonResult getlistworksheetitemDetail(int workSheetitemID)
        {
            cb_Worksheet obj = new cb_Worksheet();
            List<HC_SP_Get_TD_WorkSheet_Item_Detail_Result> result = obj.getlistworksheetitemDetail(workSheetitemID);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public string insertWorksheetitemDetail(cWorksheetitemDetail objWorksheetitemDetail)
        {
            cb_Worksheet obj = new cb_Worksheet();
            double? result = obj.insertWorksheetitemDetail(objWorksheetitemDetail);
            return result.Value.ToString("#,##0.00");
        }
        [HttpPost]
        public string deleteWorksheetitemDetail(cWorksheetitemDetail objWorksheetitemDetail)
        {
            cb_Worksheet obj = new cb_Worksheet();
            string result = obj.deleteWorksheetitemDetail(objWorksheetitemDetail);
            return result;

        }

        [HttpPost]
        public bool ChangeWorksheetStatus(string WorkSheetJobNo)
        {
            cb_Worksheet obj = new cb_Worksheet();
            bool result = obj.ChangeWorksheetStatus(WorkSheetJobNo, UserDetail.CodeName);
            return result;
        }

        [HttpPost]
        public bool ChangeWorksheetApproveStatus(string WorkSheetJobNo)
        {
            cb_Worksheet obj = new cb_Worksheet();
            bool result = obj.ChangeWorkSheetApproveStatus(WorkSheetJobNo, UserDetail.CodeName);
            return result;
        }

        [HttpPost]
        public bool sendApprovedWorksheet(cWorksheet objWorksheet)
        {
            cb_Worksheet obj = new cb_Worksheet();
            bool result = obj.sendApprovedWorksheet(objWorksheet);

            //If success Send Emaill Approve
            if (result)
            {
                cb_Email oEmail = new cb_Email();
                oEmail.sendEmailApprove(objWorksheet);

            }
            return result;
        }
        [HttpPost]
        public bool ApprovedWorksheet(cWorksheetApprove objWorksheetApprove)
        {
            cb_Worksheet obj = new cb_Worksheet();
            bool result = obj.ApprovedWorksheet(objWorksheetApprove);

            if (result)
            {
                cb_Email oEmail = new cb_Email();
                oEmail.sendEmailApprove(objWorksheetApprove);

            }

            return result;
        }
        [HttpPost]
        public JsonResult getApproveDetail(long? worksheetID)
        {
            cb_Worksheet obj = new cb_Worksheet();
            HC_SP_Get_TD_Approve_Result result = obj.getApproveDetail(worksheetID);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getApproveHistory(long? ApproveID)
        {
            cb_Worksheet obj = new cb_Worksheet();
            List<HC_SP_Get_TD_Approve_Line_Result> result = obj.getApproveHistory(ApproveID);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getApproveitemDetail(long? worksheetID)
        {
            cb_Worksheet obj = new cb_Worksheet();
            List<HC_SP_Get_TD_Approve_Item_Detail_Result> result = obj.getApproveitemDetail(worksheetID);

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult getworkorderCurrentStatus(long? worksheetID)
        {
            cb_Worksheet obj = new cb_Worksheet();
            HC_SP_Get_WO_Status_Result result = obj.getworkorderCurrentStatus(worksheetID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult checkCloseJob(long? worksheetID)
        {
            cb_Worksheet obj = new cb_Worksheet();
            HC_SP_Check_CloseJob_Result result = obj.checkCloseJob(worksheetID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult checkEndFix(long? Reference_ID, long? Reference_Item_ID)
        {
            cb_Worksheet obj = new cb_Worksheet();
            string result = obj.checkEndFix(Reference_ID, Reference_Item_ID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult checkChangeVendor(long? WorkSheet_ID)
        {
            cb_Worksheet obj = new cb_Worksheet();
            string result = obj.checkChangeVendor(WorkSheet_ID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult checkChangeWorksheetStatus(string WorkSheetJobNo)
        {
            cb_Worksheet obj = new cb_Worksheet();
            string result = obj.checkChangeWorksheetStatus(WorkSheetJobNo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult checkChangeWorksheetApproveStatus(string WorkSheetJobNo)
        {
            cb_Worksheet obj = new cb_Worksheet();
            string result = obj.checkChangeWorksheetApproveStatus(WorkSheetJobNo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PostNotificationHSA(cWorksheet objWorksheet)
        {
            string message = "";
            message = checkNotificationHSA(objWorksheet.WorkSheet_ID, objWorksheet.ReceiveJobNoText);
            if (message != "")
            {
                cb_HomeServiceAPI HomeServiceAPI = new cb_HomeServiceAPI(objWorksheet.ProjectCode, objWorksheet.UnitAddress, objWorksheet.Unit_Code, message);
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }


        private string checkNotificationHSA(long? WorkSheet_ID, string JobNoText)
        {
            string message = "";
            cb_Worksheet obj = new cb_Worksheet();
            string result = obj.checkNotificationHSA(WorkSheet_ID);
            if (result != "")
            {
                message = "[" + JobNoText + "] " + System.Configuration.ConfigurationManager.AppSettings["NotiMsg_Completed"];
            }
            return message;

        }
        public JsonResult checkBeforApprove(int WorkSheet_ID)
        {
            cb_Worksheet obj = new cb_Worksheet();
            string result = obj.checkBeforApprove(WorkSheet_ID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetWorksheetRepair(int WorkSheet_Item_ID)
        {
            cb_Worksheet obj = new cb_Worksheet();
            HC_SP_Get_Worksheet_Repair_Result result = obj.GetWorksheetRepair(WorkSheet_Item_ID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetWorksheetRepairAfter(int WorkSheet_Item_ID)
        {
            cb_Worksheet obj = new cb_Worksheet();
            HC_SP_Get_Worksheet_RepairAfter_Result result = obj.GetWorksheetRepairAfter(WorkSheet_Item_ID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult InsertRepairAppointment(cRepairAppointment Repairppointment)
        {
            bool result;
            try
            {
                cb_Worksheet obj = new cb_Worksheet();
                obj.InsertRepairAppointment(Repairppointment);

                cb_Email objEmail = new cb_Email();
                cWorksheet objWorksheet = new cWorksheet();
                objWorksheet.WorkSheet_ID = Repairppointment.WorkSheetID;
                if(Repairppointment.ExtendedApproveFlag==null)
                {
                    objEmail.sendEmailWorkSheet(objWorksheet, "ConfirmExtendRepair", "[แจ้ง Home Care ขอเลื่อนวันที่นัดหมายซ่อม]");
                   
                }
                else if (Repairppointment.ExtendedApproveFlag == true)
                {
                    objEmail.sendEmailWorkSheet(objWorksheet, "ConfirmedExtendRepair", "[แจ้ง Call Centre ยืนยันเลื่อนวันที่นัดหมายซ่อม]");
                }
                else if (Repairppointment.ExtendedApproveFlag == false)
                {
                    objEmail.sendEmailWorkSheet(objWorksheet, "RejectExtendRepair", "[แจ้ง Call Centre ยกเลิกเลื่อนวันที่นัดหมายซ่อม]");
                }


                result = true;
            }
            catch
            {
                result = false;
            }

            return Json(result, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult CheckSpacialMemo(long? WorksheetID)
        {
            cb_Worksheet obj = new cb_Worksheet();
            string result = obj.checkSpacialMemo(WorksheetID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetRepairAppointmentHistory(long? WorksheetitemID)
        {
             cb_Worksheet obj = new cb_Worksheet();
            List< HC_SP_Get_TD_Extended_Repair_History_Result> result = obj.GetRepairAppointmentHistory(WorksheetitemID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult InsertRepairAfterAppointment(cRepairAfterAppointment Repairppointment)
        {
            bool result;
            try
            {
                cb_Worksheet obj = new cb_Worksheet();
                obj.InsertRepairAfterAppointment(Repairppointment);
                
                result = true;
            }
            catch
            {
                result = false;
            }

            return Json(result, JsonRequestBehavior.AllowGet);

        }

		[HttpPost]
		public JsonResult cancelConfigWorksheet(cWorksheet objWorksheet)
		{
			cb_Worksheet obj = new cb_Worksheet();
			bool result = obj.cancelConfigWorksheet(objWorksheet);
			return Json(result, JsonRequestBehavior.AllowGet);
		}

		public static bool checkPermission(string permission)
		{
			for (int i = 0; i < PermisionList.Count; i++)
			{
				if (PermisionList[i].PermissionDescription.ToString() == permission)
				{
					return true;
				}
			}
			return false;
		}


	}
}