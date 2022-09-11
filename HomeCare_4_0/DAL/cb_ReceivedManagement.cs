using HomeCare_4_0.Common;
using HomeCare_4_0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCare_4_0.DAL
{
    public class cb_ReceivedManagement
    {
        private HomeCareDBEntities context;
        public cb_ReceivedManagement()
        {
            this.context = new HomeCareDBEntities();
        }

        public HC_SP_Get_TD_Receive_Result getTD_Receive(long? received_ID)
        {
            return context.HC_SP_Get_TD_Receive(received_ID).FirstOrDefault();
        }
        public List<HC_SP_Get_TD_Receive_Item_Result> getTD_Receive_Item(long? received_ID)
        {
            return context.HC_SP_Get_TD_Receive_Item(received_ID).ToList();
        }
        public HC_SP_Get_TD_Receive_Item_Modal_Result getTD_Receive_Item_Modal(long? ReceiveItem_ID)
        {
            return context.HC_SP_Get_TD_Receive_Item_Modal(ReceiveItem_ID).FirstOrDefault();
        }
        public List<HC_SP_Get_TD_Receive_Item_Detail_Result> getTD_Receive_Item_Detail(long? ReceiveItem_ID)
        {
            return context.HC_SP_Get_TD_Receive_Item_Detail(ReceiveItem_ID).ToList();
        }
        public List<HC_SP_Get_TD_Receive_Item_Detail_Group_Result> getlistWorkorder(int received_ID)
        {
            return context.HC_SP_Get_TD_Receive_Item_Detail_Group(received_ID).ToList();
        }

		public bool updateTD_Receive(cReceive objReceive)
        {
            try
            {
                int result = context.HC_SP_Update_TD_Receive(objReceive.ReceiveID, objReceive.Receive_StatusID
                            , objReceive.Receive_HCRemark, objReceive.Receive_CCRemark
                            , objReceive.Receive_Confirm_Status_ID, objReceive.Receive_Confirm_Remark
                            , objReceive.Receive_AppointmentDateFormateDate, objReceive.Receive_AppointmentTime_From, objReceive.Receive_AppointmentTime_To
                            , objReceive.ddlOverAPDate_ReasonID
                            , string.IsNullOrEmpty(objReceive.Receive_Appointment_Date_Real) ? null : cCommon.ddMMyyyyToyyyyMMdd(objReceive.Receive_Appointment_Date_Real, '/')
                            , objReceive.User_Code, objReceive.User_ID);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public bool updateTD_Receive_Item_Modal(cReceive objReceive)
        {
            try
            {
                int result = context.HC_SP_Update_TD_Receive_Item_Modal(objReceive.ID, objReceive.SpecL1ID, objReceive.SpecL2ID, objReceive.SpecL3ID, objReceive.LocationID, objReceive.CauseID, objReceive.DuplicateFixid, UserDetail.CodeName);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
		public bool update_Config_TD_Receive_Item_Modal(cReceive objReceive)
        {
            try
            {
                context.HC_SP_CONFIG_UPDATE_TD_RECEIVE_ITEM_MODAL(objReceive.ID, objReceive.SpecL1ID, objReceive.SpecL2ID, objReceive.SpecL3ID, objReceive.LocationID, objReceive.CauseID, objReceive.DuplicateFixid, UserDetail.CodeName);
				return true;
			}
            catch (Exception ex)
            {
                return false;
            }

        }


        public bool updateTD_Receive_Item_Detail(cReceive objReceiveitem)
        {
            try
            {
                int result = context.HC_SP_Update_TD_Receive_Item_Detail(objReceiveitem.ReceiveItemID, objReceiveitem.ID, objReceiveitem.No, objReceiveitem.StatusID, objReceiveitem.VendorID, objReceiveitem.VendorName
                    , objReceiveitem.JobTypeID
                    , string.IsNullOrEmpty(objReceiveitem.RepairAppointmentDateFrom) ? null : cCommon.ddMMyyyyToyyyyMMdd(objReceiveitem.RepairAppointmentDateFrom, '/')
                    , string.IsNullOrEmpty(objReceiveitem.RepairAppointmentDateTo) ? null : cCommon.ddMMyyyyToyyyyMMdd(objReceiveitem.RepairAppointmentDateTo, '/')
                    , objReceiveitem.ReasonID
                    , objReceiveitem.PriceID

                    , objReceiveitem.Remark, UserDetail.CodeName,UserDetail.UserID);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public string OpenWorkOrder(cReceive objReceive)
        {
            // Insert record to WorkSheet table in HomeCare
            string error = string.Empty;
            System.Data.Entity.Core.Objects.ObjectParameter MsgError = new System.Data.Entity.Core.Objects.ObjectParameter("MsgError", typeof(string));  // Define Parameter เพื่อรอรับค่า กลับ
            List<long?> worksheetIDList = context.HC_SP_Insert_TD_WorkSheet(objReceive.ReceiveID, objReceive.User_Code,UserDetail.UserID).ToList();

            // Call API Notification 
            string message = "[" + objReceive.ReceiveJobNoText + "] " + System.Configuration.ConfigurationManager.AppSettings["NotiMsg_OnProcess"];
          //  cb_HomeServiceAPI HomeServiceAPI = new cb_HomeServiceAPI(objReceive.ProjectCode, objReceive.UnitAddress, objReceive.UnitCode, message);

            // Insert record to activity table in HomeCare // เข้าไป insert ใน store แทน เพราะจะได้สร้างตามจำนวนใบงาน
            //foreach (long? worksheetID in worksheetIDList)
            //{
            //    context.HC_SP_Insert_TD_Activity("R", objReceive.ProspectID, objReceive.ProjectID, objReceive.User_Code, worksheetID);
            //}

            // Insert record to activity table in TrackingSystem
            //TrackingSystemEntities contextTracking = new TrackingSystemEntities();
            //foreach (long? worksheetID in worksheetIDList)
            //{
            //    contextTracking.TK_SP_Insert_activity_For_Homecare("R", objReceive.ProspectID, objReceive.ProjectID, objReceive.User_Code, worksheetID);
            //}




            return MsgError.ToString();
        }
        public string checkDuplicateFix(long? unitID, long? ddlGuaranteeID, long? ddlSpec1ID, long? ddlSpec2ID, long? ddlSpec3ID, long? ddlLocationID)
        {
            return context.HC_SP_Check_DuplicateFix(unitID, ddlGuaranteeID, ddlSpec1ID, ddlSpec2ID, ddlSpec3ID, ddlLocationID).FirstOrDefault();
        }

        public bool InsertReceiveItem(cReceiveItem objReceiveItem)
        {
            try
            {
                System.Data.Entity.Core.Objects.ObjectParameter TD_Receive_Item_ID = new System.Data.Entity.Core.Objects.ObjectParameter("TD_Receive_Item_ID", typeof(Int64));  //

                context.HC_SP_Insert_TD_Receive_Item(objReceiveItem.ReceiveID, objReceiveItem.ItemNo, objReceiveItem.GuaranteeID, objReceiveItem.Detail, "", objReceiveItem.Remark,
                    objReceiveItem.RepairID, objReceiveItem.ReceivedItemStatusID, true, objReceiveItem.User,string.Empty,string.Empty, TD_Receive_Item_ID);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }

        }

        public List<HC_SP_Get_Receive_Appointment_Result> GetReceivedAppointment(long? ID)
        {
            try
            {
                return context.HC_SP_Get_Receive_Appointment(ID).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void InsertReceivedAppointment(cReceiveAppointment receivedAppointment)
        {
            try
            {
                context.HC_SP_Insert_Extended_Appointment(receivedAppointment.ReceiveID, string.IsNullOrEmpty(receivedAppointment.ExtendedReceiveAppointmentDate) ? null : cCommon.ddMMyyyyToyyyyMMdd(receivedAppointment.ExtendedReceiveAppointmentDate, '/'),
                receivedAppointment.ExtendedReceiveAppointmentTimeForm, receivedAppointment.ExtendedReceiveAppointmentTimeTo,receivedAppointment.ExtendedResonID,
                receivedAppointment.ExtendedReasonRemark, receivedAppointment.ExtendedReasonRemarkCC, receivedAppointment.ExtendedApproveFlag,
                receivedAppointment.UserCode);
            }
            catch (Exception ex)
            {
                
            }
        }

        public string CheckReceivedAppointment(long? Job_ID, string Job_Code)
        {
            try
            {
                return context.HC_SP_Check_ChangeAppointment(Job_ID, Job_Code).ToList().FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string CheckReceivedAppointmentRepairAfter(long? Job_ID, string Job_Code)
        {
            try
            {
                return context.HC_SP_Check_ChangeAppointmentRepairAfter(Job_ID, Job_Code).ToList().FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<HC_SP_Get_TD_Extended_Appointment_History_Result> GetReceivedAppointmentHistory(long? ID)
        {
            try
            {
                return context.HC_SP_Get_TD_Extended_Appointment_History(ID).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

		public bool UpdateConfigRealDate(long? Receive_Id, DateTime Date,string Remark,  string User_Code) {
			try
			{
				context.HC_SP_CONFIG_UPDATE_REALDATE_TD_RECEIVE(Receive_Id, Date, Remark, User_Code);
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

	}
}