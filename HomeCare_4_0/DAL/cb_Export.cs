using GemBox.Spreadsheet;
using HomeCare_4_0.Common;
using HomeCare_4_0.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;

namespace HomeCare_4_0.DAL
{
    public class cb_Export
    {
        private HomeCareDBEntities context;
        private cb_Document objDocument = new cb_Document();
        public cb_Export()
        {
            this.context = new HomeCareDBEntities();
        }

        public List<string> ExportF1Document(List<HC_SP_Get_DocumentByFormType_Result> listDocType, int databaseSave = 1)
        {
            string excelTemplate = "";
            string Folder = "";
            string FileName = "";
            string worksheetname = "";
            string extenstion = ".xlsx";
            //string DocumentServer = "";
            string PathPdf = "";

            FileName = System.Configuration.ConfigurationManager.AppSettings["FileF1"];
            worksheetname = System.Configuration.ConfigurationManager.AppSettings["WorksheetF1"];
            Folder = System.Configuration.ConfigurationManager.AppSettings["DocumentF1"];
            //DocumentServer = System.Configuration.ConfigurationManager.AppSettings["DocumentServer"];
            
            excelTemplate = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/" + Folder + "/"), FileName + extenstion);

            List<string> listFileName = new List<string>();
            foreach (var header in listDocType)
            {
                FileInfo templateFile = new FileInfo(excelTemplate);
                ExcelPackage excel = new ExcelPackage(templateFile);
                var workSheet = excel.Workbook.Worksheets[worksheetname];
                var templateSheet = excel.Workbook.Worksheets[worksheetname];
                templateSheet.SelectedRange[1, 1, 65, 75].Copy(workSheet.SelectedRange[1, 1, 65, 75]);

                //ProjectName
                workSheet.Cells["D6"].Value = header.ProjectName;
                //WorkSheetJobNo
                workSheet.Cells["P6"].Value = header.WorkSheetJobNo;
                //WorkSheetCreateDate
                workSheet.Cells["P7"].Value = header.WorkSheetCreateDate;
                //WorkSheetCreateBy
                workSheet.Cells["E9"].Value = header.WorkSheetCreateBy;
                //WorkSheetTel
                workSheet.Cells["I9"].Value = header.WorkSheetTel;
                //ReceiveCreateDate
                workSheet.Cells["O9"].Value = header.ReceiveCreateDate;

                //CustomerName
                workSheet.Cells["E10"].Value = header.CustomerName;
                //UnitAddress
                workSheet.Cells["O11"].Value = header.UnitAddress;
                //UnitCode
                workSheet.Cells["O10"].Value = header.UnitCode;

                //CustomerTelephone
                workSheet.Cells["E11"].Value = header.CustomerTelephone;
                //UnitModel
                workSheet.Cells["I11"].Value = header.UnitModel;

                //EndGuaranteeDate
                workSheet.Cells["N14"].Value = header.EndGuaranteeDate;
                if (header.flgEndGuaranteeDate == 1)
                {
                    workSheet.Cells["K13"].Value = "X";
                }
                else
                {
                    workSheet.Cells["G13"].Value = "X";
                }

                List<HC_SP_Get_V_Receive_Item_Result> listItem = getReceiveItem(header.WorkSheetID);

                int itemindex = 19;
                int itemend = 19;
                workSheet.InsertRow(20, listItem.Count - 1, 19);
                foreach (var item in listItem)
                {
                    workSheet.Cells["C" + itemend].Value = item.ReceiveItem_No;
                    workSheet.Cells["D" + itemend].Value = item.ReceiveItem_Guarantee;
                    workSheet.Cells["G" + itemend].Value = item.ReceiveItem_Detail;
                    workSheet.Cells["K" + itemend].Value = string.Empty;
                    workSheet.Cells["P" + itemend].Value = item.ReceiveItem_status;

                    workSheet.Cells["D" + itemend + ":F" + itemend].Merge = true;
                    workSheet.Cells["G" + itemend + ":J" + itemend].Merge = true;
                    workSheet.Cells["K" + itemend + ":O" + itemend].Merge = true;
                    workSheet.Cells["D" + itemend].Style.WrapText = true;
                    workSheet.Cells["G" + itemend].Style.WrapText = true;
                    workSheet.Cells["K" + itemend].Style.WrapText = true;

                    itemend++;
                }
                itemend--;
                workSheet.Cells["C" + itemindex + ":P" + itemend].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells["C" + itemindex + ":P" + itemend].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells["C" + itemindex + ":P" + itemend].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells["C" + itemindex + ":P" + itemend].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                List<HC_SP_Get_V_Receive_Item_Detail_Result> listItemDetail = getReceiveItemDetail(header.WorkSheetID);
                int itemDetailindex = itemend + 3;
                int itemDetailend = itemDetailindex;
                workSheet.InsertRow(itemDetailindex + 1, listItemDetail.Count - 1, itemDetailindex);

                foreach (var itemDetail in listItemDetail)
                {
                    workSheet.Cells["C" + itemDetailend].Value = itemDetail.No;
                    workSheet.Cells["E" + itemDetailend].Value = itemDetail.ReceiveItem_SpecL3;
                    workSheet.Cells["K" + itemDetailend].Value = itemDetail.WorkSheet_StartDate;
                    workSheet.Cells["P" + itemDetailend].Value = itemDetail.WorkSheet_FinishDate;

                    workSheet.Cells["C" + itemDetailend + ":D" + itemDetailend].Merge = true;
                    workSheet.Cells["E" + itemDetailend + ":J" + itemDetailend].Merge = true;
                    workSheet.Cells["K" + itemDetailend + ":O" + itemDetailend].Merge = true;
                    workSheet.Cells["P" + itemDetailend + ":P" + itemDetailend].Merge = true;
                    itemDetailend++;
                }
                itemDetailend--;
                workSheet.Cells["C" + itemDetailindex + ":P" + itemDetailend].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells["C" + itemDetailindex + ":P" + itemDetailend].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells["C" + itemDetailindex + ":P" + itemDetailend].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells["C" + itemDetailindex + ":P" + itemDetailend].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                int vendorindex = itemDetailend + 4;
                workSheet.Cells["E" + vendorindex].Value = listItemDetail.FirstOrDefault().WorkSheet_VendorName;

                int jobnoindex = vendorindex + 4;
                workSheet.Cells["G" + jobnoindex].Value = header.WorkSheetJobNo + " ปัจจุบันส่วนงาน HC ได้ดำเนินการแล้วเสร็จเป็นที่เรียบร้อย";

                PathPdf = saveFile(FileName, header.WorkSheetID, extenstion, Folder, excel, databaseSave);
                listFileName.Add(PathPdf);

                Thread.Sleep(1000);
            }
            return listFileName;
        }

        public List<string> ExportF2Document(List<HC_SP_Get_DocumentByFormType_Result> listDocType, string VendorName, string VendorAddress, long VendorID = 0, int databaseSave = 1)
        {
            string excelTemplate = "";
            string Folder = "";
            string FileName = "";
            string worksheetname = "";
            string extenstion = ".xlsx";
            string DocumentSignature = "";
            string PathPdf = "";

            FileName = System.Configuration.ConfigurationManager.AppSettings["FileF2"];
            worksheetname = System.Configuration.ConfigurationManager.AppSettings["WorksheetF2"];
            Folder = System.Configuration.ConfigurationManager.AppSettings["DocumentF2"];
            DocumentSignature = System.Configuration.ConfigurationManager.AppSettings["DocumentSignature"];


            excelTemplate = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/" + Folder + "/"), FileName + extenstion);

            List<string> listFileName = new List<string>();
            foreach (var header in listDocType)
            {

                FileInfo templateFile = new FileInfo(excelTemplate);
                ExcelPackage excel = new ExcelPackage(templateFile);
                var workSheet = excel.Workbook.Worksheets[worksheetname];
                var templateSheet = excel.Workbook.Worksheets[worksheetname];
                templateSheet.SelectedRange[1, 1, 65, 75].Copy(workSheet.SelectedRange[1, 1, 65, 75]);


                //WorkSheetJobNo
                workSheet.Cells["D8"].Value = header.WorkSheetJobNo;
                //F2FirstPrintDate
                workSheet.Cells["K10"].Value = header.F2FirstPrintDate;
                //Vendor_Name
                workSheet.Cells["D15"].Value = VendorName;
                //Vendor_Address
                workSheet.Cells["B16"].Value = VendorAddress;
                //WorkSheetJobNo
                workSheet.Cells["J18"].Value = header.WorkSheetJobNo;
                //WorkSheetCreateDate
                workSheet.Cells["N18"].Value = header.WorkSheetCreateDate;

                //UnitCode
                workSheet.Cells["O20"].Value = header.UnitCode;
                //UnitAddress 
                workSheet.Cells["C21"].Value = header.UnitAddress;
                //CustomerName 
                workSheet.Cells["G21"].Value = header.CustomerName;
                //ItemNum
                workSheet.Cells["B22"].Value = header.ItemNum;
                //F2FirstPrintAdd7Daydate
                workSheet.Cells["L22"].Value = header.F2FirstPrintAdd7Daydate;

                //WorkSheetTel
                workSheet.Cells["H25"].Value = header.WorkSheetTel;

                //WorkSheetCreateBy
                workSheet.Cells["J32"].Value = header.WorkSheetCreateBy;
                //Signature
                string logoPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/" + DocumentSignature + "/"), UserDetail.CodeName + ".png");
                if (File.Exists(logoPath))
                {
                    Image logo = Image.FromFile(logoPath);
                    var Picture = workSheet.Drawings.AddPicture("TestByPicture", logo);
                    Picture.SetSize(200, 100);
                    Picture.SetPosition(27, 0, 10, 0);
                }

                PathPdf = saveFile(FileName, header.WorkSheetID, extenstion, Folder, excel, databaseSave, VendorID, VendorName);
                listFileName.Add(PathPdf);

                Thread.Sleep(1000);
            }
            return listFileName;
        }

