using HomeCare_4_0.DAL;
using HomeCare_4_0.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeCare_4_0.Controllers
{
    public class FileuploadController : BaseController
    {
        // GET: Fileupload

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult uploadAttachmentFile(long ReferenceID, string ReferenceType, int AttachmentTypeID, string AttachmentDetail, string User,long ReferenceItemID)
        {
            long? reponse=0;
            try
            {
                string Folder = Server.MapPath("~/"+ System.Configuration.ConfigurationManager.AppSettings["UploadFile"]);

                cb_FileUpload objFileUpload = new cb_FileUpload();

                //string Folder = "D:\\WebApp\\HomeCare_4_0\\HomeCare_4_0\\" +System.Configuration.ConfigurationManager.AppSettings["UploadFile"];
                HttpPostedFileBase file = Request.Files[0];

                string folderName = objFileUpload.getAttachType().FirstOrDefault(o => o.ID == AttachmentTypeID).DescriptionEN.ToString();
                string attachmentPath = folderName; // folderName.Replace(" ", "_");

                string FolderPath = Path.Combine(Folder, ReferenceType, ReferenceID.ToString(), attachmentPath);

                if (!Directory.Exists(FolderPath))
                {
                    Directory.CreateDirectory(FolderPath);
                }

                file.SaveAs(FolderPath + "/" + file.FileName.Replace(" ",string.Empty).Replace(" ",string.Empty));

                string savePath = Path.Combine(ReferenceType, ReferenceID.ToString(), attachmentPath) + "\\" + file.FileName.Replace(" ",string.Empty);



                reponse=objFileUpload.insertAttachFile(ReferenceID, ReferenceType, AttachmentTypeID, AttachmentDetail, savePath, User, ReferenceItemID);


            }
            catch(Exception ex)
            {

            }
            return Json(reponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getAttachmentFile(long ReferenceID, string ReferenceType ,long? Attachment_Type_ID,long Reference_Item_ID)
        {
            List<HC_SP_Get_TD_Attachment_Result> list = new List<HC_SP_Get_TD_Attachment_Result>();
            try
            {
                cb_FileUpload objFileUpload = new cb_FileUpload();
                list= objFileUpload.getAttachFile(ReferenceID, ReferenceType, Attachment_Type_ID, Reference_Item_ID);

            }
            catch (Exception ex)
            {
                
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult getAttachmentFileReceive(long ReceiveItemID)
        {
            List<HC_SP_Get_TD_Attachment_Receive_Result> list = new List<HC_SP_Get_TD_Attachment_Receive_Result>();
            try
            {
                cb_FileUpload objFileUpload = new cb_FileUpload();
                list = objFileUpload.getAttachFileReceive(ReceiveItemID);

            }
            catch (Exception ex)
            {

            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult updateItemID(long ReferenceID, long ReferenceitemID, string Type)
        {
            bool result;
            try
            {
                cb_FileUpload objFileUpload = new cb_FileUpload();
                result = objFileUpload.updateItemID(ReferenceID, ReferenceitemID, Type);
 
            }
            catch (Exception ex)
            {
                result = false;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateAttachment(long AttachmentID, string User)
        {
            bool result;
            try
            {
                cb_FileUpload objFileUpload = new cb_FileUpload();
                result = objFileUpload.UpdateAttachment(AttachmentID, User);

            }
            catch (Exception ex)
            {
                result = false;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateAttachmentDetail(long AttachmentID,string AttachmentDetail, string User)
        {
            bool result;
            try
            {
                cb_FileUpload objFileUpload = new cb_FileUpload();
                result = objFileUpload.UpdateAttachmentDetail(AttachmentID, AttachmentDetail, User);

            }
            catch (Exception ex)
            {
                result = false;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}