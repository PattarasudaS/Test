//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HomeCare_4_0.Models
{
    using System;
    
    public partial class HC_SP_Get_TD_Receive_Item_Detail_Result
    {
        public long TD_Receive_Item_Detail_ID { get; set; }
        public Nullable<long> TD_Receive_Item_ID { get; set; }
        public Nullable<int> No { get; set; }
        public Nullable<int> Status_ID { get; set; }
        public Nullable<long> Vendor_ID { get; set; }
        public string Vendor_Name { get; set; }
        public Nullable<int> JobTypeID { get; set; }
        public string RepairAppointmentDateFrom { get; set; }
        public string RepairAppointmentDateTo { get; set; }
        public Nullable<int> RepairAppointmentDateReasonID { get; set; }
        public Nullable<long> Price_ID { get; set; }
        public string Receive_Order_Date { get; set; }
        public Nullable<bool> Worksheet_Create_Flag { get; set; }
        public string Remark { get; set; }
        public string Create_By { get; set; }
        public Nullable<System.DateTime> Create_Date { get; set; }
        public string Update_By { get; set; }
        public Nullable<System.DateTime> Update_Date { get; set; }
    }
}
