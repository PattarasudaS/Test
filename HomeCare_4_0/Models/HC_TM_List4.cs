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
    using System.Collections.Generic;
    
    public partial class HC_TM_List4
    {
        public int id { get; set; }
        public string name { get; set; }
        public Nullable<int> tm_ListL3_id { get; set; }
        public Nullable<decimal> GoodsPrice { get; set; }
        public Nullable<decimal> WagePerUnit { get; set; }
        public string Unit { get; set; }
        public string PriceType { get; set; }
        public string Remark { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<System.DateTime> createdDate { get; set; }
        public string createdBy { get; set; }
        public Nullable<System.DateTime> updatedDate { get; set; }
        public string updatedBy { get; set; }
    }
}