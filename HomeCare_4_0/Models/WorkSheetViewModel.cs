using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeCare_4_0.Models
{
    public class WorkSheetIndexViewModel
    {
        public UnitViewModel Data_Unit { get; set; }
        public CustomerViewModel Data_Customer { get; set; }
        public InformHeadlViewModel Data_Inform { get; set; }
        public ReceiveHeadlViewModel Data_Receive { get; set; }

        public WorkSheetHeadlViewModel Data_WorkSheet { get; set; }

        public List<WorkSheetitemlViewModel> Data_WorkSheetItem { get; set; }

        public List<HC_SP_Get_TD_Attachment_Result> Data_WorkSheetAttachment { get; set; }

        public List<HC_SP_Get_DocumentFByWorkSheet_Result> Data_DocumentF { get; set; }
    }
    public class WorkSheetHeadlViewModel:HC_SP_Get_TD_WorkSheet_Result
    {
      
    }
    public class WorkSheetitemlViewModel : HC_SP_Get_TD_WorkSheet_Item_Result
    {

    }
    

}