        public List<string> ExportF3Document(List<HC_SP_API_GET_DOCUMENT_F3_Result> listDocType, int databaseSave = 1)
        {
            string excelTemplate = "";
            string Folder = "";
            string FileName = "";
            string worksheetname = "";
            string extenstion = ".xlsx";
            //string DocumentServer = "";
            string DocumentSignature = "";
            string PathPdf = "";


            FileName = System.Configuration.ConfigurationManager.AppSettings["FileF3"];
            worksheetname = System.Configuration.ConfigurationManager.AppSettings["WorksheetF3"];
            Folder = System.Configuration.ConfigurationManager.AppSettings["DocumentF3"];
            DocumentSignature = System.Configuration.ConfigurationManager.AppSettings["DocumentSignature"];

            excelTemplate = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/" + Folder + "/"), FileName + extenstion);

            List<string> listFileName = new List<string>();
            foreach (var header in listDocType)
            {

                FileInfo templateFile = new FileInfo(excelTemplate);
                ExcelPackage excel = new ExcelPackage(templateFile);
                var workSheet = excel.Workbook.Worksheets[worksheetname];
                var templateSheet = excel.Workbook.Worksheets[worksheetname];
                templateSheet.SelectedRange[1, 1, 65, 75].Copy(workSheet.SelectedRange[1, 1, 65, 75]);


                //WorkSheetJobNo
                workSheet.Cells["B9"].Value = header.F3ExportHeader;
                //Todaydate
                workSheet.Cells["K11"].Value = header.Todaydate; 
                //UnitAddress 
                workSheet.Cells["C16"].Value = header.UnitAddress;
                //UnitCode
                workSheet.Cells["F16"].Value = header.UnitCode;
                //ProjectName
                workSheet.Cells["C17"].Value = header.ProjectName;
                //ProjectName
                workSheet.Cells["B20"].Value = header.ProjectName;
                //DueDate
                workSheet.Cells["C23"].Value = header.EndGaranteeDate;
                //DueDate
                workSheet.Cells["G25"].Value = header.EndGaranteeDate;
                //F3SignName
                workSheet.Cells["D38"].Value = header.F3SignName;
                //F3Position
                workSheet.Cells["D39"].Value = header.F3Position;
                //Signature
                string logoPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/" + DocumentSignature + "/"), header.F3SignatureName + ".png");
                
                if (File.Exists(logoPath))
                {
                    Image logo = Image.FromFile(logoPath);
                    var Picture = workSheet.Drawings.AddPicture("TestByPicture", logo);
                    Picture.SetSize(200, 100);
                    Picture.SetPosition(33, 0, 5, 0);
                }

                PathPdf = saveFile(FileName, header.UnitId, extenstion, Folder, excel, databaseSave);
                listFileName.Add(PathPdf);

                Thread.Sleep(1000);
            }
            return listFileName;
        }

        public List<string> ExportF3ENDocument(List<HC_SP_API_GET_DOCUMENT_F3_Result> listDocType, int databaseSave = 1)
        {
            string excelTemplate = "";
            string Folder = "";
            string FileName = "";
            string worksheetname = "";
            string extenstion = ".xlsx";
            //string DocumentServer = "";
            string DocumentSignature = "";
            string PathPdf = "";


            FileName = System.Configuration.ConfigurationManager.AppSettings["FileF3EN"];
            worksheetname = System.Configuration.ConfigurationManager.AppSettings["WorksheetF3"];
            Folder = System.Configuration.ConfigurationManager.AppSettings["DocumentF3"];
            //DocumentServer = System.Configuration.ConfigurationManager.AppSettings["DocumentServer"];
            DocumentSignature = System.Configuration.ConfigurationManager.AppSettings["DocumentSignature"];

            excelTemplate = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/" + Folder + "/"), FileName + extenstion);

            List<string> listFileName = new List<string>();
            foreach (var header in listDocType)
            {

                FileInfo templateFile = new FileInfo(excelTemplate);
                ExcelPackage excel = new ExcelPackage(templateFile);
                var workSheet = excel.Workbook.Worksheets[worksheetname];
                var templateSheet = excel.Workbook.Worksheets[worksheetname];
                templateSheet.SelectedRange[1, 1, 65, 75].Copy(workSheet.SelectedRange[1, 1, 65, 75]);


                //WorkSheetJobNo
                workSheet.Cells["B9"].Value = header.F3ExportHeader;
                //Todaydate
                workSheet.Cells["K11"].Value = header.TodaydateEng;
                //UnitAddress 
                workSheet.Cells["D14"].Value = header.UnitAddress;
                //UnitCode
                workSheet.Cells["H14"].Value = header.UnitCode;
                //ProjectName
                workSheet.Cells["D15"].Value = header.ProjectNameEng;
                //ProjectName
                workSheet.Cells["A20"].Value = header.ProjectNameEng;
                //DueDate
                workSheet.Cells["D22"].Value = header.EndGuaranteeDateEN;
                //DueDate
                workSheet.Cells["H24"].Value = header.EndGuaranteeDateEN;
                //F3SignName
                workSheet.Cells["D35"].Value = header.F3SignNameEn;
                //F3Position
                workSheet.Cells["D36"].Value = header.F3PositionEn;
                //Signature
                string logoPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/" + DocumentSignature + "/"), header.F3SignatureName + ".png");
                if (File.Exists(logoPath))
                {
                    Image logo = Image.FromFile(logoPath);
                    var Picture = workSheet.Drawings.AddPicture("TestByPicture", logo);
                    Picture.SetSize(200, 100);
                    Picture.SetPosition(30, 0, 5, 0);
                }

                PathPdf = saveFile(FileName, header.UnitId, extenstion, Folder, excel, databaseSave);
                listFileName.Add(PathPdf);

                Thread.Sleep(1000);
            }
            return listFileName;
        }
        public List<string> ExportF5Document(List<HC_SP_Get_DocumentByFormType_Result> listDocType, int databaseSave = 1)
        {
            string excelTemplate = "";
            string Folder = "";
            string FileName = "";
            string worksheetname = "";
            string extenstion = ".xlsx";
            //string DocumentServer = "";
            string DocumentSignature = "";
            string PathPdf = "";

            FileName = System.Configuration.ConfigurationManager.AppSettings["FileF5"];
            worksheetname = System.Configuration.ConfigurationManager.AppSettings["WorksheetF5"];
            Folder = System.Configuration.ConfigurationManager.AppSettings["DocumentF5"];
            //DocumentServer = System.Configuration.ConfigurationManager.AppSettings["DocumentServer"];
            DocumentSignature = System.Configuration.ConfigurationManager.AppSettings["DocumentSignature"];
            excelTemplate = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/" + Folder + "/"), FileName + extenstion);

            List<string> listFileName = new List<string>();
            foreach (var header in listDocType)
            {

                FileInfo templateFile = new FileInfo(excelTemplate);
                ExcelPackage excel = new ExcelPackage(templateFile);
                var workSheet = excel.Workbook.Worksheets[worksheetname];
                var templateSheet = excel.Workbook.Worksheets[worksheetname];
                templateSheet.SelectedRange[1, 1, 65, 75].Copy(workSheet.SelectedRange[1, 1, 65, 75]);


                //WorkSheetJobNo
                workSheet.Cells["C10"].Value = header.WorkSheetJobNo;
                //Todaydate
                workSheet.Cells["G11"].Value = header.Todaydate;

                //UnitAddress 
                workSheet.Cells["C16"].Value = header.UnitAddress;
                //UnitCode
                workSheet.Cells["F16"].Value = header.UnitCode;
                //ProjectName
                workSheet.Cells["C17"].Value = header.ProjectName;

                //ReceiveCreateDate
                workSheet.Cells["I19"].Value = header.ReceiveCreateDate;
                //WorkSheetTel
                workSheet.Cells["G24"].Value = header.WorkSheetTel;
                //WorkSheetJobNo
                workSheet.Cells["D26"].Value = header.WorkSheetJobNo;
                //WorkSheetCreateBy
                workSheet.Cells["E38"].Value = header.WorkSheetCreateBy;
                //Signature
                string logoPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/" + DocumentSignature + "/"), UserDetail.CodeName + ".png");
                if (File.Exists(logoPath))
                {
                    Image logo = Image.FromFile(logoPath);
                    var Picture = workSheet.Drawings.AddPicture("TestByPicture", logo);
                    Picture.SetSize(200, 100);
                    Picture.SetPosition(33, 0, 5, 0);
                }

                PathPdf = saveFile(FileName, header.WorkSheetID, extenstion, Folder, excel, databaseSave);
                listFileName.Add(PathPdf);

                //string exportFileName = FileName + DateTime.Now.ToString("yyyyMMdd_hhmmss") + extenstion;

                //excel.SaveAs(new FileInfo(Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/" + Folder + "/"), exportFileName)));
                //string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/" + Folder + "/"), exportFileName);

                //SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
                //ExcelFile.Load(path).Save(path.Replace(".xlsx", ".pdf"));
                //excel.Dispose();

                //listFileName.Add(DocumentServer + exportFileName.Replace(".xlsx", ".pdf"));

                ////Insert log
                //objDocument.DocumentByWorkSheetPrint(header.WorkSheetID, "F05", DocumentServer + exportFileName.Replace(".xlsx", ".pdf"));

                Thread.Sleep(1000);
            }
            return listFileName;
        }

