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
    
    public partial class HC_SP_Get_TD_Guarantee_Result
    {
        public string DOCUMENT_NO { get; set; }
        public string PROJECT_CODE { get; set; }
        public string PROJECT_NAME { get; set; }
        public string HOUSE_OWNER { get; set; }
        public string HOUSE_NO { get; set; }
        public string PLOT { get; set; }
        public Nullable<System.DateTime> TRANSFORM_OWER_CONTRACT_DATE { get; set; }
        public Nullable<System.DateTime> TRANSFORM_OWER_ACTUAL_DATE { get; set; }
        public Nullable<System.DateTime> HOUSE_RECEIVE_DATE { get; set; }
        public Nullable<System.DateTime> START_WARANTY_DATE { get; set; }
        public string TIME_EXTEND { get; set; }
        public Nullable<System.DateTime> TIME_EXTEND_FROM { get; set; }
        public Nullable<System.DateTime> TIME_EXTEND_TO { get; set; }
        public long WORKFLOW_NO { get; set; }
        public string WORKFLOW_STATUS { get; set; }
        public string USER_ROLE { get; set; }
    }
}
