using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCare_4_0.ClassLib
{
    public static class EnumConfiguration
    {

        public enum Reference_Type
        {
            Receive,
            Receive_Item,
            Receive_Detail,
            WorkSheet,
            WorkSheet_item,
            WorkSheet_Detail,
        }
        public enum enumAttachmentType
        {
            Picture,
            HCF,
            Memo,
            Vendor
        }

        public enum TypeEmail
        {
            TO,
            CC
        }
        public enum EmailActionType
        {
            SendApprove = 1,
            Approved = 2,
            NotApproved = 3
        }

        public enum ApproveStatus
        {
            INIT,
            APPV,
            REJT
        }

        public enum MainProcessStatus
        {
            ReadyForApproved = 10,
            WaitingForApproved = 11
        }
        public enum AttachFileRefType
        {
            R,
            W
        }
        public enum Role
        {
            CallCentre,
            VENDOR,
            SUPPORT_HC,
            AdminHC,
            PMR,
            ADMIN,
            HC,
            SM
        }
        public enum EmailContentType
        {
            Inform=0,
            RecievedHC=1,
            PrintF=2 ,
            ConfirmRecieve=3,
            ConfirmAppointment = 4,
            ConfirmedAppointment = 5,
            RejectAppointment = 6
        }

        public enum ResponseCode
        {
            Success = 201,
            Unsuccess = 400,
            NotFound = 404
        }

        public enum NotiMessage
        {
            Accepted = 201,
            Unsuccess = 400,
            NotFound = 404
        }
        public enum ReceiveStatusID
        {
            NotOperation = 1,
            Operation = 2,
        }
        public enum PriceType
        {
            Std = 1,
            Extra = 2,
        }

        public enum ApproveType
        {
            WORK_ORDER,
            INSURANCE_EXTEND
        }
    }
}