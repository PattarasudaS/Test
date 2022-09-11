using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeCare_4_0.Models
{
    public class GuaranteeViewModel
    {
        //public CriteriaMaser objCriteriaMaser { get; set; }      
        public long? ProjectID { get; set; }
        public DropDownListModel Data_DLL_HC_Unit { get; set; }

        public DropDownListModel ExtendDay { get; set; }
        public DropDownListModel ExtendMonth { get; set; }
        public DropDownListModel ExtendYear { get; set; }
        public UnitViewModel Data_Unit { get; set; }
        public CustomerViewModel Data_Customer { get; set; }
        public InformHeadlViewModel Data_Inform { get; set; }
        public HC_SP_Get_TD_Guarantee_Result Data_GuaranteeDetail { get; set; }
        public List<GViewModel> Data_Inform_Item_List { get; set; }



    }

    public class GViewModel
    {
        public long UnitID { get; set; }
        public string InformItem_Detail { get; set; }
        public string InformItem_Remark { get; set; }
        
    }





}