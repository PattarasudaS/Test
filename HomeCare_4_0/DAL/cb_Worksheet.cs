using HomeCare_4_0.Common;
using HomeCare_4_0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCare_4_0.DAL
{
    public class cb_Worksheet
    {
        private HomeCareDBEntities context;
        public cb_Worksheet()
        {
            this.context = new HomeCareDBEntities();
        }

        public bool updateWorksheet(cWorksheet objWorksheet)
        {
            try
            {
                context.HC_SP_Update_TD_WorkSheet(objWorksheet.WorkSheet_ID, string.IsNullOrEmpty(objWorksheet.CloseJob_Date) ? null : cCommon.ddMMyyyyToyyyyMMdd(objWorksheet.CloseJob_Date, '/'), objWorksheet.Vendor_ID, objWorksheet.Vendor_Name
                , objWorksheet.VendorChangeRemark, string.IsNullOrEmpty(objWorksheet.RepairAppointmentFromDate) ? null : cCommon.ddMMyyyyToyyyyMMdd(objWorksheet.RepairAppointmentFromDate, '/')
                , string.IsNullOrEmpty(objWorksheet.RepairAppointmentToDate) ? null : cCommon.ddMMyyyyToyyyyMMdd(objWorksheet.RepairAppointmentToDate, '/')
                , objWorksheet.Operating_Percent
                , objWorksheet.Vat_Percent, objWorksheet.PaymentType, objWorksheet.PaymentType_ID, objWorksheet.User);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public double? insertWorksheetitemDetail(cWorksheetitemDetail objWorksheetitemDetail)
        {
            try
            {
                context.HC_SP_Insert_TD_WorkSheet_Item_Detail(objWorksheetitemDetail.WorkSheet_Item_Detail_ID, objWorksheetitemDetail.TD_WorkSheet_Item_ID, objWorksheetitemDetail.Order, objWorksheetitemDetail.List1_ID,
                    objWorksheetitemDetail.List2_ID, objWorksheetitemDetail.List3_ID, objWorksheetitemDetail.List4_ID, objWorksheetitemDetail.Special_Materia, objWorksheetitemDetail.Special_WagePrice, objWorksheetitemDetail.PriceMaterialType, objWorksheetitemDetail.PriceWageType, objWorksheetitemDetail.Quantity, objWorksheetitemDetail.Remark, objWorksheetitemDetail.CreateBy);
            }
            catch (Exception ex)
            {
                //return false;
            }

            var resultTotal = context.HC_SP_Get_TD_WorkSheet(objWorksheetitemDetail.WorkSheet_ID).FirstOrDefault();
            if (resultTotal != null && resultTotal.TotalPrice > 0)
            {
                return resultTotal.TotalPrice;
            }
            else
            {
                return 0;
            }
        }

        public bool updateWorksheetitem(cWorksheetitem objWorksheetite)
        {
            try
            {
                context.HC_SP_Update_TD_WorkSheet_Item(objWorksheetite.WorkSheet_ID, objWorksheetite.WorkSheet_Item_ID, objWorksheetite.Status, objWorksheetite.Cause_Text, string.IsNullOrEmpty(objWorksheetite.Confirm_Price_Date) ? null : cCommon.ddMMyyyyToyyyyMMdd(objWorksheetite.Confirm_Price_Date, '/'), string.IsNullOrEmpty(objWorksheetite.StartDate) ? null : cCommon.ddMMyyyyToyyyyMMdd(objWorksheetite.StartDate, '/'), string.IsNullOrEmpty(objWorksheetite.FinishDate) ? null : cCommon.ddMMyyyyToyyyyMMdd(objWorksheetite.FinishDate, '/'),objWorksheetite.User);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool ChangeWorksheetStatus(string WorkSheetJobNo, string User)
        {
            try
            {
                context.HC_SP_Change_WorkSheetStatus(WorkSheetJobNo, User);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool ChangeWorkSheetApproveStatus(string WorkSheetJobNo, string User)
        {
            try
            {
                context.HC_SP_Change_WorkSheetApproveStatus(WorkSheetJobNo, User);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public List<HC_SP_Get_TD_WorkSheet_Item_Detail_Result> getlistworksheetitemDetail(int workSheetitemID)
        {
            return context.HC_SP_Get_TD_WorkSheet_Item_Detail(workSheetitemID).ToList();
        }

        public bool sendApprovedWorksheet(cWorksheet objWorksheet)
        {
            try
            {
                context.HC_SP_Insert_TD_Approve(objWorksheet.WorkSheet_ID, objWorksheet.PaymentType, objWorksheet.PaymentType_ID,objWorksheet.ApprovedRemark, objWorksheet.User);
            }catch(Exception ex)
            {
                return false;
            }
            return true;
        }
        public bool ApprovedWorksheet(cWorksheetApprove objWorksheetApprove)
        {
            try
            {
                long ApproveID = context.HC_SP_Get_TD_Approve(objWorksheetApprove.WorkSheet_ID).FirstOrDefault().Approve_ID;
                context.HC_SP_Update_TD_Approve(ApproveID, bool.Parse(objWorksheetApprove.ApproveVal), objWorksheetApprove.Role, objWorksheetApprove.Remark, UserDetail.CodeName);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public HC_SP_Get_TD_Approve_Result getApproveDetail(long? worksheetID)
        {
            HC_SP_Get_TD_Approve_Result result = new HC_SP_Get_TD_Approve_Result();
            var response = context.HC_SP_Get_TD_Approve(worksheetID).FirstOrDefault();
            if (response != null) {
                return response;
            }
            else {
                return result;
            }

        }

        public List<HC_SP_Get_TD_Approve_Item_Detail_Result> getApproveitemDetail(long? worksheetID)
        {
            List<HC_SP_Get_TD_Approve_Item_Detail_Result> res = new List<HC_SP_Get_TD_Approve_Item_Detail_Result>();
            var result = context.HC_SP_Get_TD_Approve_Item_Detail(worksheetID).ToList();
            if (result != null)
            {
                return result;
            }
            else {
                return res;
            }
      
        }
        public HC_SP_Get_TD_WorkSheet_Result getWorksheet(long? worksheetID)
        {
            return context.HC_SP_Get_TD_WorkSheet(worksheetID).FirstOrDefault();
        }

        public TK_SP_Get_Customer_Info_By_Unit_Result getCustomerInfo(long UnitID)
        {
            return context.TK_SP_Get_Customer_Info_By_Unit(UnitID).FirstOrDefault();
        }

        public HC_SP_Get_TD_WorkSheet_Item_Result getReceiveID(long? workSheet_ID)
        {
            return context.HC_SP_Get_TD_WorkSheet_Item(workSheet_ID).FirstOrDefault();
        }
        public List<HC_SP_Get_TD_WorkSheet_Item_Result> getWorksheetItem(long? workSheet_ID)
        {
            return context.HC_SP_Get_TD_WorkSheet_Item(workSheet_ID).ToList();
        }

        public HC_SP_Get_TD_Receive_Result getreceiveDetial(long? receive_ID)            
        {
            return context.HC_SP_Get_TD_Receive(receive_ID).FirstOrDefault();
        }

        public List<HC_SP_Get_TD_Approve_Line_Result> getApproveHistory(long? approveID)
        {
            return context.HC_SP_Get_TD_Approve_Line(approveID).ToList();
        }

        public HC_SP_Get_WO_Status_Result getworkorderCurrentStatus(long? worksheetID)
        {
            return context.HC_SP_Get_WO_Status(worksheetID).FirstOrDefault();
        }
        public HC_SP_Check_HignValueCustomer_Result checkHignValueCustomer(long? worksheetID)
        {
            return context.HC_SP_Check_HignValueCustomer(worksheetID).FirstOrDefault();
        }

        public HC_SP_Check_CloseJob_Result checkCloseJob(long? worksheetID)
        {
            return context.HC_SP_Check_CloseJob(worksheetID).FirstOrDefault();
        }

        public string  deleteWorksheetitemDetail(cWorksheetitemDetail objWorksheetitemDetail)
        {
              
                
            string result=context.HC_SP_Update_TD_WorkSheet_Item_Detail(objWorksheetitemDetail.WorkSheet_Item_Detail_ID, objWorksheetitemDetail.TD_WorkSheet_Item_ID, objWorksheetitemDetail.UpdateBy).FirstOrDefault();
            if(string.IsNullOrEmpty(result))
            {
                var resultTotal = context.HC_SP_Get_TD_WorkSheet(objWorksheetitemDetail.WorkSheet_ID).FirstOrDefault();
                if (resultTotal != null && resultTotal.TotalPrice > 0)
                {
                    return resultTotal.TotalPrice.ToString("#,##0.00");
                }
                else
                {
                    return "0.00";
                }

            }
            else
            {
                return result;
            }
        }

        public string checkEndFix(long? reference_ID, long? reference_Item_ID)
        {
            return context.HC_SP_Check_EndFix(reference_ID, reference_Item_ID).FirstOrDefault();
        }

        public string checkChangeVendor(long? WorkSheet_ID)
        {
            return context.HC_SP_Check_ChangeVendor(WorkSheet_ID).FirstOrDefault();
        }

        public string checkChangeWorksheetStatus(string WorkSheetJobNo)
        {
            return context.HC_SP_Check_ChangeWorksheetStatus(WorkSheetJobNo).FirstOrDefault();
        }

        public string checkChangeWorksheetApproveStatus(string WorkSheetJobNo)
        {
            return context.HC_SP_Check_ChangeWorksheetApproveStatus(WorkSheetJobNo).FirstOrDefault();
        }

        public List<HC_SP_Get_DocumentFByWorkSheet_Result> getDocumentFByWorksheet(long? WorkSheetID)
        {
            return context.HC_SP_Get_DocumentFByWorkSheet(WorkSheetID).ToList();
        }

        public string checkNotificationHSA(long? WorkSheet_ID)
        {
            return context.HC_SP_Check_NotificationHSA(WorkSheet_ID).FirstOrDefault();
        }

       
        public string checkBeforApprove(long? WorkSheet_ID)
        {
            return context.HC_SP_Check_BeforApprove(WorkSheet_ID).FirstOrDefault();
        }

        public HC_SP_Get_Worksheet_Repair_Result GetWorksheetRepair(int workSheet_Item_ID)
        {
            return context.HC_SP_Get_Worksheet_Repair(workSheet_Item_ID).FirstOrDefault();
        }

        public HC_SP_Get_Worksheet_RepairAfter_Result GetWorksheetRepairAfter(int workSheet_Item_ID)
        {
            return context.HC_SP_Get_Worksheet_RepairAfter(workSheet_Item_ID).FirstOrDefault();
        }

        public void InsertRepairAppointment(cRepairAppointment ReceiveAppointment)
        {
            context.HC_SP_Insert_Extended_Repair(ReceiveAppointment.Worksheet_Item_ID, string.IsNullOrEmpty(ReceiveAppointment.ExtRepairAppointmentFormDate) ? null : cCommon.ddMMyyyyToyyyyMMdd(ReceiveAppointment.ExtRepairAppointmentFormDate, '/'),
                string.IsNullOrEmpty(ReceiveAppointment.ExtRepairAppointmentToDate) ? null : cCommon.ddMMyyyyToyyyyMMdd(ReceiveAppointment.ExtRepairAppointmentToDate, '/'), ReceiveAppointment.ExtendedResonID,
                ReceiveAppointment.ExtendedReasonRemark, ReceiveAppointment.ExtendedReasonRemarkCC, ReceiveAppointment.ExtendedApproveFlag,
                ReceiveAppointment.UserCode);
        }

        public string checkSpacialMemo(long? worksheetID)
        {
            return context.HC_SP_Check_SpacialMemo(worksheetID).FirstOrDefault();
        }

        public List<HC_SP_Get_TD_Extended_Repair_History_Result> GetRepairAppointmentHistory(long? worksheetitemID)
        {
            return context.HC_SP_Get_TD_Extended_Repair_History(worksheetitemID).ToList();
        }

        public void InsertRepairAfterAppointment(cRepairAfterAppointment ReceiveAppointment)
        {
            context.HC_SP_Insert_Extended_RepairAfter(
                   ReceiveAppointment.Worksheet_Item_ID
                , ReceiveAppointment.RepairAppointmentTimes
                , string.IsNullOrEmpty(ReceiveAppointment.RepairAppointmentDate_Old) ? null : cCommon.ddMMyyyyToyyyyMMdd(ReceiveAppointment.RepairAppointmentDate_Old, '/')
                , string.IsNullOrEmpty(ReceiveAppointment.RepairAppointmentDate) ? null : cCommon.ddMMyyyyToyyyyMMdd(ReceiveAppointment.RepairAppointmentDate, '/')
                , string.IsNullOrEmpty(ReceiveAppointment.ExpectDate_Old) ? null : cCommon.ddMMyyyyToyyyyMMdd(ReceiveAppointment.ExpectDate_Old, '/')
                , string.IsNullOrEmpty(ReceiveAppointment.ExpectDate) ? null : cCommon.ddMMyyyyToyyyyMMdd(ReceiveAppointment.ExpectDate, '/')
                , ReceiveAppointment.ExtendedReasonID
                , ReceiveAppointment.ExtendedRemark
                , ReceiveAppointment.ExtendedUserID
                , ReceiveAppointment.ExtendedUserCode
                , ReceiveAppointment.ExtendedApproveFlag
                , ReceiveAppointment.ExtendedRemarkCC
                , ReceiveAppointment.ExtendedUserIDCC
                , ReceiveAppointment.ExtendedUserCodeCC
                , ReceiveAppointment.CreatedBy
                );
        }
		public bool cancelConfigWorksheet(cWorksheet objWorksheet)
		{
			try
			{
				context.HC_SP_API_UPDATE_CONFIG_CLOSE_WORKSHEET(objWorksheet.WorkSheet_ID, objWorksheet.CancelRemark, UserDetail.CodeName);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
    }
}