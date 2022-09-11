using HomeCare_4_0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCare_4_0.DAL
{
   
    public class cb_FileUpload
    {
        private HomeCareDBEntities context;

        public cb_FileUpload()
        {
            this.context = new HomeCareDBEntities();
        }

        public List<HC_TM_Attachment> getAttachType()
        {
            return context.HC_TM_Attachment.Where(o => o.Active == true).ToList();
        }
        
        public long? insertAttachFile(long ReferenceID, string ReferenceType, int AttachmentTypeID, string AttachmentDetail, string Attachment_URL, string User,long WorksheetitemID)
        {
            return context.HC_SP_Insert_TD_Attachment(ReferenceID,ReferenceType,AttachmentTypeID, AttachmentDetail, Attachment_URL,string.Empty,string.Empty,User, WorksheetitemID).FirstOrDefault();
        }

        public List<HC_SP_Get_TD_Attachment_Result> getAttachFile(long? Reference_ID,string Reference_Type,long? Attachment_Type_ID,long Reference_Item_ID)
        {
            return context.HC_SP_Get_TD_Attachment(Reference_ID, Reference_Type, Attachment_Type_ID, Reference_Item_ID).ToList();
        }

        public List<HC_SP_Get_TD_Attachment_Receive_Result> getAttachFileReceive(long? ReceiveItemID)
        {
            return context.HC_SP_Get_TD_Attachment_Receive(ReceiveItemID).ToList();
        }


        public bool updateItemID(long referenceID, long referenceitemID, string type)
        {
            try
            {
                context.HC_SP_Update_TD_AttachmentItemID(referenceID, referenceitemID, type);
                return true;
            }catch
            {
                return false;
            }
            
        }

        public bool UpdateAttachment(long AttachmentID, string User)
        {
            try
            {
                context.HC_SP_Update_TD_Attachment(AttachmentID, User);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateAttachmentDetail(long AttachmentID, string AttachmentDetail, string User)
        {
            try
            {
                context.HC_SP_Update_TD_AttachmentDetail(AttachmentID, AttachmentDetail, User);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}