        public List<string> ExportF9Document(List<HC_SP_Get_DocumentByFormType_Result> listDocType, int databaseSave = 1)
        {
            string excelTemplate = "";
            string Folder = "";
            string FileName = "";
            string worksheetname = "";
            string extenstion = ".xlsx";
            //string DocumentServer = "";
            string DocumentSignature = "";
            string PathPdf = "";


            FileName = System.Configuration.ConfigurationManager.AppSettings["FileF9"];
            worksheetname = System.Configuration.ConfigurationManager.AppSettings["WorksheetF9"];
            Folder = System.Configuration.ConfigurationManager.AppSettings["DocumentF9"];
            //DocumentServer = System.Configuration.ConfigurationManager.AppSettings["DocumentServer"];
            DocumentSignature = System.Configuration.ConfigurationManager.AppSettings["DocumentSignature"];

            excelTemplate = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/" + Folder + "/"), FileName + extenstion);

            List<string> listFileName = new List<string>();
            foreach (var header in listDocType)
            {

                FileInfo templateFile = new FileInfo(excelTemplate);
                ExcelPackage excel = new ExcelPackage(templateFile);
                var workSheet = excel.Workbook.Worksheets[worksheetname];
                var templateSheet = excel.Workbook.Worksheets[worksheetname];
                templateSheet.SelectedRange[1, 1, 65, 75].Copy(workSheet.SelectedRange[1, 1, 65, 75]);


                //WorkSheetJobNo
                workSheet.Cells["B9"].Value = header.F9WorkSheetJobNo;
                //Todaydate
                workSheet.Cells["K11"].Value = header.Todaydate;
                //TodaydateEng
                workSheet.Cells["K12"].Value = header.TodaydateEng;

                //CustomerName 
                workSheet.Cells["B17"].Value = header.CustomerName;
                //UnitAddress 
                workSheet.Cells["C18"].Value = header.UnitAddress;
                //UnitCode
                workSheet.Cells["F18"].Value = header.UnitCode;
                //ProjectName
                workSheet.Cells["C19"].Value = header.ProjectName;
                //CustomerEngName 
                workSheet.Cells["B21"].Value = header.CustomerNameEng;
                //UnitAddress 
                workSheet.Cells["C22"].Value = header.UnitAddress;
                //UnitCode
                workSheet.Cells["F22"].Value = header.UnitCode;
                //ProjectName
                workSheet.Cells["C23"].Value = header.ProjectNameEng;


                //F9Reference 
                workSheet.Cells["B25"].Value = header.F9Reference;
                //F9ReferenceEng 
                workSheet.Cells["B27"].Value = header.F9ReferenceEng;


                //ReceiveCreateDate
                workSheet.Cells["A31"].Value = header.F9Paragraph1;
                //ReceiveCreateDateEng
                workSheet.Cells["A33"].Value = header.F9Paragraph1Eng;


                //F9Paragraph2
                workSheet.Cells["A35"].Value = header.F9Paragraph2;
                //F9Paragraph2Eng
                workSheet.Cells["A42"].Value = header.F9Paragraph2Eng;


                //F9Paragraph3
                workSheet.Cells["A49"].Value = header.F9Paragraph3;
                //F9Paragraph3Eng
                workSheet.Cells["A53"].Value = header.F9Paragraph3Eng;

                //F9SignName
                workSheet.Cells["D64"].Value = header.F9SignName;
                //Signature
                string logoPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/" + DocumentSignature + "/"), UserDetail.CodeName + ".png");
                if (File.Exists(logoPath))
                {
                    Image logo = Image.FromFile(logoPath);
                    var Picture = workSheet.Drawings.AddPicture("TestByPicture", logo);
                    Picture.SetSize(200, 100);
                    Picture.SetPosition(59, 0, 5, 0);
                }

                PathPdf = saveFile(FileName, header.WorkSheetID, extenstion, Folder, excel, databaseSave);
                listFileName.Add(PathPdf);

                Thread.Sleep(1000);
            }
            return listFileName;
        }

