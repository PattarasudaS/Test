using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCare_4_0.DAL
{
    public class cWorksheet
    {
        public long? WorkSheet_ID { get; set; }
        public string CloseJob_Date { get; set; }
        public string RepairAppointmentFromDate { get; set; }
        public string RepairAppointmentToDate { get; set; }

        public int Vendor_ID { get; set; }
        public string Vendor_Name { get; set; }

        public double Operating_Percent { get; set; }
        public double Vat_Percent { get; set; }

        public string PaymentType { get; set; }
        public int PaymentType_ID { get; set; }
        public string User { get; set; }
        public string Unit_Code { get; set; }
        public string UnitAddress { get; set; }
        public long Unit_ID { get; set; }
        public string ProjectCode { get; set; }

        public int ApproveType { get; set; }

        public string ReceiveJobNoText { get; set; }

        //vendor change Remark
        public string VendorChangeRemark { get; set; }
        public string ApprovedRemark { get; set; }
		public string CancelRemark { get; set; }
        // Add Retention flag
        public bool RetentionFlag { get; set; }
        public decimal NetPrice { get; set; }
    }
    public class cWorksheetitemDetail
    {
        public long? WorkSheet_ID { get; set; }
        public int WorkSheet_Item_Detail_ID { get; set; }
        public int TD_WorkSheet_Item_ID { get; set; }
        public int Order { get; set; }
        public int List1_ID { get; set; }
        public int List2_ID { get; set; }
        public int List3_ID { get; set; }
        public int List4_ID { get; set; }

        public decimal Special_Materia { get; set; }
        public decimal Special_WagePrice { get; set; }

        public int PriceMaterialType { get; set; }
        public int PriceWageType { get; set; }
        public decimal Quantity { get; set; }
        public string Remark { get; set; }
        public string CreateBy { get; set; }

        public string UpdateBy { get; set; }

    }

    public class cWorksheetitem : cWorksheet
    {
        public string Cause_Text { get; set; }
        public string Confirm_Price_Date { get; set; }
        public string FinishDate { get; set; }
        public string StartDate { get; set; }
        public int? Status { get; set; }
        public long WorkSheet_Item_ID { get; set; }
    }
    public class cWorksheetApprove : cWorksheet
    {
        public string ApproveVal { get; set; }

        public string Role { get; set; }

        public string Remark { get; set; }
    }

    public class cRepairAppointment
    {
        public long? Worksheet_Item_ID { get; set; }

        public string ExtRepairAppointmentFormDate { get; set; }
        public string ExtRepairAppointmentToDate { get; set; }
        public int ExtendedResonID { get; set; }
        public string ExtendedReasonRemark { get; set; }
        public string ExtendedReasonRemarkCC { get; set; }
        public bool? ExtendedApproveFlag { get; set; }

        public string UserCode { get; set; }
        public long? WorkSheetID { get; set; }
    }

    public class cRepairAfterAppointment
    {
        public long? Worksheet_Item_ID { get; set; }

        public int RepairAppointmentTimes { get; set; }
        public string RepairAppointmentDate_Old { get; set; }
        public string RepairAppointmentDate { get; set; }
        public string ExpectDate_Old { get; set; }
        public string ExpectDate { get; set; }
        public int ExtendedReasonID { get; set; }
        public string ExtendedRemark { get; set; }
        public long? ExtendedUserID { get; set; }
        public string ExtendedUserCode { get; set; }
        public bool? ExtendedApproveFlag { get; set; }

        public string ExtendedRemarkCC { get; set; }
        public long? ExtendedUserIDCC { get; set; }
        public string ExtendedUserCodeCC { get; set; }

        public string CreatedBy { get; set; }
        

    }
}