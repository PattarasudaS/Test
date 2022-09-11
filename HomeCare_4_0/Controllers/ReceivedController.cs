using HomeCare_4_0.ClassLib;
using HomeCare_4_0.Common;
using HomeCare_4_0.DAL;
using HomeCare_4_0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
/// <summary>
/// Controller สำหรับ รับเรื่อง
/// </summary>
namespace HomeCare_4_0.Controllers
{
    public class ReceivedController : BaseController
    {

        public enum ReceivedItem
        {
            itemID = 0,
            itemRemark = 1,
            itemStatusID = 2,
            itemStatusIDOld = 3,
            itemRepairID = 4,
            itemCancelResonID = 5
        }

        private Cls_HC_TM_Lookup cls_hc_tm_lookup = new Cls_HC_TM_Lookup();
        private long Receive_ID;
        private long ReceiveItem_ID;
		private static List<HC_SP_GET_PERMISSION_Result> PermisionList;

        public ActionResult ReceivedDetail(long id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            this.Receive_ID = id;
            ReceivedIndexViewModel Received_Data = new ReceivedIndexViewModel();
            Received_Data = BindData();
            return View(Received_Data);
        }

		[HttpPost]
        public JsonResult ReceivedDetail(cReceive objReceive, long id)
        {
            try
            {
                
                //1 = "รอ HC รับเรื่อง 12 ชม"
                if (objReceive.Main_ProcessID == 1)
                {
                    objReceive.Receive_AppointmentDateFormateDate = string.IsNullOrEmpty(objReceive.Receive_AppointmentDate) ? null : cCommon.ddMMyyyyToyyyyMMdd(objReceive.Receive_AppointmentDate, '/');

                    //Save Receive Head ส่วนรับเรื่อง
                    updateReceive(objReceive);

                    //send Email รับเรื่อง
                    cb_Email objEmail = new cb_Email();
                    objEmail.SendEmail(objReceive.UnitID, id, (int)EnumConfiguration.EmailContentType.RecievedHC);

                    
                    //BindData
                    cb_ReceivedManagement obj1 = new cb_ReceivedManagement();
                    HC_SP_Get_TD_Receive_Result Data_Inform = obj1.getTD_Receive(objReceive.ReceiveID);
                    List<HC_SP_Get_TD_Receive_Item_Result> Data_Inform_Item_List = obj1.getTD_Receive_Item(objReceive.ReceiveID).ToList();
                    ReceivedIndexViewModel result = new ReceivedIndexViewModel();
                    result.Data_Inform = Data_Inform;
                    result.Data_Inform_Item_List = Data_Inform_Item_List;

                    // Call Service Noti HSA
                    if (objReceive.Receive_StatusID == 5)
                    {
                        string message = "[" + objReceive.ReceiveJobNoText + "] " + System.Configuration.ConfigurationManager.AppSettings["NotiMsg_Declined"];
                        cb_HomeServiceAPI HomeServiceAPI = new cb_HomeServiceAPI(objReceive.ProjectCode, objReceive.UnitAddress, objReceive.UnitCode, message);
                    }

                    //ยิงข้อมูลไปหา Octagon กรณีมีชื่อ HC ที่รับเรื่องได้แล้ว ให้ส่งรายชื่อเจ้าหน้าที่ไปอัพเดต
                    if (objReceive.Receive_StatusID == 2 || objReceive.Receive_StatusID == 3)
                    {
                        long ActivityID = objReceive.ReceiveID.GetValueOrDefault(0) + 900000000;
                        string Name_EN = UserDetail.Fullname;
                        string Name_TH = UserDetail.FullnameTH;
                        string OctagonURL = "http://192.9.0.169/sansiri/icontact_utils/u_staffhc.php?Activity_id=" + ActivityID + "&StaffNameEN=" + Name_EN + "&StaffNameTH=" + Name_TH + "";
                        System.Net.WebRequest req = System.Net.HttpWebRequest.Create(OctagonURL);
                        System.Net.WebResponse webResponse = req.GetResponse();

                    }

                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    using (HomeCareDBEntities HomeCareDB = new HomeCareDBEntities())
                    {

                        if (@EnumConfiguration.Role.CallCentre.ToString() == UserDetail.Role && objReceive.Receive_Confirm_date == null)
                        {
                            //Save Receive Head ส่วนรับเรื่อง
                            updateReceive(objReceive);

                            //send Email Call Centre ไม่ยืนยันการรับเรื่อง
                            if (objReceive.Receive_Confirm_Status_ID == 2)
                            {
                                cb_Email objEmail = new cb_Email();
                                objEmail.SendEmail(objReceive.UnitID, id, (int)EnumConfiguration.EmailContentType.ConfirmRecieve);
                            }
                        }
                        else
                        {
                            
                            //Save Inform Receive Item 
                            foreach (var m in objReceive.listReceiveItem)
                            {
                                string[] ReceiveItem = m.Split('|');
                                if (ReceiveItem.Count() > 0)
                                {
                                    string message = "";
                                    long StatusID = long.Parse(ReceiveItem[(int)ReceivedItem.itemStatusID]);
                                    long StatusOldID = long.Parse(ReceiveItem[(int)ReceivedItem.itemStatusIDOld]);
                                    long CancelResonID = long.Parse(ReceiveItem[(int)ReceivedItem.itemCancelResonID]);

                                    if (StatusID != StatusOldID)
                                    { 
                                        HomeCareDB.HC_SP_Update_TD_Receive_Item(long.Parse(ReceiveItem[(int)ReceivedItem.itemID]), ReceiveItem[(int)ReceivedItem.itemRemark].ToString()
                                            , StatusID, long.Parse(ReceiveItem[(int)ReceivedItem.itemRepairID]), CancelResonID, objReceive.User_Code);

										// ==== Don't Use ====
                                        //message = checkNotificationHSA(StatusID, StatusOldID, CancelResonID, objReceive.ReceiveJobNoText);
                                        //if (message != "")
                                        //{
                                        //    // Call Service Noti HSA
                                        //    cb_HomeServiceAPI HomeServiceAPI = new cb_HomeServiceAPI(objReceive.ProjectCode, objReceive.UnitAddress, objReceive.UnitCode, message);
                                        //}

                                    }

                                }
                            }
                        }

                        //update วันนัดตรวจจริง
                        HC_SP_Get_TD_Receive_Result Data_Inform = HomeCareDB.HC_SP_Get_TD_Receive(objReceive.ReceiveID).FirstOrDefault();
                        if (objReceive.Receive_Appointment_Date_Real != "" && Data_Inform.Receive_Appointment_Date_Real == "" && objReceive.Receive_StatusID == 2)
                        {
                            updateReceive(objReceive);
                        }

                        //BindData
                        HC_SP_Get_TD_Receive_Result Data_InformNew = HomeCareDB.HC_SP_Get_TD_Receive(objReceive.ReceiveID).FirstOrDefault();
                        List<HC_SP_Get_TD_Receive_Item_Result> Data_Inform_Item_List = HomeCareDB.HC_SP_Get_TD_Receive_Item(objReceive.ReceiveID).ToList();
                        ReceivedIndexViewModel result = new ReceivedIndexViewModel();
                        result.Data_Inform = Data_InformNew;
                        result.Data_Inform_Item_List = Data_Inform_Item_List;
                        
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }

                }


            }
            catch (Exception ex)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }

       


