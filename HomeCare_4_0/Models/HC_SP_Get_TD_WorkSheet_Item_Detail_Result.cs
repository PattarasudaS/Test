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
    
    public partial class HC_SP_Get_TD_WorkSheet_Item_Detail_Result
    {
        public long ID { get; set; }
        public Nullable<long> TD_WorkSheet_Item_ID { get; set; }
        public Nullable<int> Order { get; set; }
        public string Listname1 { get; set; }
        public string Listname2 { get; set; }
        public string Listname3 { get; set; }
        public string Listname4 { get; set; }
        public decimal MainMateria { get; set; }
        public decimal MainWagePrice { get; set; }
        public Nullable<decimal> Special_Materia { get; set; }
        public Nullable<decimal> Special_WagePrice { get; set; }
        public Nullable<int> PriceMaterialType { get; set; }
        public Nullable<int> PriceWageType { get; set; }
        public string Unit { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public string Remark { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
    }
}