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
    
    public partial class HC_SP_Get_TD_WorkSheet_Result
    {
        public long WorkSheet_ID { get; set; }
        public string WorkSheet_JobNoText { get; set; }
        public string WorkSheet_Status { get; set; }
        public string WorkSheet_StatusID { get; set; }
        public string Approve_Status { get; set; }
        public string Approve_StatusID { get; set; }
        public int WorkSheet_CloseJobFlag { get; set; }
        public string WorkSheet_CloseJobDate { get; set; }
        public string WorkSheet_CreateBy { get; set; }
        public string WorkSheet_Create_Date { get; set; }
        public long VendorID { get; set; }
        public string VendorName { get; set; }
        public string WorkSheet_RepairAppointmentFromDate { get; set; }
        public string WorkSheet_RepairAppointmentToDate { get; set; }
        public double TotalPrice { get; set; }
        public double NetPrice { get; set; }
        public double OperatingPercent { get; set; }
        public double VatPercent { get; set; }
        public long Project_ID { get; set; }
        public string Project_Code { get; set; }
        public long Unit_ID { get; set; }
        public string Unit_Code { get; set; }
        public long Prospect_ID { get; set; }
        public double retention_percent { get; set; }
    }
}
