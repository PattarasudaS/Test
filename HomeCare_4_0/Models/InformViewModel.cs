using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeCare_4_0.Models
{
    //private List<InformItemViewModel> _Data_Inform_Item_List = new List<InformItemViewModel>();
    public class InformIndexViewModel
    {
        public UnitViewModel Data_Unit { get; set; }
        public CustomerViewModel Data_Customer { get; set; }
        public InformHeadlViewModel Data_Inform { get; set; }
        public List<InformItemViewModel> Data_Inform_Item_List { get; set; }
        public DropDownListModel Data_DLL_HC_Guarantee { get; set; }
        public DropDownListModel Data_DLL_HC_Person_Inform_Status { get; set; }
        public DropDownListModel Data_DLL_HC_OverAppointmentDate_Reason { get; set; }

    }

    public class InformHeadlViewModel
    {
        public string Inform_JobNoText { get; set; }
        public string Main_Process_Name { get; set; }
        public long? Main_ProcessID { get; set; }
        public string Inform_Date { get; set; }
        public string Inform_User { get; set; }
        public long? Inform_User_ID { get; set; }
        
 
        public string Inform_Remark { get; set; }
        public long? Inform_CustomerType_ID { get; set; }
        public string Inform_CustomerType_Name { get; set; }
        public string Inform_CustomerName { get; set; }
        public string Inform_CustomerTel { get; set; }
        public string Receive_Appointment_Date { get; set; }
        public Nullable<System.TimeSpan> Receive_Appointment_Time_From { get; set; }
        public Nullable<System.TimeSpan> Receive_Appointment_Time_To { get; set; }

    }


    public class InformItemViewModel
    {
        public long InformItem_ID { get; set; }
        public int? InformItem_No { get; set; }
        public long? InformItem_Status_ID { get; set; }
        public long? InformItem_Guarantee_ID { get; set; }
        public string InformItem_Guarantee_Name { get; set; }
        public string InformItem_Detail { get; set; }
        public string InformItem_Remark { get; set; }
        public string ReceivedItem_Remark { get; set; }
        public long? ReceivedItem_Repair_ID { get; set; }
        public long? ReceivedItem_Cancel_Reason_ID { get; set; }
        public long? ReceivedItem_SpecL1 { get; set; }
        public long? ReceivedItem_SpecL2 { get; set; }
        public long? ReceivedItem_SpecL3 { get; set; }
        public long? ReceivedItem_Location_ID { get; set; }
        public long? ReceivedItem_Cause_ID { get; set; }
        public string ReceivedItem_Cause_text { get; set; }
        public long? ReceivedItem_Priority_ID { get; set; }
        public string ReceivedItem_Priority_Name { get; set; }
    }

    
}