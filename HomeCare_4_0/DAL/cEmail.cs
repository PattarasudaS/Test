using HomeCare_4_0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCare_4_0.DAL
{
    public class cEmail
    {
        public string ProjectCode { get; set; }
        public string Role { get; set; }
        public string TypeEmail { get; set; }
        public int TypeBotton { get; set; }

        public HC_SP_Get_TD_Receive_Result receiveDetail { get; set; }



    }

    public class cEmailApprove : cEmail
    {
        public HC_SP_Get_TD_WorkSheet_Result worksheet { get; set; }
        public List<HC_SP_Get_TD_Approve_Item_Detail_Result> listWorksheetDetail { get; set; }
        public HC_SP_Get_TD_Approve_Result approve { get; internal set; }

        public TK_SP_Get_Customer_Info_By_Unit_Result customerInfo { get; internal set; }
       

        public bool flagApp { get; set; }
        public bool flagFinal { get; set; }
    }

    public class cEmailWorkSheet : cEmail
    {
        public HC_SP_Get_TD_WorkSheet_Result worksheet { get; set; }
        public List<HC_SP_Get_TD_WorkSheet_Item_Result> worksheetItem { get; set; }
        public TK_SP_Get_Customer_Info_By_Unit_Result customerInfo { get; internal set; }

    }

    public class cEmailResponsible : cEmail
    {
        public long TD_Approve_ID { get; set; }


    }
    public class cEmailContent : cEmail
    {
        public TK_SP_Get_Address_Result customerContract { get; set; }
        public List<HC_SP_Get_TD_Receive_Item_Result> receiveItem { get; set; }
        public string Type { get; set; }
        public TK_SP_Get_Customer_Info_By_Unit_Result unitInfo { get; set; }
        public long? VendorID { get; set; }

        public bool FlagChina { get; set; }
    }
    public class cEmailSupport : cEmail
    {
        public List<HC_SP_Get_DocumentByFormType_Result> DocumentItem { get; set; }
        public string FormType { get; set; }
        public string VendorNameForF2 { get; set; }
        public List<F2AttachedFiles> AttachedFiles { get; set; }
        public class F2AttachedFiles
        {
            public string WorkSheetName { get; set; }
            public string FilePath { get; set; }
        }
    }
}