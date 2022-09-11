using System;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using HomeCare_4_0.ClassLib;
using HomeCare_4_0.Models;
using HomeCare_4_0.PushService;

namespace HomeCare_4_0.DAL
{
    public class cb_HomeServiceAPI
    {
        public cb_HomeServiceAPI(string Project_code, string Unit_Address, string Unit_Code, string Message)
        {
            cNotificationAPIResult NotificationAPI = new cNotificationAPIResult();
            NotificationAPI.ssr_proj_id = Project_code;
            NotificationAPI.address = Unit_Address;
            NotificationAPI.unitcode_sap = Unit_Code;
            NotificationAPI.message = Message;

            //DebugMode 
            if (System.Configuration.ConfigurationManager.AppSettings["DebugMode"] == "1")
            {
                NotificationAPI.ssr_proj_id = "9998";
                NotificationAPI.address = "9998/1";
                NotificationAPI.unitcode_sap = "9998-1";
            }

            UpdateStatusNotification(NotificationAPI);

        }

        public void UpdateStatusNotification(cNotificationAPIResult NotificationAPI)
        {
            DateTime RequestTime = DateTime.Now;
            string Service_Name = System.Configuration.ConfigurationManager.AppSettings["Service_Name"];
            object Result = null;
            string SW_write = string.Empty;
            SaveLog ClsSaveLog = new SaveLog();
            var json = new JavaScriptSerializer().Serialize(new
            {
                message = NotificationAPI.message,
                ssr_proj_id = NotificationAPI.ssr_proj_id,
                address = NotificationAPI.address,
                unitcode_sap = NotificationAPI.unitcode_sap
            });
            SW_write = json.ToString();
            

            try
            {
                
                string urlAPI = System.Configuration.ConfigurationManager.AppSettings["UrlApiNotification"];
                WebRequest myRequest = WebRequest.Create(urlAPI);
                myRequest.Credentials = CredentialCache.DefaultCredentials;
                myRequest.Method = "POST";
                myRequest.ContentType = "application/json";
                
                using (StreamWriter sw = new StreamWriter(myRequest.GetRequestStream()))
                {
                    sw.Write(SW_write);
                }
                HttpWebResponse response = (HttpWebResponse)myRequest.GetResponse();
                using (StreamReader responseReader = new StreamReader(response.GetResponseStream()))
                {
                    Result = responseReader.ReadToEnd();
                    ResponseService resService = GetResponseService(Result);
                    if (resService.status == (int)EnumConfiguration.ResponseCode.Success)
                    {
                        ClsSaveLog.WriteLog(Service_Name, "Call method UpdateStatusNotification()", RequestTime, DateTime.Now, EnumConfiguration.ResponseCode.Success.ToString(), "Success", SW_write, Result.ToString(), "", 0, "");
                    }
                    else if (resService.status == (int)EnumConfiguration.ResponseCode.Unsuccess)
                    {
                        ClsSaveLog.WriteLog(Service_Name, "Call method UpdateStatusNotification()", RequestTime, DateTime.Now, EnumConfiguration.ResponseCode.Unsuccess.ToString(), "Error", SW_write, Result.ToString(), "", 0, "");
                    }
                    else if (resService.status == (int)EnumConfiguration.ResponseCode.NotFound)
                    {
                        ClsSaveLog.WriteLog(Service_Name, "Call method UpdateStatusNotification()", RequestTime, DateTime.Now, EnumConfiguration.ResponseCode.NotFound.ToString(), "NotFound", SW_write, Result.ToString(), "", 0, "");
                    }
                }
            }
            catch (Exception ex)
            {
                dynamic msg = ex.Message + ex.StackTrace;
                ClsSaveLog.WriteLog(Service_Name, "Call method UpdateStatusNotification()", RequestTime, DateTime.Now, EnumConfiguration.ResponseCode.Unsuccess.ToString(), "Error exception", SW_write, "", msg.ToString(), 0, "");
            }
        }

        public ResponseService GetResponseService(object param)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            ResponseService clsreturn = new ResponseService();
            clsreturn = jss.Deserialize<ResponseService>(param.ToString());
            return clsreturn;
        }

        public struct ResponseService
        {
            public int status;
            public string message;
        }

        
    }

    public class cNotificationAPIResult
    {
        public string message { get; set; }
        public string ssr_proj_id { get; set; }
        public string address { get; set; }
        public string unitcode_sap { get; set; }
    }

    public class SaveLog
    {

        
        /// <summary>
        /// Save log (ค่าไหนไม่มี set เป็น ค่าว่างก็ได้)
        /// </summary>
        public bool WriteLog(string Service_Name, string Description, DateTime RequestTime, DateTime ResponseTime, string Return_Code, string Return_Message, string Input_Param, string Output_Data, string Query, int ReferenceId = 0, string ReferenceIdAll = "")
        {

            try
            {
                string strHostName = System.Net.Dns.GetHostName();
                string strIPAddress = GetIPAddress();
                LogAppEntities contextLogApp = new LogAppEntities();
                int result = contextLogApp.usp_homeservice_SaveLog_WS(
                    Service_Name, Description, RequestTime, ResponseTime, Return_Code, Return_Message
                    , Input_Param, Output_Data, Query, strHostName, strIPAddress, ReferenceId, ReferenceIdAll);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public static string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string sIPAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            
            if (string.IsNullOrEmpty(sIPAddress))
            {
                return context.Request.ServerVariables["REMOTE_ADDR"];
            }
            else
            {
                string[] ipArray = sIPAddress.Split(new Char[] { ',' });
                return ipArray[0];
            }
        }
    }


    public class cb_Notification
    {

        public void NotificationMobileApprove(cNotificationMobileApproveResult NotificationAPI)
        {

            try
            {
                if (NotificationAPI.email != "")
                {
                    PushService.PushService ws = new PushService.PushService();
                    PushData data = new PushData();
                    data.process = NotificationAPI.process;
                    data.doctype = NotificationAPI.doctype;
                    data.intno = NotificationAPI.intno;
                    data.email = NotificationAPI.email;
                    data.cnttype = "";
                    data.gsber = "";
                    string result = ws.SendNotification(data);
                }
            }
            catch (Exception ex)
            {

            }
        }

    }

    public class cNotificationMobileApproveResult
    {
        public string process { get; set; }
        public string intno { get; set; }
        public string gsber { get; set; }
        public string dept { get; set; }
        public string cnttype { get; set; }
        public string doctype { get; set; }
        public string intitem { get; set; }
        public string email { get; set; }
    }


}