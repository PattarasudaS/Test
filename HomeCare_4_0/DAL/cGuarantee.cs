using HomeCare_4_0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCare_4_0.DAL
{
    public class cGuarantee
    {
        public string[] listItem { get; set; }
        //public string UnitID{ get; set; }
        //public string ExtendDay { get; set; }
        //public string ExtendMonth { get; set; }
        //public string ExtendYear { get; set; }
        //public string User { get; set; }
    }

    public class GuaranteeDetail
    {

        public int UnitID { get; set; }
        public int GuaranteeDay { get; set; }
        public int GuaranteeMonth { get; set; }
        public int GuaranteeYear { get; set; }
        public string GuaranteeRemark { get; set; }
        public string User { get; set; } 
    }
    

}