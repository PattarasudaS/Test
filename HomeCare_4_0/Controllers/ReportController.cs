using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeCare_4_0.Controllers
{
    public class ReportController : Controller
    {

        public ActionResult PrintLabel(string WorksheetID, string FormType)
        {
            //Get Report Path
            string FileName = System.Configuration.ConfigurationManager.AppSettings["FilePrintLabel"];
            //Get Report Folder
            string Folder = System.Configuration.ConfigurationManager.AppSettings["DocumentPrintLabel"];

            //Load Crystal Report
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath(Folder), FileName));
             
            //Set Database Configuration
            rd.SetDatabaseLogon("AdminHomeCare", "AdminDpta@IT");
            rd.SetParameterValue("@WorkListID", WorksheetID);
            rd.SetParameterValue("@FormType", FormType);

            //rd.PrintOptions.PaperSize = PaperSize.DefaultPaperSize;
            //rd.PrintOptions.PrinterName = "TSC TTP-345";


            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            //Set Export To Pdf
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            // แสดงผ่าน Browser เป็น PDF
            var fsResult = new FileStreamResult(stream, "application/pdf");
            return fsResult;
        }

        // GET: Report
        
    }
}