using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeCare_4_0.ClassLib;
using HomeCare_4_0.Models;
using HomeCare_4_0.DAL;
using HomeCare_4_0.Common;
/// <summary>
/// Controller สำหรับ ใบแจ้งซ่อม
/// </summary>
namespace HomeCare_4_0.Controllers
{
    public class InformController : BaseController
    {
        private Cls_HC_TM_Lookup cls_hc_tm_lookup = new Cls_HC_TM_Lookup();
        private long? UnitID;


        private long User_ID;

        public enum Role
        {
            CallCentre,
        }


        public ActionResult InformAddDetail(long id)
        {
            //ViewBag.Title = "Add Inform";
            try
            {
                UnitID = id;

                if (UnitID != null)
                {
                    HomeCareDBEntities HomeCareDB = new HomeCareDBEntities();
                    InformIndexViewModel Inform_data = new InformIndexViewModel();
                    
                    // Customer Data
                    var data = HomeCareDB.TK_SP_Get_Customer_Info_By_Unit(UnitID).ToList();
                    CustomerViewModel Data_Customer = new CustomerViewModel();
                    Data_Customer.ID = data.FirstOrDefault().ID_1;
                    Data_Customer.FullName = data.FirstOrDefault().FullNameTH_1;
                    Data_Customer.FullTelephone = data.FirstOrDefault().Mobile_1 + " " + data.FirstOrDefault().Telephone_1;
                    Data_Customer.VipStatus = HomeCareDB.TK_SP_Get_CheckProspectVipStatus(data.FirstOrDefault().ID_1).FirstOrDefault();

                    // Unit Data
                    UnitViewModel Data_Unit = new UnitViewModel();
                    Data_Unit.PJ_ID = data.FirstOrDefault().Project_ID;
                    Data_Unit.PJ_Code = data.FirstOrDefault().Project_Code;
                    Data_Unit.PJ_Name = data.FirstOrDefault().Project_NameTH;
                    Data_Unit.PJ_SSegment = data.FirstOrDefault().Project_SSegment;
                    Data_Unit.Unit_ID = data.FirstOrDefault().Unit_ID;
                    Data_Unit.Unit_Code = data.FirstOrDefault().Unit_Code;
                    Data_Unit.Unit_Address = data.FirstOrDefault().Unit_Address;
                    Data_Unit.Unit_Code_Address = data.FirstOrDefault().Unit_Code_Address;
                    Data_Unit.Unit_Model = data.FirstOrDefault().Unit_Model;
                    Data_Unit.StartGuaranteeDate = data.FirstOrDefault().StartGuaranteeDate;
                    Data_Unit.EndGuaranteeDate = data.FirstOrDefault().EndGuaranteeDate;
                    Data_Unit.ExtraGuaranteeDate = data.FirstOrDefault().ExtraGuaranteeDate;
                    
                    List<InformItemViewModel> Data_Inform_Item_List = new List<InformItemViewModel>();
                    for (var i = 0; i < 15; i++)
                    {
                        InformItemViewModel Data_Inform_Item_ = new InformItemViewModel();
                        Data_Inform_Item_.InformItem_Guarantee_ID = 0;
                        Data_Inform_Item_.InformItem_Detail = "";
                        Data_Inform_Item_List.Add(Data_Inform_Item_);
                    }

                    // Get Master HC_Person_Inform_Status
                    DropDownListModel Data_DLL_HC_Person_Inform_Status = new DropDownListModel();
                    Data_DLL_HC_Person_Inform_Status.Items = cls_hc_tm_lookup.Get_Master_DDL(Cls_HC_TM_Lookup_Type.HC_Person_Inform_Status);

                    // Get Master HC_Guarantee
                    DropDownListModel Data_DLL_HC_Guarantee = new DropDownListModel();
                    Data_DLL_HC_Guarantee.Items = cls_hc_tm_lookup.Get_Master_DDL(Cls_HC_TM_Lookup_Type.HC_Guarantee);

                    // Get Master HC_OverAppointmentDate_Reason
                    DropDownListModel Data_DLL_HC_OverAppointmentDate_Reason = new DropDownListModel();
                    Data_DLL_HC_OverAppointmentDate_Reason.Items = cls_hc_tm_lookup.Get_Master_DDL(Cls_HC_TM_Lookup_Type.HC_OverAppointmentDate_Reason);

                    Inform_data.Data_Customer = Data_Customer;
                    Inform_data.Data_Unit = Data_Unit;
                    Inform_data.Data_Inform_Item_List = Data_Inform_Item_List;
                    Inform_data.Data_DLL_HC_Guarantee = Data_DLL_HC_Guarantee;
                    Inform_data.Data_DLL_HC_Person_Inform_Status = Data_DLL_HC_Person_Inform_Status;
                    Inform_data.Data_DLL_HC_OverAppointmentDate_Reason = Data_DLL_HC_OverAppointmentDate_Reason;

                    return View(Inform_data);
                }
                else
                {
                    // เซ็ต Error                                
                    ErrorViewModel Error = new ErrorViewModel();
                    Error.code = Cls_HC_Constance.HC_Error_404;
                    Error.message_1 = Cls_HC_Constance.HC_Error_404_Message_TH;
                    Error.HandleError = new HandleErrorInfo(new Exception(Cls_HC_Constance.HC_Error_404_Message_EN), "Inform", "Index"); ;
                    return View("Error", Error);
                }

            }
            catch (Exception ex)
            {
                // เซ็ต Error                
                ErrorViewModel Error = new ErrorViewModel();
                Error.code = Cls_HC_Constance.HC_Error_500;
                Error.HandleError = new HandleErrorInfo(ex, "Inform", "Index");
                return View("Error", Error);
            }
        }

