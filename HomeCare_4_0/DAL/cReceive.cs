using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCare_4_0.DAL
{
    public class cReceive
    {
        public int Main_ProcessID { get; set; }
        public string ReceiveJobNoText { get; set; }


        public string[] listReceiveItem { get; set; }

        public int SpecL1ID { get; set; }
        public int SpecL2ID { get; set; }
        public int SpecL3ID { get; set; }

        public int LocationID { get; set; }
        public int CauseID { get; set; }
        public string CauseText { get; set; }
        public bool DuplicateFixid { get; set; }
        public int HeaderID { get; set; }

        public int ID { get; set; }


        public int ReceiveItemID { get; set; }
        public int No { get; set; }
        public int StatusID { get; set; }
        public int StatusIDOld { get; set; }
        public int VendorID { get; set; }
        public string VendorName { get; set; }

        public int? JobTypeID { get; set; }
        public string RepairAppointmentDateFrom { get; set; }
        public string RepairAppointmentDateTo { get; set; }
        public int? ReasonID { get; set; }

        public int PriceID { get; set; }
        public bool ReceiveOrderFlag { get; set; }
        public string ReceiveOrderDate { get; set; }
        public string Remark { get; set; }


        public long ProspectID { get; set; }
        public long UnitID { get; set; }
        public string UnitCode { get; set; }
        public string UnitAddress { get; set; }
        public long ProjectID { get; set; }
        public string ProjectCode { get; set; }
        public string User_Code { get; set; }
        public long? User_ID { get; set; }



        public long? ReceiveID { get; set; }
        public long? Receive_StatusID { get; set; }
        public string Receive_HCRemark { get; set; }
        public string Receive_CCRemark { get; set; }



        public long? Receive_Confirm_Status_ID { get; set; }
        public string Receive_Confirm_Remark { get; set; }
        public string Receive_Confirm_User { get; set; }
        public string Receive_Confirm_date { get; set; }


        public string Receive_AppointmentDate { get; set; }
        public DateTime? Receive_AppointmentDateFormateDate { get; set; }
        public TimeSpan? Receive_AppointmentTime_From { get; set; }
        public TimeSpan? Receive_AppointmentTime_To { get; set; }
        public int? ddlOverAPDate_ReasonID { get; set; }
        public string Receive_Appointment_Date_Real { get; set; }
    }

    public class cReceiveItem
    { 
        public int ReceivedItemStatusID { get; set; }
        public int GuaranteeID { get; set; }
        public string Remark { get; set; }
        public string Detail { get; set; }

        public int RepairID { get; set; }
        public int CancelResonID { get; set; }
        public long? ItemNo { get; set; }
        public long? ReceiveID { get; set; }
        public string User { get; set; }
    }

    public class cReceiveAppointment
    {
        public long? ReceiveID { get; set; }

        public string ExtendedReceiveAppointmentDate { get; set; }
        public TimeSpan ExtendedReceiveAppointmentTimeForm { get; set; }
        public TimeSpan ExtendedReceiveAppointmentTimeTo { get; set; }
        public int ExtendedResonID { get; set; }
        public string ExtendedReasonRemark { get; set; }
        public string ExtendedReasonRemarkCC { get; set; }
        public bool? ExtendedApproveFlag { get; set; }

        public string UserCode { get; set; }
        public long UnitID { get; set; }
    }
    
}