        public List<string> ExportQuotationDocument(long WorksheetID)
        {
            string excelTemplate = "";
            string Folder = "";
            string FileName = "";
            string worksheetname = "";
            string extenstion = ".xlsx";
            //string DocumentServer = "";
            string PathPdf = "";

            FileName = System.Configuration.ConfigurationManager.AppSettings["FileQT"];
            worksheetname = System.Configuration.ConfigurationManager.AppSettings["WorksheetQT"];
            Folder = System.Configuration.ConfigurationManager.AppSettings["DocumentQT"];
            //DocumentServer = System.Configuration.ConfigurationManager.AppSettings["DocumentServer"];



            excelTemplate = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/" + Folder + "/"), FileName + extenstion);

            List<string> listFileName = new List<string>();


            FileInfo templateFile = new FileInfo(excelTemplate);
            ExcelPackage excel = new ExcelPackage(templateFile);
            var workSheet = excel.Workbook.Worksheets[worksheetname];
            var templateSheet = excel.Workbook.Worksheets[worksheetname];
            templateSheet.SelectedRange[1, 1, 65, 75].Copy(workSheet.SelectedRange[1, 1, 65, 75]);

            cb_Document objDocument = new cb_Document();
            HC_SP_Get_Quotation_Header_Result QuotatonHeader = new HC_SP_Get_Quotation_Header_Result();
            QuotatonHeader = objDocument.GetQuotationHeader(WorksheetID);
            //ProjectName
            if (QuotatonHeader != null)
            {
                workSheet.Cells["D3"].Value = QuotatonHeader.ProjectThaiName;
                workSheet.Cells["V6"].Value = QuotatonHeader.ProjectThaiName;
                workSheet.Cells["O4"].Value = QuotatonHeader.Unit_Code;
                workSheet.Cells["X4"].Value = QuotatonHeader.Address;
                workSheet.Cells["AH4"].Value = QuotatonHeader.JobNoText;


                workSheet.Cells["G5"].Value = QuotatonHeader.VendorName;
            }

            cb_Worksheet obj = new cb_Worksheet();
            List<HC_SP_Get_TD_Approve_Item_Detail_Result> listItem = obj.getApproveitemDetail(WorksheetID);
            //A13 N R V Z AD AH

            int itemDetailindex = 13;
            int itemDetailend = 13;
            //workSheet.InsertRow(20, listItem.Count - 1, 19);
            foreach (var item in listItem)
            {
                workSheet.Cells["A" + itemDetailend + ":M" + itemDetailend].Merge = true;
                workSheet.Cells["A" + (itemDetailend)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                workSheet.Cells["A" + itemDetailend].Style.WrapText = true;

                workSheet.Cells["N" + itemDetailend + ":P" + itemDetailend].Merge = true;
                workSheet.Cells["N" + (itemDetailend)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                workSheet.Cells["Q" + itemDetailend + ":U" + itemDetailend].Merge = true;
                workSheet.Cells["Q" + (itemDetailend)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                workSheet.Cells["V" + itemDetailend + ":Y" + itemDetailend].Merge = true;
                workSheet.Cells["V" + (itemDetailend)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                workSheet.Cells["Z" + itemDetailend + ":AC" + itemDetailend].Merge = true;
                workSheet.Cells["Z" + (itemDetailend)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                workSheet.Cells["AD" + itemDetailend + ":AG" + itemDetailend].Merge = true;
                workSheet.Cells["AD" + (itemDetailend)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                workSheet.Cells["AH" + itemDetailend + ":AN" + itemDetailend].Merge = true;
                workSheet.Cells["AH" + (itemDetailend)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                workSheet.Cells["AH" + itemDetailend].Style.WrapText = true;

                workSheet.Cells["A" + itemDetailend].Value = item.AppvItem_List1Name + ":" + item.AppvItem_List2Name + ":" + item.AppvItem_List3Name + ":" + item.AppvItem_List4Name;
                workSheet.Cells["N" + itemDetailend].Value = item.AppvItem_Quantity;
                workSheet.Cells["R" + itemDetailend].Value = item.AppvItem_Unit;
                workSheet.Cells["V" + itemDetailend].Value = item.AppvItem_StandardPrice.Value.ToString("#,##0.00");
                workSheet.Cells["Z" + itemDetailend].Value = item.AppvItem_SpecialPrice.Value.ToString("#,##0.00");
                workSheet.Cells["AD" + itemDetailend].Value = item.AppvItem_Price.Value.ToString("#,##0.00");
                workSheet.Cells["AH" + itemDetailend].Value = item.AppvItem_Remark;

                itemDetailend++;
            }
            itemDetailend--;
            workSheet.Cells["A" + itemDetailindex + ":AN" + itemDetailend].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            workSheet.Cells["A" + itemDetailindex + ":AN" + itemDetailend].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            workSheet.Cells["A" + itemDetailindex + ":AN" + itemDetailend].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            workSheet.Cells["A" + itemDetailindex + ":AN" + itemDetailend].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            //-----Summary-----//
            ///1
            workSheet.Cells["N" + (itemDetailend + 1)].Value = "รวมราคาตามรายการ :";
            workSheet.Cells["N" + (itemDetailend + 1)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            workSheet.Cells["AH" + (itemDetailend + 1)].Value = " บาท";

            workSheet.Cells["AD" + (itemDetailend + 1)].Value = QuotatonHeader.TotalPrice.ToString("#,##0.00");
            workSheet.Cells["AD" + (itemDetailend + 1)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

            ///2
            workSheet.Cells["N" + (itemDetailend + 2)].Value = "บวก ค่าดำเนินการ :";
            workSheet.Cells["N" + (itemDetailend + 2)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            workSheet.Cells["W" + (itemDetailend + 2)].Value = QuotatonHeader.OperatingPercent + " %  เป็นเงิน";

            workSheet.Cells["AH" + (itemDetailend + 2)].Value = " บาท";

            workSheet.Cells["AD" + (itemDetailend + 2)].Value = QuotatonHeader.OperatingPrice.ToString("#,##0.00");
            workSheet.Cells["AD" + (itemDetailend + 2)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

            ///3
            workSheet.Cells["N" + (itemDetailend + 3)].Value = "รวมจำนวนเงิน :";
            workSheet.Cells["N" + (itemDetailend + 3)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

            workSheet.Cells["AD" + (itemDetailend + 3)].Value = QuotatonHeader.IncludeOperatingPrice.ToString("#,##0.00");
            workSheet.Cells["AD" + (itemDetailend + 3)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            workSheet.Cells["AH" + (itemDetailend + 3)].Value = " บาท";

            ///4
            workSheet.Cells["N" + (itemDetailend + 4)].Value = "บวก ค่าภาษีมูลค่าเพิ่ม :";
            workSheet.Cells["N" + (itemDetailend + 4)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            workSheet.Cells["W" + (itemDetailend + 4)].Value = QuotatonHeader.VatPercent + " %   เป็นเงิน";
            workSheet.Cells["AD" + (itemDetailend + 4)].Value = QuotatonHeader.VatPrice.ToString("#,##0.00");
            workSheet.Cells["AD" + (itemDetailend + 4)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            workSheet.Cells["AH" + (itemDetailend + 4)].Value = " บาท";


            ///5
            workSheet.Cells["N" + (itemDetailend + 5)].Value = "รวมเป็นเงินทั้งสิ้น :";
            workSheet.Cells["N" + (itemDetailend + 5)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;///5
            workSheet.Cells["AD" + (itemDetailend + 5)].Value = QuotatonHeader.NetPrice.ToString("#,##0.00"); ;
            workSheet.Cells["AD" + (itemDetailend + 5)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            workSheet.Cells["AH" + (itemDetailend + 5)].Value = " บาท";


            //--------------------------//
            itemDetailend++;
            workSheet.Cells["A" + itemDetailend + ":AN" + (itemDetailend + 3)].Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;
            workSheet.Cells["A" + (itemDetailend + 4) + ":AN" + (itemDetailend + 4)].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            workSheet.Cells["A" + (itemDetailend) + ":A" + (itemDetailend + 4)].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            workSheet.Cells["M" + (itemDetailend) + ":M" + (itemDetailend + 4)].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            workSheet.Cells["AD" + (itemDetailend) + ":AD" + (itemDetailend + 4)].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            workSheet.Cells["AH" + (itemDetailend) + ":AH" + (itemDetailend + 4)].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            workSheet.Cells["AN" + (itemDetailend) + ":AN" + (itemDetailend + 4)].Style.Border.Right.Style = ExcelBorderStyle.Thin;

            workSheet.Cells["N" + itemDetailend + ":AC" + itemDetailend].Merge = false;
            workSheet.Cells["N" + itemDetailend + ":U" + itemDetailend].Merge = true;
            workSheet.Cells["AD" + itemDetailend + ":AG" + itemDetailend].Merge = true;

            itemDetailend++;
            workSheet.Cells["N" + itemDetailend + ":AC" + itemDetailend].Merge = false;
            workSheet.Cells["N" + itemDetailend + ":U" + itemDetailend].Merge = true;
            workSheet.Cells["AD" + itemDetailend + ":AG" + itemDetailend].Merge = true;
            workSheet.Cells["W" + itemDetailend + ":AA" + itemDetailend].Merge = true;

            itemDetailend++;
            workSheet.Cells["N" + itemDetailend + ":AC" + itemDetailend].Merge = false;
            workSheet.Cells["N" + itemDetailend + ":U" + itemDetailend].Merge = true;
            workSheet.Cells["AD" + itemDetailend + ":AG" + itemDetailend].Merge = true;

            itemDetailend++;
            workSheet.Cells["N" + itemDetailend + ":AC" + itemDetailend].Merge = false;
            workSheet.Cells["N" + itemDetailend + ":U" + itemDetailend].Merge = true;
            workSheet.Cells["W" + itemDetailend + ":AA" + itemDetailend].Merge = true;
            workSheet.Cells["AD" + itemDetailend + ":AG" + itemDetailend].Merge = true;

            itemDetailend++;
            workSheet.Cells["N" + itemDetailend + ":AC" + itemDetailend].Merge = false;
            workSheet.Cells["N" + itemDetailend + ":U" + itemDetailend].Merge = true;
            workSheet.Cells["AD" + itemDetailend + ":AG" + itemDetailend].Merge = true;

            //--------------------------//

            //-----End of Summary-----//



            itemDetailend++;
            itemDetailend++;
            itemDetailend++;

            workSheet.Cells["A" + itemDetailend + ":M" + itemDetailend].Merge = false;
            workSheet.Cells["C" + itemDetailend + ":M" + itemDetailend].Merge = true;
            workSheet.Cells["C" + itemDetailend].Value = "จึงเรียนมาเพื่อโปรดพิจารณา";

            itemDetailend++;
            itemDetailend++;
            workSheet.Cells["A" + itemDetailend + ":AN" + itemDetailend].Merge = false;
            workSheet.Cells["A" + itemDetailend + ":M" + itemDetailend].Merge = true;
            workSheet.Cells["A" + itemDetailend].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells["A" + itemDetailend].Value = "ชื่อผู้อนุมัติ";

            workSheet.Cells["N" + itemDetailend + ":R" + itemDetailend].Merge = true;
            workSheet.Cells["N" + itemDetailend].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells["N" + itemDetailend].Value = "สิทธิ์ผู้อนุมัติ";


            workSheet.Cells["S" + itemDetailend + ":W" + itemDetailend].Merge = true;
            workSheet.Cells["S" + itemDetailend].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells["S" + itemDetailend].Value = "วันที่อนุมัติ";

            //----//
            itemDetailend++;
            workSheet.Cells["A" + itemDetailend + ":AN" + itemDetailend].Merge = false;
            workSheet.Cells["A" + itemDetailend + ":M" + itemDetailend].Merge = true;
            workSheet.Cells["A" + itemDetailend].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells["A" + itemDetailend].Value = "";

            workSheet.Cells["N" + itemDetailend + ":R" + itemDetailend].Merge = true;
            workSheet.Cells["N" + itemDetailend].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells["N" + itemDetailend].Value = "";


            workSheet.Cells["S" + itemDetailend + ":W" + itemDetailend].Merge = true;
            workSheet.Cells["S" + itemDetailend].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells["S" + itemDetailend].Value = "";


            workSheet.Cells["A" + (itemDetailend - 1) + ":W" + (itemDetailend)].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            workSheet.Cells["A" + (itemDetailend - 1) + ":W" + (itemDetailend)].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            workSheet.Cells["A" + (itemDetailend - 1) + ":W" + (itemDetailend)].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            workSheet.Cells["A" + (itemDetailend - 1) + ":W" + (itemDetailend)].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            itemDetailend++;
            itemDetailend++;
            itemDetailend++;
            itemDetailend++;
            itemDetailend++;

            workSheet.Cells["A" + itemDetailend + ":AN" + itemDetailend].Merge = false;
            workSheet.Cells["AA" + itemDetailend + ":AJ" + itemDetailend].Merge = true;
            workSheet.Cells["AA" + itemDetailend].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells["AA" + itemDetailend].Value = "ขอแสดงความนับถือ";

            itemDetailend++;
            itemDetailend++;
            itemDetailend++;

            workSheet.Cells["A" + itemDetailend + ":AN" + itemDetailend].Merge = false;
            workSheet.Cells["C" + itemDetailend + ":L" + itemDetailend].Merge = true;
            //workSheet.Cells["AA" + itemDetailend].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells["C" + itemDetailend].Value = "";
            workSheet.Cells["C" + itemDetailend + ":L" + (itemDetailend)].Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;

            workSheet.Cells["O" + itemDetailend + ":X" + itemDetailend].Merge = true;
            //workSheet.Cells["O" + itemDetailend].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells["O" + itemDetailend].Value = "";
            workSheet.Cells["O" + itemDetailend + ":X" + (itemDetailend)].Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;

            workSheet.Cells["AA" + itemDetailend + ":AK" + itemDetailend].Merge = true;
            //workSheet.Cells["AA" + itemDetailend].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells["AA" + itemDetailend].Value = "";
            workSheet.Cells["AA" + itemDetailend + ":AK" + (itemDetailend)].Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;

            itemDetailend++;

            workSheet.Cells["A" + itemDetailend + ":AN" + itemDetailend].Merge = false;


            workSheet.Cells["D" + itemDetailend + ":K" + itemDetailend].Merge = true;
            workSheet.Cells["AA" + itemDetailend].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells["C" + itemDetailend].Value = "(";
            workSheet.Cells["L" + itemDetailend].Value = ")";
            workSheet.Cells["C" + itemDetailend].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            workSheet.Cells["L" + itemDetailend].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            workSheet.Cells["D" + itemDetailend + ":K" + (itemDetailend)].Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;

            workSheet.Cells["P" + itemDetailend + ":W" + itemDetailend].Merge = true;
            workSheet.Cells["P" + itemDetailend].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells["O" + itemDetailend].Value = "(";
            workSheet.Cells["X" + itemDetailend].Value = ")";
            workSheet.Cells["O" + itemDetailend].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            workSheet.Cells["X" + itemDetailend].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            workSheet.Cells["P" + itemDetailend + ":W" + (itemDetailend)].Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;

            workSheet.Cells["AB" + itemDetailend + ":AJ" + itemDetailend].Merge = true;
            workSheet.Cells["AB" + itemDetailend].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells["AA" + itemDetailend].Value = "(";
            workSheet.Cells["AK" + itemDetailend].Value = ")";
            workSheet.Cells["AA" + itemDetailend].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            workSheet.Cells["AK" + itemDetailend].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            workSheet.Cells["AB" + itemDetailend + ":AJ" + (itemDetailend)].Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;

            itemDetailend++;

            workSheet.Cells["A" + itemDetailend + ":AN" + itemDetailend].Merge = false;
            workSheet.Cells["D" + itemDetailend + ":K" + itemDetailend].Merge = true;

            workSheet.Cells["D" + itemDetailend].Value = "ผู้อนุมัติราคา";
            workSheet.Cells["D" + itemDetailend].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            workSheet.Cells["P" + itemDetailend + ":W" + itemDetailend].Merge = true;
            workSheet.Cells["P" + itemDetailend].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells["P" + itemDetailend].Value = "ผู้รับทราบ";


            workSheet.Cells["X" + itemDetailend + ":Z" + itemDetailend].Merge = true;
            workSheet.Cells["X" + itemDetailend].Value = "บริษัท";
            workSheet.Cells["X" + itemDetailend].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            workSheet.Cells["AA" + itemDetailend + ":AM" + itemDetailend].Merge = true;
            workSheet.Cells["AA" + itemDetailend].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells["AA" + itemDetailend + ":AM" + (itemDetailend)].Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;
            workSheet.Cells["AA" + itemDetailend].Value = QuotatonHeader.VendorName;

            itemDetailend++;

            workSheet.Cells["A" + itemDetailend + ":AN" + itemDetailend].Merge = false;
            workSheet.Cells["AB" + itemDetailend + ":AK" + itemDetailend].Merge = true;
            workSheet.Cells["AB" + itemDetailend].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells["AB" + itemDetailend].Value = "ผู้เสนอราคา";

            PathPdf = saveFile(FileName, QuotatonHeader.WorkSheet_ID, extenstion, Folder, excel);
            listFileName.Add(PathPdf);

            Thread.Sleep(1000);

            return listFileName;



        }

        public List<string> ExportWorkSheet(TrackerSearchViewModel model)
        {
            string excelTemplate = "";
            string Folder = "";
            string FileName = "";
            string worksheetname = "";
            string extenstion = ".xlsx";
            //string DocumentServer = "";
            string PathPdf = "";

            FileName = System.Configuration.ConfigurationManager.AppSettings["FileWSReport"];
            worksheetname = System.Configuration.ConfigurationManager.AppSettings["WorksheetReport"];
            Folder = System.Configuration.ConfigurationManager.AppSettings["DocumentWorksheetReport"];
            //DocumentServer = System.Configuration.ConfigurationManager.AppSettings["DocumentServer"]; 
            excelTemplate = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/" + Folder + "/"), FileName + extenstion);

            FileInfo templateFile = new FileInfo(excelTemplate);
            ExcelPackage excel = new ExcelPackage(templateFile);
            var workSheet = excel.Workbook.Worksheets[worksheetname];
            var templateSheet = excel.Workbook.Worksheets[worksheetname];
            templateSheet.SelectedRange[1, 1, 65, 75].Copy(workSheet.SelectedRange[1, 1, 65, 75]);

            int itemDetailend = 3;


            cb_Tracker obj = new cb_Tracker();

            TrackerViewModel Tracker = new TrackerViewModel();
            List<HC_SP_Get_TrackerByWorkSheet_Result> result = obj.getlistTrackerByWorkSheet(model);
            //Tracker.TrackerByWorkSheetResult = result;

            foreach (HC_SP_Get_TrackerByWorkSheet_Result item in result)
            {

                workSheet.Cells["A" + itemDetailend].Value = item.FlagAfterTransfer;
                workSheet.Cells["B" + itemDetailend].Value = item.Status;
                workSheet.Cells["C" + itemDetailend].Value = item.PaymentType;
                workSheet.Cells["D" + itemDetailend].Value = item.ConfirmFinishDate;
                workSheet.Cells["E" + itemDetailend].Value = item.ActivityDateThaiFormat;
                workSheet.Cells["F" + itemDetailend].Value = item.UserAcctName;
                workSheet.Cells["G" + itemDetailend].Value = item.JobNoText;
                workSheet.Cells["H" + itemDetailend].Value = item.Address;
                workSheet.Cells["I" + itemDetailend].Value = item.ProjectName;
                workSheet.Cells["J" + itemDetailend].Value = item.UnitCode;

                workSheet.Cells["K" + itemDetailend].Value = item.SpecNameLevel1;
                workSheet.Cells["L" + itemDetailend].Value = item.SpecNameLevel2;
                workSheet.Cells["M" + itemDetailend].Value = item.SpecNameLevel3;
                workSheet.Cells["N" + itemDetailend].Value = item.SpecCodeLevel3;
                workSheet.Cells["O" + itemDetailend].Value = item.Violence;
                workSheet.Cells["P" + itemDetailend].Value = item.SpecDetailLevel3;
                workSheet.Cells["Q" + itemDetailend].Value = item.Priority;
                workSheet.Cells["R" + itemDetailend].Value = item.StartDate;
                workSheet.Cells["S" + itemDetailend].Value = item.ExpectDate;
                workSheet.Cells["T" + itemDetailend].Value = item.VendorName;

                workSheet.Cells["U" + itemDetailend].Value = item.FlagVendorReceive;
                workSheet.Cells["V" + itemDetailend].Value = item.StartDate;
                workSheet.Cells["W" + itemDetailend].Value = item.FinishDate;
                workSheet.Cells["X" + itemDetailend].Value = item.MissDueDate;
                workSheet.Cells["Y" + itemDetailend].Value = item.CustomerTotalCost;
                workSheet.Cells["Z" + itemDetailend].Value = item.FixPriceSum;
                workSheet.Cells["AA" + itemDetailend].Value = item.GpValues;
                workSheet.Cells["AB" + itemDetailend].Value = item.Remark;
                workSheet.Cells["AC" + itemDetailend].Value = item.TransferDate;
                workSheet.Cells["AD" + itemDetailend].Value = item.StartGuaranteeDate;
                workSheet.Cells["AE" + itemDetailend].Value = item.EndGuaranteeDate;
                workSheet.Cells["AF" + itemDetailend].Value = item.TransferEndLessThan6Month;
                workSheet.Cells["AG" + itemDetailend].Value = item.TransferEndLessThan18Month;

                itemDetailend++;
            }
            itemDetailend--;
            workSheet.Cells["A" + (3) + ":AG" + (itemDetailend)].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            workSheet.Cells["A" + (3) + ":AG" + (itemDetailend)].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            workSheet.Cells["A" + (3) + ":AG" + (itemDetailend)].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            workSheet.Cells["A" + (3) + ":AG" + (itemDetailend)].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            PathPdf = saveFile(FileName, extenstion, Folder, excel);
            List<string> listFileName = new List<string>();
            listFileName.Add(PathPdf);

            Thread.Sleep(1000);

            return listFileName;
        }

        public List<string> ExportWorkSheetSLA(TrackerSearchViewModel model)
        {
            string excelTemplate = "";
            string Folder = "";
            string FileName = "";
            string worksheetname = "";
            string extenstion = ".xlsx";
            //string DocumentServer = "";
            string PathPdf = "";

            FileName = System.Configuration.ConfigurationManager.AppSettings["FileWSSLAReport"];
            worksheetname = System.Configuration.ConfigurationManager.AppSettings["WorksheetSLAReport"];
            Folder = System.Configuration.ConfigurationManager.AppSettings["DocumentWorksheetReport"];
            //DocumentServer = System.Configuration.ConfigurationManager.AppSettings["DocumentServer"]; 
            excelTemplate = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/" + Folder + "/"), FileName + extenstion);

            FileInfo templateFile = new FileInfo(excelTemplate);
            ExcelPackage excel = new ExcelPackage(templateFile);
            var workSheet = excel.Workbook.Worksheets[worksheetname];
            var templateSheet = excel.Workbook.Worksheets[worksheetname];
            templateSheet.SelectedRange[1, 1, 65, 75].Copy(workSheet.SelectedRange[1, 1, 65, 75]);

            int itemDetailend = 3;


            cb_Tracker obj = new cb_Tracker();

            TrackerViewModel Tracker = new TrackerViewModel();
            List<HC_SP_Get_TrackerByWorkSheet_Kpi_Result> result = obj.getListTrackerByWorkSheetKpi(model);
            //Tracker.TrackerByWorkSheetResult = result;
            if (result.Count > 0)
                workSheet.Cells["A1"].Value = "รายงานสรุปรายละเอียดใบงานโครงการ " + result.FirstOrDefault().ProjectName;

            foreach (HC_SP_Get_TrackerByWorkSheet_Kpi_Result item in result)
            {

                workSheet.Cells["A" + itemDetailend].Value = item.ProjectName;
                workSheet.Cells["B" + itemDetailend].Value = item.UnitCode;
                workSheet.Cells["C" + itemDetailend].Value = item.WorkSheet_JobNoText;
                workSheet.Cells["D" + itemDetailend].Value = item.WorkSheet_Status;
                workSheet.Cells["E" + itemDetailend].Value = item.WorkSheet_StatusApprove;
                workSheet.Cells["F" + itemDetailend].Value = item.InformDate;
                workSheet.Cells["G" + itemDetailend].Value = item.WorkSheet_CreateDate;
                workSheet.Cells["H" + itemDetailend].Value = item.WorkSheet_CloseDate;
                workSheet.Cells["I" + itemDetailend].Value = item.WorkSheet_CreateBy;
                workSheet.Cells["J" + itemDetailend].Value = item.VendorName;

                workSheet.Cells["K" + itemDetailend].Value = item.PaymentName;
                workSheet.Cells["L" + itemDetailend].Value = item.NetPrice;
                workSheet.Cells["M" + itemDetailend].Value = item.WorkSheetItem_Status;
                workSheet.Cells["N" + itemDetailend].Value = item.HC_Guarantee;
                workSheet.Cells["O" + itemDetailend].Value = item.Spec1Name;
                workSheet.Cells["P" + itemDetailend].Value = item.Spec2Name;
                workSheet.Cells["Q" + itemDetailend].Value = item.Spec3Name;
                workSheet.Cells["R" + itemDetailend].Value = item.LocationName;
                workSheet.Cells["S" + itemDetailend].Value = item.WI_RepairAppointmentDateFrom;
                workSheet.Cells["T" + itemDetailend].Value = item.WorkSheet_StartDate;
                workSheet.Cells["U" + itemDetailend].Value = item.DateDiff_AppointmentStartDate;
                workSheet.Cells["V" + itemDetailend].Value = item.DateDiff_AppointmentStartDate > 0 ? "MissStart" : string.Empty;
                workSheet.Cells["W" + itemDetailend].Value = item.WI_RepairAppointmentDateTo;
                workSheet.Cells["X" + itemDetailend].Value = item.WorkSheet_FinishDate;
                workSheet.Cells["Y" + itemDetailend].Value = item.DateDiff_AppointmentFinishDate;
                workSheet.Cells["Z" + itemDetailend].Value = item.DateDiff_AppointmentFinishDate > 0 ? "MissFinish" : string.Empty;
                workSheet.Cells["AA" + itemDetailend].Value = item.PriorityName;
                workSheet.Cells["AB" + itemDetailend].Value = item.WorkSheet_ExpectDate;

                workSheet.Cells["AC" + itemDetailend].Value = item.DateDiff_ExpectDate;
                workSheet.Cells["AD" + itemDetailend].Value = item.DateDiff_ExpectDate > 0 ? "MissExpect" : string.Empty;



                itemDetailend++;
            }
            itemDetailend--;
            workSheet.Cells["A" + (3) + ":AD" + (itemDetailend)].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            workSheet.Cells["A" + (3) + ":AD" + (itemDetailend)].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            workSheet.Cells["A" + (3) + ":AD" + (itemDetailend)].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            workSheet.Cells["A" + (3) + ":AD" + (itemDetailend)].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            PathPdf = saveFile(FileName, extenstion, Folder, excel);
            List<string> listFileName = new List<string>();
            listFileName.Add(PathPdf);

            Thread.Sleep(1000);

            return listFileName;
        }

        public List<string> ExportWorkSheetByReceived(TrackerSearchViewModel model)
        {
            string excelTemplate = "";
            string Folder = "";
            string FileName = "";
            string worksheetname = "";
            string extenstion = ".xlsx";
            //string DocumentServer = "";
            string PathPdf = "";

            FileName = System.Configuration.ConfigurationManager.AppSettings["FileWSSLAByReceivedReport"];
            worksheetname = System.Configuration.ConfigurationManager.AppSettings["WorksheetSLAByReceivedReport"];
            Folder = System.Configuration.ConfigurationManager.AppSettings["DocumentWorksheetReport"];
            //DocumentServer = System.Configuration.ConfigurationManager.AppSettings["DocumentServer"]; 
            excelTemplate = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/" + Folder + "/"), FileName + extenstion);

            FileInfo templateFile = new FileInfo(excelTemplate);
            ExcelPackage excel = new ExcelPackage(templateFile);
            var workSheet = excel.Workbook.Worksheets[worksheetname];
            var templateSheet = excel.Workbook.Worksheets[worksheetname];
            templateSheet.SelectedRange[1, 1, 65, 75].Copy(workSheet.SelectedRange[1, 1, 65, 75]);

            int itemDetailend = 3;


            cb_Tracker obj = new cb_Tracker();

            TrackerViewModel Tracker = new TrackerViewModel();
            List<HC_SP_Get_TrackerByReceive_Result> result = obj.getlistTrackerByReceive(model);
            //Tracker.TrackerByWorkSheetResult = result;
            if (result.Count > 0)
                workSheet.Cells["A1"].Value = "รายงานสรุปรายละเอียดใบรับเรื่องโครงการ ";

            foreach (HC_SP_Get_TrackerByReceive_Result item in result)
            {

                workSheet.Cells["A" + itemDetailend].Value = item.ProjectName;
                workSheet.Cells["B" + itemDetailend].Value = item.UnitCode;
                workSheet.Cells["C" + itemDetailend].Value = item.ReceiveJobNoText;
                workSheet.Cells["D" + itemDetailend].Value = item.ReceiveMainProcessName;
                workSheet.Cells["E" + itemDetailend].Value = item.ReceiveCountNumber;
                workSheet.Cells["F" + itemDetailend].Value = item.InformChannel;
                workSheet.Cells["G" + itemDetailend].Value = item.InformCallcentre;
                workSheet.Cells["H" + itemDetailend].Value = item.InfromDate;
                workSheet.Cells["I" + itemDetailend].Value = item.InfromRemark;
                workSheet.Cells["J" + itemDetailend].Value = item.ReceiveStatusName;
                workSheet.Cells["K" + itemDetailend].Value = item.ReceiveHCDate;
                workSheet.Cells["L" + itemDetailend].Value = item.ReceiveHCName;
                workSheet.Cells["M" + itemDetailend].Value = item.ReceiveHCRemark;
                workSheet.Cells["N" + itemDetailend].Value = item.DateDiff_ReceiveDate;
                workSheet.Cells["O" + itemDetailend].Value = item.DateDiff_ReceiveDate > 24 ? "MissReceive" : string.Empty;
                workSheet.Cells["P" + itemDetailend].Value = item.Receive_Appointment_Date;
                workSheet.Cells["Q" + itemDetailend].Value = item.Receive_Appointment_Time_From;
                workSheet.Cells["R" + itemDetailend].Value = item.Receive_Appointment_Time_To;
                workSheet.Cells["S" + itemDetailend].Value = item.Receive_Appointment_Date_Real;
                workSheet.Cells["T" + itemDetailend].Value = item.DateDiff_ReceiveAppointmentDate;
                workSheet.Cells["U" + itemDetailend].Value = item.DateDiff_ReceiveAppointmentDate > 0 ? "MissCheck" : string.Empty;

                itemDetailend++;
            }
            itemDetailend--;
            workSheet.Cells["A" + (3) + ":U" + (itemDetailend)].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            workSheet.Cells["A" + (3) + ":U" + (itemDetailend)].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            workSheet.Cells["A" + (3) + ":U" + (itemDetailend)].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            workSheet.Cells["A" + (3) + ":U" + (itemDetailend)].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            PathPdf = saveFile(FileName, extenstion, Folder, excel);
            List<string> listFileName = new List<string>();
            listFileName.Add(PathPdf);

            Thread.Sleep(1000);

            return listFileName;
        }

        public List<string> ExportWorkSheetQuestionnaire(TrackerSearchViewModel model)
        {
            string excelTemplate = "";
            string Folder = "";
            string FileName = "";
            string worksheetname = "";
            string extenstion = ".xlsx";
            //string DocumentServer = "";
            string PathPdf = "";

            FileName = System.Configuration.ConfigurationManager.AppSettings["FileQuestionnaireReport"];
            worksheetname = System.Configuration.ConfigurationManager.AppSettings["QuestionnaireReport"];
            Folder = System.Configuration.ConfigurationManager.AppSettings["DocumentQuestionnaireReport"];
            //DocumentServer = System.Configuration.ConfigurationManager.AppSettings["DocumentServer"]; 
            excelTemplate = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/" + Folder + "/"), FileName + extenstion);

            FileInfo templateFile = new FileInfo(excelTemplate);
            ExcelPackage excel = new ExcelPackage(templateFile);
            var workSheet = excel.Workbook.Worksheets[worksheetname];
            var templateSheet = excel.Workbook.Worksheets[worksheetname];
            templateSheet.SelectedRange[1, 1, 65, 75].Copy(workSheet.SelectedRange[1, 1, 65, 75]);

            int itemDetailend = 3;


            cb_Tracker obj = new cb_Tracker();

            TrackerViewModel Tracker = new TrackerViewModel();
            List<HC_SP_Get_TrackerByWorkSheet_Ques_Result> result = obj.getListTrackerByWorkSheetQues(model);
            //Tracker.TrackerByWorkSheetResult = result;
            if (result.Count > 0)
                workSheet.Cells["A1"].Value = "รายงานสรุปผลคะแนนแบบประเมิน ";

            foreach (HC_SP_Get_TrackerByWorkSheet_Ques_Result item in result)
            {

				workSheet.Cells["A" + itemDetailend].Value = item.ProjectName;
				workSheet.Cells["B" + itemDetailend].Value = item.UnitCode.Trim();
				workSheet.Cells["C" + itemDetailend].Value = item.WorkSheet_JobNoText.Trim();
				workSheet.Cells["D" + itemDetailend].Value = item.FinishDate.Trim();
				workSheet.Cells["E" + itemDetailend].Value = item.CloseDate.Trim();
				workSheet.Cells["F" + itemDetailend].Value = item.Evaluate_Media.Trim();
				workSheet.Cells["G" + itemDetailend].Value = item.Questionnaire_status.Trim();
				workSheet.Cells["H" + itemDetailend].Value = item.DescriptionTH == null ? "-" : item.DescriptionTH.Trim();
				workSheet.Cells["I" + itemDetailend].Value = !string.Equals(item.PointQ1 + "", "") ? item.PointQ1 + "" : "-";
				workSheet.Cells["J" + itemDetailend].Value = !string.Equals(item.PointQ2 + "", "") ? item.PointQ2 + "" : "-";
				workSheet.Cells["K" + itemDetailend].Value = !string.Equals(item.PointQ3 + "", "") ? item.PointQ3 + "" : "-";
				workSheet.Cells["L" + itemDetailend].Value = !string.Equals(item.PointQ4 + "", "") ? item.PointQ4 + "" : "-";
				workSheet.Cells["M" + itemDetailend].Value = !string.Equals(item.PointQ5 + "", "") ? item.PointQ5 + "" : "-";
				workSheet.Cells["N" + itemDetailend].Value = string.Equals(item.Questionnaire_status.Trim(), "ประเมินแล้ว") ? (!string.Equals(item.PointQ6 + "", "") ? item.PointQ6 + "" : "ไม่มีข้อเสนอแนะ") : "-";
				workSheet.Cells["O" + itemDetailend].Value = item.HcCreatedBy.Trim();
				workSheet.Cells["P" + itemDetailend].Value = item.VendorName.Trim();
				workSheet.Cells["Q" + itemDetailend].Value = item.ReceiveCreatedBy.Trim();

				itemDetailend++;
			}
			itemDetailend--;
			workSheet.Cells["A" + (3) + ":Q" + (itemDetailend)].Style.Border.Right.Style = ExcelBorderStyle.Thin;
			workSheet.Cells["A" + (3) + ":Q" + (itemDetailend)].Style.Border.Left.Style = ExcelBorderStyle.Thin;
			workSheet.Cells["A" + (3) + ":Q" + (itemDetailend)].Style.Border.Top.Style = ExcelBorderStyle.Thin;
			workSheet.Cells["A" + (3) + ":Q" + (itemDetailend)].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
			//workSheet.Cells["B" + (3) + ":Q" + (itemDetailend)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
			//workSheet.Cells["A" + (3) + ":A" + (itemDetailend)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

            PathPdf = saveFile(FileName, extenstion, Folder, excel);
            List<string> listFileName = new List<string>();
            listFileName.Add(PathPdf);

            Thread.Sleep(1000);

            return listFileName;
        }

        public List<string> ExportCombineDocument(List<HC_SP_Get_DocumentByFormType_Result> listDocType, int doc1, int doc2, int databaseSave1 = 1, int databaseSave2 = 0, string VendorName = null, string VendorAddress = null, long VendorID = 0)
        {
            List<string> pathDoc1 = new List<string>();
            List<string> pathDoc2 = new List<string>();
            var indexString1 = string.Empty;
            var indexString2 = string.Empty;

            switch (doc1)
            {
                case 1:
                    pathDoc1 = ExportF1Document(listDocType, databaseSave1);
                    indexString1 = "/DocumentF/F1/F1";
                    break;
                case 2:
                    pathDoc1 = ExportF2Document(listDocType, VendorName, VendorAddress, VendorID, databaseSave1);
                    indexString1 = "/DocumentF/F2/F2";
                    break;
                case 5:
                    pathDoc1 = ExportF5Document(listDocType, databaseSave1);
                    indexString1 = "/DocumentF/F5/F5";
                    break;
                case 9:
                    pathDoc1 = ExportF9Document(listDocType, databaseSave1);
                    indexString1 = "/DocumentF/F9/F9";
                    break;
                default:
                    break;
            }

            switch (doc2)
            {
                case 1:
                    pathDoc2 = ExportF1Document(listDocType, databaseSave2);
                    indexString2 = "/DocumentF/F1/F1";
                    break;
                case 2:
                    pathDoc2 = ExportF2Document(listDocType, VendorName, VendorAddress, databaseSave2);
                    indexString2 = "/DocumentF/F2/F2";
                    break;               
                case 5:
                    pathDoc2 = ExportF5Document(listDocType, databaseSave2);
                    indexString2 = "/DocumentF/F5/F5";
                    break;
                case 9:
                    pathDoc2 = ExportF9Document(listDocType, databaseSave2);
                    indexString2 = "/DocumentF/F9/F9";
                    break;
                default:
                    break;
            }

            for (int i = 0; i < pathDoc1.Count; i++)
            {
                string Doc1FilePath = System.AppContext.BaseDirectory + pathDoc1[i].Substring(pathDoc1[i].IndexOf(indexString1));
                string Doc2FilePath = "";
                if (pathDoc2.Count > 0) {
                    Doc2FilePath = System.AppContext.BaseDirectory + pathDoc2[i].Substring(pathDoc2[i].IndexOf(indexString2));
                }

                using (PdfDocument Doc1Pdf = PdfReader.Open(Doc1FilePath, PdfDocumentOpenMode.Import))             
                using (PdfDocument outPdf = new PdfDocument())
                {
                    CopyPages(Doc1Pdf, outPdf);
                    if (Doc2FilePath != "") {
                        using (PdfDocument Doc2Pdf = PdfReader.Open(Doc2FilePath, PdfDocumentOpenMode.Import))
                        CopyPages(Doc2Pdf, outPdf);
                    }                  

                    outPdf.Save(Doc1FilePath);
                }
            }
            return pathDoc1;
        }

        private void CopyPages(PdfDocument from, PdfDocument to)
        {
            for (int i = 0; i < from.PageCount; i++)
            {
                to.AddPage(from.Pages[i]);
            }
        }


        private string saveFile(string FileName, long WorkSheetID, string extenstion, string Folder, ExcelPackage excel, int databaseSave = 1, long VendorID = 0, string VendorName = null)
        {
            String DocumentServer = System.Configuration.ConfigurationManager.AppSettings["DocumentServer"];
            string exportFileName = FileName + "_" + WorkSheetID + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + extenstion;
            string pathFile = Path.Combine(HttpContext.Current.Server.MapPath("~/" + Folder + "/"), exportFileName);
            excel.SaveAs(new FileInfo(pathFile));
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            ExcelFile.Load(pathFile).Save(pathFile.Replace(".xlsx", ".pdf"));
            excel.Dispose();

            string pathServer = DocumentServer + Folder + exportFileName;
            string pathPdf = pathServer.Replace(".xlsx", ".pdf");

            //Insert log
            if (databaseSave == 1)
            {
                objDocument.DocumentByWorkSheetPrint(WorkSheetID, FileName, pathPdf, VendorID, VendorName);
            }

            return pathPdf;
        }
        private string saveFile(string FileName, string extenstion, string Folder, ExcelPackage excel)
        {
            String DocumentServer = System.Configuration.ConfigurationManager.AppSettings["DocumentServer"];
            string exportFileName = FileName + "_" + "WorkSheetReport" + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + extenstion;
            string pathFile = Path.Combine(HttpContext.Current.Server.MapPath("~/" + Folder + "/"), exportFileName);
            excel.SaveAs(new FileInfo(pathFile));
            excel.Dispose();

            string pathServer = DocumentServer + Folder + exportFileName;
            return pathServer;
        }

        public List<HC_SP_Get_DocumentByFormType_Result> getDocumentByFormType(string WorkSheetIDList)
        {
            return context.HC_SP_Get_DocumentByFormType(WorkSheetIDList).ToList();
        }

        public List<HC_SP_Get_V_Receive_Item_Result> getReceiveItem(long? WorkSheet)
        {
            List<HC_SP_Get_V_Receive_Item_Result> listItem = context.HC_SP_Get_V_Receive_Item(WorkSheet).ToList();
            return listItem;
        }
        public List<HC_SP_Get_V_Receive_Item_Detail_Result> getReceiveItemDetail(long? WorkSheet)
        {
            List<HC_SP_Get_V_Receive_Item_Detail_Result> listItemDetail = context.HC_SP_Get_V_Receive_Item_Detail(WorkSheet).ToList();
            return listItemDetail;
        }

        public List<HC_SP_API_GET_DOCUMENT_F3_Result> getDataExportDocumentF3(string UnitIdList)
        {
            return context.HC_SP_API_GET_DOCUMENT_F3(null, null, null, null, null, UnitIdList).ToList();
        }

        public List<string> ExportCombineDocumentF3(List<HC_SP_API_GET_DOCUMENT_F3_Result> listDocType, int doc1, int databaseSave1 = 1)
        {
            List<string> pathDoc1 = new List<string>();
            List<string> pathDoc2 = new List<string>();
            var indexString1 = string.Empty;
            var indexString2 = string.Empty;

            switch (doc1)
            {
                case 3:
                    pathDoc1 = ExportF3Document(listDocType, databaseSave1);
                    indexString1 = "/DocumentF/F3/F3";
                    break;
                case 4:
                    pathDoc1 = ExportF3ENDocument(listDocType, databaseSave1);
                    indexString1 = "/DocumentF/F3/F3";
                    break;
                default:
                    break;
            }

            for (int i = 0; i < pathDoc1.Count; i++)
            {
                string Doc1FilePath = System.AppContext.BaseDirectory + pathDoc1[i].Substring(pathDoc1[i].IndexOf(indexString1));
                string Doc2FilePath = "";
                if (pathDoc2.Count > 0)
                {
                    Doc2FilePath = System.AppContext.BaseDirectory + pathDoc2[i].Substring(pathDoc2[i].IndexOf(indexString2));
                }

                using (PdfDocument Doc1Pdf = PdfReader.Open(Doc1FilePath, PdfDocumentOpenMode.Import))
                using (PdfDocument outPdf = new PdfDocument())
                {
                    CopyPages(Doc1Pdf, outPdf);
                    if (Doc2FilePath != "")
                    {
                        using (PdfDocument Doc2Pdf = PdfReader.Open(Doc2FilePath, PdfDocumentOpenMode.Import))
                            CopyPages(Doc2Pdf, outPdf);
                    }

                    outPdf.Save(Doc1FilePath);
                }
            }
            return pathDoc1;
        }

    }
}