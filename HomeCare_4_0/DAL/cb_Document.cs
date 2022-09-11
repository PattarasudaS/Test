using HomeCare_4_0.Common;
using HomeCare_4_0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCare_4_0.DAL
{
    public class cb_Document
    {
        private HomeCareDBEntities context;
        public cb_Document()
        {
            this.context = new HomeCareDBEntities();
        }
        public List<HC_SP_Get_DocumentByFormType_Result> getlistDocumentByFormType(string WorkSheet_IDList)
        {
            return context.HC_SP_Get_DocumentByFormType(WorkSheet_IDList).ToList();
        }

        public void DocumentByWorkSheetPrint(long WorkSheet_ID, string FormType, string FileName, long VendorID = 0, string VendorName = null)
        {
            context.HC_SP_Insert_DocumentByWorkSheet(Convert.ToInt64(WorkSheet_ID), FormType, FileName, UserDetail.CodeName, VendorID, VendorName);
        }

        public HC_SP_Get_Quotation_Header_Result GetQuotationHeader(long WorkSheet_ID)
        {
            HC_SP_Get_Quotation_Header_Result result = context.HC_SP_Get_Quotation_Header(WorkSheet_ID).FirstOrDefault();
            return result;
        }
    }
}