        public ActionResult InformSave(int projectID,string unitCode)
        {
            cInform obj = new cInform();
            obj.ProjectID= projectID;
            obj.UnitCode = unitCode; 
            return View(obj);
        }

        // popup ที่แสดงสถานะเบื้องต้น
        public ActionResult InformListByID()
        {
            return View();
        }

        public ActionResult InformListByID_1()
        {
            return View();
        }

		#region FUNCTION INFORM CONTROLLER
		private void SetPermissions()
        {
            User_ID = UserDetail.UserID;

            ViewBag.Show_Menu = Cls_HC_Constance.HC_Hidden_Main_Menu;
            ViewBag.Show_Sub_Menu = Cls_HC_Constance.HC_Show_Sub_Menu;
            ViewBag.Show_Sub_Menu_HomeCare = Cls_HC_Constance.HC_Hidden_Sub_Menu_HomeCare;
            ViewBag.Show_Sub_Menu_Callcenter = Cls_HC_Constance.HC_Show_Sub_Menu_Callcenter;

        }

        #endregion

        [HttpPost]
        public JsonResult insertInform(cInform objInform)
        {

            objInform.Receive_AppointmentDateFormateDate = string.IsNullOrEmpty(objInform.Receive_AppointmentDate) ? null : cCommon.ddMMyyyyToyyyyMMdd(objInform.Receive_AppointmentDate, '/');
            long ref_TD_Inform_Receive_Head_ID = 0;

            try
            {
                // Check Unit Code Not Null
                if (objInform.UnitID != null)
                {

                    using (HomeCareDBEntities HomeCareDB = new HomeCareDBEntities())
                    {
                        // Create JobNo
                        System.Data.Entity.Core.Objects.ObjectParameter TD_JobNo_ID = new System.Data.Entity.Core.Objects.ObjectParameter("TD_JobNo_ID", typeof(long));  // Define Parameter เพื่อรอรับค่า กลับ
                        int resJobCode = HomeCareDB.HC_SP_Insert_TD_JobNo(objInform.ProjectID, objInform.ProjectCode, objInform.UnitID, Cls_HC_Constance.HC_Job_Code_Stage_Inform_Receive, UserDetail.CodeName, TD_JobNo_ID);

                        // Create Receive
                        System.Data.Entity.Core.Objects.ObjectParameter TD_Receive_ID = new System.Data.Entity.Core.Objects.ObjectParameter("TD_Receive_ID", typeof(long));  // Define Parameter เพื่อรอรับค่า กลับ
                        int resInformHetestcallcentread = HomeCareDB.HC_SP_Insert_TD_Receive(Convert.ToInt64(TD_JobNo_ID.Value), objInform.ProjectID, objInform.ProjectCode, objInform.UnitID
                            , objInform.UnitCode, objInform.CustomerID
                            , string.Empty,string.Empty, string.Empty, string.Empty, string.Empty,string.Empty,string.Empty,string.Empty
                            , objInform.Remark, objInform.CustomerTypeID, objInform.CustomerName, objInform.CustomerTel,string.Empty
                            , objInform.Receive_AppointmentDateFormateDate, objInform.Receive_AppointmentTime_From, objInform.Receive_AppointmentTime_To
                            , objInform.ddlOverAPDate_ReasonID
                            , UserDetail.CodeName, UserDetail.UserID,objInform.flagAgent,objInform.flagChina, false, UserDetail.Icontact_userID,UserDetail.UserID
                            , TD_Receive_ID);
                        ref_TD_Inform_Receive_Head_ID = Convert.ToInt64(TD_Receive_ID.Value);

                        //Create Receive Item   
                        int itemNo = 0;
                        if (objInform.listItem != null && objInform.listItem.Count() > 0)
                        {
                            string[] itemDetail;
                            int GuaranteeID;
                            string Detail;
                            string Remark;
                            foreach (var m in objInform.listItem)
                            {
                                itemDetail = m.Split('|');
                                GuaranteeID = int.Parse(itemDetail[0]);
                                Detail = itemDetail[1].ToString();
                                Remark = itemDetail[2].ToString();

                                if (GuaranteeID != 0)
                                {
                                    System.Data.Entity.Core.Objects.ObjectParameter TD_Receive_Item_ID = new System.Data.Entity.Core.Objects.ObjectParameter("TD_Receive_Item_ID", typeof(Int64));  //

                                    itemNo = itemNo + 1;
                                    int resInformDetail = HomeCareDB.HC_SP_Insert_TD_Receive_Item(ref_TD_Inform_Receive_Head_ID, itemNo,
                                    GuaranteeID, Detail, Remark, null, null, EnumConfiguration.ReceiveStatusID.NotOperation.GetHashCode(),true, UserDetail.CodeName,string.Empty,string.Empty, TD_Receive_Item_ID);
                                }

                            }
                        }

                        //Send Email
                        cb_Email objEmail = new cb_Email();
                        objEmail.SendEmail(objInform.UnitID.Value, ref_TD_Inform_Receive_Head_ID, (int)EnumConfiguration.EmailContentType.Inform);

                        //Create Activity
                        HomeCareDB.HC_SP_Insert_TD_Activity("Q", objInform.CustomerID, objInform.ProjectID, UserDetail.CodeName, ref_TD_Inform_Receive_Head_ID);

                    }
                    //Create Activity
                    //TrackingSystemEntities contextTracking = new TrackingSystemEntities();
                    //contextTracking.TK_SP_Insert_activity_For_Homecare("Q", objInform.CustomerID, objInform.ProjectID, UserDetail.CodeName, ref_TD_Inform_Receive_Head_ID);
                }
                return Json(true, JsonRequestBehavior.AllowGet); 
            }
            catch(Exception ex)
            {
                return Json(true, JsonRequestBehavior.AllowGet); 
            }



        }

		public ActionResult InformRepair()
		{
			return View();
		}

        public ActionResult InformYearly()
        {
            return View();
        }
    }
}



