using HomeCare_4_0.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCare_4_0.Models
{
    public class DocumentViewModel
    {
        public DocumentSearchViewModel DocumentSearch { get; set; }
        public List<DocumentByWorkSheetResultViewModel> DocumentByWorkSheetResultList { get; set; }
        public CriteriaMaser objCriteriaMaser { get; set; }
        public DropDownListModel Data_DLL_HC_FormType { get; set; }

        public DocumentF3SearchViewModel DocumentF3Search { get; set; }
        public List<DocumentByUnitIdResultViewModel> DocumentByUnitIdResultList { get; set; }
    }


    public class DocumentSearchViewModel
    {
        public long ProjectID { get; set; }
        public long UnitID { get; set; }
        public string UnitAddress { get; set; }
        public string WorkSheet_DateFrom { get; set; }
        public string WorkSheet_DateTo { get; set; }
        public string WorkSheetIDList { get; set; }

    }

    public class DocumentByWorkSheetResultViewModel
    {
        public bool Check { get; set; }
        public string ProjectName { get; set; }
        public string UnitCode { get; set; }
        public string UnitAddress { get; set; }
        public long WorkSheet_ID { get; set; }
        public string WorkSheet_JobNoText { get; set; }
        public string WorkSheet_Date { get; set; }
        public string WorkSheet_Status { get; set; }
        public string Document_F2Date { get; set; }
        public string Document_F2By { get; set; }
        public string Document_F9Date { get; set; }
        public string Document_F9By { get; set; }
        public string Document_F3Date { get; set; }
        public string Document_F3By { get; set; }
    }

    public class DocumentF3SearchViewModel
    {
        public long ProjectID { get; set; }
        public long UnitID { get; set; }
        public string UnitAddress { get; set; }
        public string TransferDateFrom { get; set; }
        public string TransferDateTo { get; set; }
        public string UnitIdList { get; set; }

    }

    public class DocumentByUnitIdResultViewModel
    {
        public bool Check { get; set; }
        public string ProjectName { get; set; }
        public long UnitId { get; set; }
        public string UnitCode { get; set; }
        public string UnitAddress { get; set; }
        public string ProspectName { get; set; }
        public string TransferDate { get; set; }
        public string PrintF3Date { get; set; }
        public string PrintF3By { get; set; }
    }

}