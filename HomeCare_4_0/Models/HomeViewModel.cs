using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeCare_4_0.Models
{
    public class HomeViewModel
    {
        public long HC_Main_Process_1 { get; set; }
        public long HC_Main_Process_2 { get; set; }
        public long HC_Main_Process_3 { get; set; }
        public long HC_Main_Process_4 { get; set; }
        public long HC_Main_Process_5 { get; set; }
        public long HC_Main_Process_6 { get; set; }
        public long HC_Main_Process_7 { get; set; }
        public long HC_Main_Process_8 { get; set; }
        public long HC_Main_Process_9 { get; set; }
        public long HC_Main_Process_10 { get; set; }
        public long HC_Main_Process_11 { get; set; }
        public long HC_Main_Process_12 { get; set; }

        public CriteriaMaser objCriteriaMaser { get; set; }
        public CustomerViewModel Data_Customer { get; set; }
        public UnitViewModel Data_Unit { get; set; }
        public long ProjectID { get; set; }
       
        public long UnitID { get; set; }
        public int HC_Main_Process { get; set; }
    }
    public class CriteriaMaser {
        public List<SelectListItem> projectMaster { get; set; }
        public List<SelectListItem> unitMaster { get; set; }
    }

    public class RequestModel
    {
        public long ID { get; set; }
    }
    public class RequestUnit
    {
        public long UnitID { get; set; }
    }
    public class Project_List_Model
    {
        public long PJ_ID { get; set; }
        public string PJ_Name { get; set; }
        public string PJ_Code { get; set; }
    }
}