        [HttpPost]
        public JsonResult ReceivedLastItem(long ReceiveID)        {

            cb_ReceivedManagement receivedManagement = new cb_ReceivedManagement();
            HC_SP_Get_TD_Receive_Item_Result ReceiveItem = receivedManagement.getTD_Receive_Item(ReceiveID).LastOrDefault();
            return Json(ReceiveItem, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ReceivedDetailItem(long ReceiveItem_ID, long Receive_ID)
        {
            this.Receive_ID = Receive_ID;
            this.ReceiveItem_ID = ReceiveItem_ID;
            ReceivedIndexViewModel Received_Data = new ReceivedIndexViewModel();
            Received_Data = BindItemDetailData();
            return Json(Received_Data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getlistworksheetitemDetail(int workSheetitemID)
        {
            cb_ReceivedManagement obj = new cb_ReceivedManagement();
            HC_SP_Get_TD_Receive_Item_Modal_Result result = obj.getTD_Receive_Item_Modal(workSheetitemID);

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        private ReceivedIndexViewModel BindData()
        {

            HomeCareDBEntities HomeCareDB = new HomeCareDBEntities();
            ReceivedIndexViewModel Received_Data = new ReceivedIndexViewModel();

            cb_ReceivedManagement obj1 = new cb_ReceivedManagement();
			cAuthorization obj2 = new cAuthorization();
            HC_SP_Get_TD_Receive_Result Data_Inform = obj1.getTD_Receive(this.Receive_ID);
            List<HC_SP_Get_TD_Receive_Item_Result> Data_Inform_Item_List = obj1.getTD_Receive_Item(this.Receive_ID);

            // Customer Data
            var data = HomeCareDB.TK_SP_Get_Customer_Info_By_Unit(Data_Inform.UnitID).ToList();
            CustomerViewModel Data_Customer = new CustomerViewModel();
            Data_Customer.FullName = data.FirstOrDefault().FullNameTH_1;
            Data_Customer.FullTelephone = data.FirstOrDefault().Mobile_1 + "," + data.FirstOrDefault().Telephone_1;
            Data_Customer.ID = data.FirstOrDefault().ID_1;
            Data_Customer.VipStatus = HomeCareDB.TK_SP_Get_CheckProspectVipStatus(Data_Customer.ID).FirstOrDefault();

            // Unit Data
            UnitViewModel Data_Unit = new UnitViewModel();
            Data_Unit.PJ_ID = data.FirstOrDefault().Project_ID;
            Data_Unit.PJ_Name = data.FirstOrDefault().Project_NameTH;
            Data_Unit.PJ_Code = data.FirstOrDefault().Project_Code;
            Data_Unit.PJ_SSegment = data.FirstOrDefault().Project_SSegment;
            Data_Unit.Unit_ID = data.FirstOrDefault().Unit_ID;
            Data_Unit.Unit_Code = data.FirstOrDefault().Unit_Code;
            Data_Unit.Unit_Address = data.FirstOrDefault().Unit_Address;
            Data_Unit.Unit_Code_Address = data.FirstOrDefault().Unit_Code_Address;
            Data_Unit.Unit_Model = data.FirstOrDefault().Unit_Model;
            Data_Unit.Unit_GuaranteedYield = data.FirstOrDefault().Unit_GuaranteedYield;
            Data_Unit.Unit_Defer = data.FirstOrDefault().Unit_Defer;
            Data_Unit.StartGuaranteeDate = data.FirstOrDefault().StartGuaranteeDate;
            Data_Unit.EndGuaranteeDate = data.FirstOrDefault().EndGuaranteeDate;
            Data_Unit.ExtraGuaranteeDate = data.FirstOrDefault().ExtraGuaranteeDate;
			// Get Permission
			PermisionList = obj2.getPermission(UserDetail.UserID);

			// Get Master HC_Receive_Status
			DropDownListModel Data_DLL_Receive_Status = new DropDownListModel();
            Data_DLL_Receive_Status.Items = cls_hc_tm_lookup.Get_Master_DDL(Cls_HC_TM_Lookup_Type.HC_Receive_Status);

            // Get Master HC_Receive_Confirm_Status
            DropDownListModel Data_DLL_Receive_Confirm_Status = new DropDownListModel();
            Data_DLL_Receive_Confirm_Status.Items = cls_hc_tm_lookup.Get_Master_DDL(Cls_HC_TM_Lookup_Type.HC_Receive_Confirm_Status);

            // Get Master HC_Guarantee
            DropDownListModel Data_DLL_HC_Guarantee = new DropDownListModel();
            Data_DLL_HC_Guarantee.Items = cls_hc_tm_lookup.Get_Master_DDL(Cls_HC_TM_Lookup_Type.HC_Guarantee);

            // Get Master HC_Receive_Item_Status
            DropDownListModel Data_DLL_HC_Receive_Item_Status = new DropDownListModel();
            Data_DLL_HC_Receive_Item_Status.Items = cls_hc_tm_lookup.Get_Master_DDL(Cls_HC_TM_Lookup_Type.HC_Receive_Item_Status);

            // Get Master HC_Flg_Repair_Transfer
            DropDownListModel Data_DLL_HC_Flg_Repair_Transfer = new DropDownListModel();
            Data_DLL_HC_Flg_Repair_Transfer.Items = cls_hc_tm_lookup.Get_Master_DDL(Cls_HC_TM_Lookup_Type.HC_Flg_Repair_Transfer);

            // Get Master HC_Receive_Item_Cancel_Reason
            DropDownListModel Data_DLL_HC_Receive_Item_Cancel_Reason = new DropDownListModel();
            Data_DLL_HC_Receive_Item_Cancel_Reason.Items = cls_hc_tm_lookup.Get_Master_DDL(Cls_HC_TM_Lookup_Type.HC_Receive_Item_Cancel_Reason);

            // Get Master HC_OverAppointmentDate_Reason
            DropDownListModel Data_DLL_HC_OverAppointmentDate_Reason = new DropDownListModel();
            Data_DLL_HC_OverAppointmentDate_Reason.Items = cls_hc_tm_lookup.Get_Master_DDL(Cls_HC_TM_Lookup_Type.HC_OverAppointmentDate_Reason);

            Received_Data.Data_Customer = Data_Customer;
            Received_Data.Data_Unit = Data_Unit;
			Received_Data.Data_Inform = Data_Inform;
            Received_Data.Data_Inform_Item_List = Data_Inform_Item_List;
            Received_Data.Data_DLL_Receive_Status = Data_DLL_Receive_Status;
            Received_Data.Data_DLL_Receive_Confirm_Status = Data_DLL_Receive_Confirm_Status;
            Received_Data.Data_DLL_HC_Receive_Item_Status = Data_DLL_HC_Receive_Item_Status;
            Received_Data.Data_DLL_HC_Guarantee = Data_DLL_HC_Guarantee;
			Received_Data.Data_DLL_HC_Flg_Repair_Transfer = Data_DLL_HC_Flg_Repair_Transfer;
            Received_Data.Data_DLL_HC_Receive_Item_Cancel_Reason = Data_DLL_HC_Receive_Item_Cancel_Reason;
            Received_Data.Data_DLL_HC_OverAppointmentDate_Reason = Data_DLL_HC_OverAppointmentDate_Reason;

            return Received_Data;
        }

        private ReceivedIndexViewModel BindItemDetailData()
        {

            cb_ReceivedManagement obj1 = new cb_ReceivedManagement();
            HC_SP_Get_TD_Receive_Item_Modal_Result Data_Inform_Item = obj1.getTD_Receive_Item_Modal(this.ReceiveItem_ID);
            List<HC_SP_Get_TD_Receive_Item_Detail_Result> Data_Inform_Item_Detail_List = obj1.getTD_Receive_Item_Detail(this.ReceiveItem_ID);

            // Get Master HC_Cause 
            DropDownListModel Data_DLL_HC_Cause = new DropDownListModel();
            Data_DLL_HC_Cause.Items = cls_hc_tm_lookup.Get_Master_DDL(Cls_HC_TM_Lookup_Type.HC_Cause);

            // Get Master HC_Receive_Item_Detail_Status
            DropDownListModel Data_DLL_HC_Receive_Item_Detail_Status = new DropDownListModel();
            Data_DLL_HC_Receive_Item_Detail_Status.Items = cls_hc_tm_lookup.Get_Master_DDL(Cls_HC_TM_Lookup_Type.HC_Receive_Item_Detail_Status);

            // Get Master HC_Vendor_Price 
            DropDownListModel Data_DLL_HC_Vendor_Price = new DropDownListModel();
            Data_DLL_HC_Vendor_Price.Items = cls_hc_tm_lookup.Get_HC_List1();

            //Get HC_Spec1 Master
            cb_MasterData obj = new cb_MasterData();
            List<Dropdown> HCSpec1 = obj.getHCSpec1();

            // Get Master HC_OverRepairAppointmentDate_Reason
            DropDownListModel Data_DLL_HC_OverRepairAppointmentDate_Reason = new DropDownListModel();
            Data_DLL_HC_OverRepairAppointmentDate_Reason.Items = cls_hc_tm_lookup.Get_Master_DDL(Cls_HC_TM_Lookup_Type.HC_OverRepairAppointmentDate_Reason);

            // Get Master HC_JobType
            DropDownListModel Data_DLL_HC_JobType = new DropDownListModel();
            Data_DLL_HC_JobType.Items = cls_hc_tm_lookup.Get_Master_DDL(Cls_HC_TM_Lookup_Type.HC_JobType);


            ReceivedIndexViewModel Receive = new ReceivedIndexViewModel();
            Receive.Data_Inform_Item = Data_Inform_Item;
            Receive.Data_Inform_Item_Detail_List = Data_Inform_Item_Detail_List;
            Receive.Data_DLL_HC_Spec1 = HCSpec1;
            if (Receive.Data_Inform_Item.SpecL1_ID != null)
            {
                long SpecID = Receive.Data_Inform_Item.SpecL1_ID.Value;
                List<Dropdown> HCSpec2 = obj.getHCSpec2(SpecID);
                Receive.Data_DLL_HC_Spec2 = HCSpec2;

                //Get Location
                List<HC_SP_Get_TM_Location_Result> HCLocation = obj.getLocation(SpecID);
                Receive.Data_DLL_HC_Location = HCLocation;
            }
            if (Receive.Data_Inform_Item.SpecL2_ID != null)
            {
                long SpecID = Receive.Data_Inform_Item.SpecL2_ID.Value;
                List<Dropdown> HCSpec3 = obj.getHCSpec3(SpecID);
                Receive.Data_DLL_HC_Spec3 = HCSpec3;
            }

            Receive.Data_DLL_HC_Cause = Data_DLL_HC_Cause;
            Receive.Data_DLL_HC_Receive_Item_Detail_Status = Data_DLL_HC_Receive_Item_Detail_Status;
            Receive.Data_DLL_HC_Vendor_Price = Data_DLL_HC_Vendor_Price;
            Receive.Data_DLL_HC_OverRepairAppointmentDate_Reason = Data_DLL_HC_OverRepairAppointmentDate_Reason;
            Receive.Data_DLL_HC_JobType = Data_DLL_HC_JobType;

            return Receive;

        }

        [HttpPost]
        public bool updateReceive(cReceive objReceive)
        {

            cb_ReceivedManagement obj = new cb_ReceivedManagement();
            bool result = obj.updateTD_Receive(objReceive);

            return result;
        }
        [HttpPost]
        public bool InsertReceiveItem(cReceiveItem objReceiveItem)
        {

            cb_ReceivedManagement obj = new cb_ReceivedManagement();
            bool result = obj.InsertReceiveItem(objReceiveItem);

            

            return result;
        }

        [HttpPost]
        public bool updateReceiveItemModal(cReceive objReceive)
        {

            cb_ReceivedManagement obj = new cb_ReceivedManagement();
            bool result = obj.updateTD_Receive_Item_Modal(objReceive);

            return result;
        }
        [HttpPost]
        public bool updateReceiveItemDetail(cReceive objReceiveitem)
        {

            cb_ReceivedManagement obj = new cb_ReceivedManagement();
            bool result = obj.updateTD_Receive_Item_Detail(objReceiveitem);

            return result;
        }
        [HttpPost]
        public JsonResult PrepairWorkOrder(int Receive_ID)
        {

            cb_ReceivedManagement obj = new cb_ReceivedManagement();
            List<HC_SP_Get_TD_Receive_Item_Detail_Group_Result> list = obj.getlistWorkorder(Receive_ID);

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public string OpenWorkOrder(cReceive objReceive)
        {
            cb_ReceivedManagement obj = new cb_ReceivedManagement();
            string result = obj.OpenWorkOrder(objReceive);
            return result;
        }
        [HttpPost]
        public JsonResult GetReceivedAppointment(cReceive Received)
        {
            cb_ReceivedManagement obj = new cb_ReceivedManagement();
            List<HC_SP_Get_Receive_Appointment_Result> result = obj.GetReceivedAppointment(Received.ReceiveID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult InsertReceivedAppointment(cReceiveAppointment ReceivedAppointment)
        {
            bool result;
            try
            {
                cb_ReceivedManagement obj = new cb_ReceivedManagement();
                obj.InsertReceivedAppointment(ReceivedAppointment);


                if (ReceivedAppointment.ExtendedApproveFlag == true)
                {
                    cb_Email objEmail = new cb_Email();
                    objEmail.SendEmail(ReceivedAppointment.UnitID, ReceivedAppointment.ReceiveID.Value, (int)EnumConfiguration.EmailContentType.ConfirmedAppointment);

                }
                else if (ReceivedAppointment.ExtendedApproveFlag == false)
                {
                    cb_Email objEmail = new cb_Email();
                    objEmail.SendEmail(ReceivedAppointment.UnitID, ReceivedAppointment.ReceiveID.Value, (int)EnumConfiguration.EmailContentType.RejectAppointment);

                }
                else
                {
                    //Send Email
                    cb_Email objEmail = new cb_Email();
                    objEmail.SendEmail(ReceivedAppointment.UnitID, ReceivedAppointment.ReceiveID.Value, (int)EnumConfiguration.EmailContentType.ConfirmAppointment);
                }


                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }

            return Json(result, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult CheckReceivedAppointment(long Job_ID, string Job_Code)
        {
            cb_ReceivedManagement obj = new cb_ReceivedManagement();
            string result = obj.CheckReceivedAppointment(Job_ID, Job_Code);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CheckReceivedAppointmentRepairAfter(long Job_ID, string Job_Code)
        {
            cb_ReceivedManagement obj = new cb_ReceivedManagement();
            string result = obj.CheckReceivedAppointmentRepairAfter(Job_ID, Job_Code);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetReceivedAppointmentHistory(long ID )
        {
            cb_ReceivedManagement obj = new cb_ReceivedManagement();
            List<HC_SP_Get_TD_Extended_Appointment_History_Result> result = obj.GetReceivedAppointmentHistory(ID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult checkDuplicateFix(long? unitID, long? ddlGuaranteeID, long? ddlSpec1ID, long? ddlSpec2ID, long? ddlSpec3ID, long? ddlLocationID)
        {
            cb_ReceivedManagement obj = new cb_ReceivedManagement();
            string result = obj.checkDuplicateFix(unitID, ddlGuaranteeID, ddlSpec1ID, ddlSpec2ID, ddlSpec3ID, ddlLocationID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

		[HttpPost]
		public JsonResult UpdateConfigRealDate(long? Receive_ID,string Remark, DateTime Date)
		{
			cb_ReceivedManagement obj = new cb_ReceivedManagement();
			bool result = obj.UpdateConfigRealDate(Receive_ID, Date, Remark, UserDetail.CodeName);
			return Json(result, JsonRequestBehavior.AllowGet);
		}
		[HttpPost]
		public JsonResult updateConfigReceiveItemModal(cReceive objReceive)
		{
			cb_ReceivedManagement obj = new cb_ReceivedManagement();
			bool result = obj.update_Config_TD_Receive_Item_Modal(objReceive);
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

		private string checkNotificationHSA(long StatusID, long StatusOldID, long CancelResonID, string JobNoText)
        {
            string message = "";

            if (StatusID == StatusOldID)
            {
                message = "";
            }
            else if (StatusID == 2 && StatusOldID != 2)
            {
                message = "[" + JobNoText + "] " + System.Configuration.ConfigurationManager.AppSettings["NotiMsg_Accepted"];
            }
            else if (StatusID == 3 && StatusOldID != 3)
            {
                message = "[" + JobNoText + "] " + System.Configuration.ConfigurationManager.AppSettings["NotiMsg_Declined"];
            }
            //else if (StatusID == 4 && StatusOldID != 4)
            //{
            //    if (CancelResonID == 9)
            //    {
            //        message = System.Configuration.ConfigurationManager.AppSettings["NotiMsg_PendingWarrantyJob"];
            //    }
            //    else if (CancelResonID == 10)
            //    {
            //        message = System.Configuration.ConfigurationManager.AppSettings["NotiMsg_PendingF5"];
            //    }
            //}
                return message;
        }

		public ActionResult ReceivedRepair()
		{
			return View();
		}

        public ActionResult ReceivedHomeCard()
        {
            return View();
        }
    }
}