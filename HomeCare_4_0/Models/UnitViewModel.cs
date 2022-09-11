using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCare_4_0.Models
{
    public class AJAX_UnitViewModel
    {
        public long Unit_ID { get; set; }
        public string Unit_Code { get; set; }
        public string Unit_Address { get; set; }
    }

    public class UnitViewModel
    {
        public long? PJ_ID { get; set; }
        public string PJ_Code { get; set; }
        public string PJ_Name { get; set; }
        public int PJ_SSegment { get; set; }
        public long Unit_ID { get; set; }
        public string Unit_Code { get; set; }
        public string Unit_Code_Address { get; set; }
        public string Unit_Address { get; set; }
        public string Unit_Model { get; set; }
        public int Unit_GuaranteedYield { get; set; }
        public int Unit_Defer { get; set; }
        public string StartGuaranteeDate { get; set; }
        public string EndGuaranteeDate { get; set; }
        public string ExtraGuaranteeDate { get; set; }
    }

    public class AJAX_UnitResultViewModel
    {
        public List<AJAX_UnitViewModel> data { get; set; }
    }
}