using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HomeCare_4_0.DAL;

namespace HomeCare_4_0.Models
{
    public class ReceivedIndexViewModel
    {
        public UnitViewModel Data_Unit { get; set; }
        public CustomerViewModel Data_Customer { get; set; }
        public ReceiveHeadlViewModel Data_Receive { get; set; }
        public HC_SP_Get_TD_Receive_Result Data_Inform { get; set; }
        public List<HC_SP_Get_TD_Receive_Item_Result> Data_Inform_Item_List { get; set; }
        public HC_SP_Get_TD_Receive_Item_Modal_Result Data_Inform_Item { get; set; }
        public List<HC_SP_Get_TD_Receive_Item_Detail_Result> Data_Inform_Item_Detail_List { get; set; }
        public DropDownListModel Data_DLL_Receive_Status { get; set; }
        public DropDownListModel Data_DLL_Receive_Confirm_Status { get; set; }
        public DropDownListModel Data_DLL_HC_Receive_Item_Status { get; set; }
        public DropDownListModel Data_DLL_HC_Guarantee { get; set; }
        public DropDownListModel Data_DLL_HC_Flg_Repair_Transfer { get; set; }
        public DropDownListModel Data_DLL_HC_Receive_Item_Cancel_Reason { get; set; }

        public DropDownListModel Data_DLL_HC_OverAppointmentDate_Reason { get; set; }
        public DropDownListModel Data_DLL_HC_Defect { get; set; }
        
        public DropDownListModel Data_DLL_HC_Cause { get; set; }
        public DropDownListModel Data_DLL_HC_Receive_Item_Detail_Status { get; set; }
        public DropDownListModel Data_DLL_HC_Vendor_Price { get; set; }
        public List<Dropdown> Data_DLL_HC_Spec1 { get;  set; }
        public List<Dropdown> Data_DLL_HC_Spec2 { get; set; }
        public List<Dropdown> Data_DLL_HC_Spec3 { get; set; }
        public List<HC_SP_Get_TM_Location_Result> Data_DLL_HC_Location { get; set; }
        public DropDownListModel Data_DLL_HC_OverRepairAppointmentDate_Reason { get; set; }
        public DropDownListModel Data_DLL_HC_JobType { get; set; }

    }

    public class ReceiveHeadlViewModel
    {
        public long? Inform_Receive_Head_ID { get; set; }
        public long? Receive_Status_ID { get; set; }
        public string Receive_Status_Name { get; set; }
        public string Receive_HC_Date { get; set; }
        public string Receive_HC_Remark { get; set; }
        public long? Receive_HC_User_ID { get; set; }
        public string Receive_HC_User { get; set; }
        public string Receive_CC_Date { get; set; }
        public string Receive_CC_Remark { get; set; }
        public long? Receive_CC_User_ID { get; set; }
        public string Receive_CC_User { get; set; }
        public string Receive_Appointment_Date { get; set; }
        public System.TimeSpan? Receive_Appointment_Time_From { get; set; }
        public System.TimeSpan? Receive_Appointment_Time_To { get; set; }
        public bool? Receive_FalgAgent { get; set; }
        public bool? Receive_FlagChina { get; set; }
    }

   
}