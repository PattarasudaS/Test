using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Mail;


namespace HomeCare_4_0.Common
{
    public class cCommon
    {
        public static string MailServerIP
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["MailServerIP"].ToString();
            }
        }
        public static DateTime? ddMMyyyyToyyyyMMdd(string date, char split)
        {
            if(date.Length>10)
            {
                date = date.Substring(0, 10);
            }
            string[] convertDate = date.Split(split);

            DateTime newDate = DateTime.Parse(convertDate[2] + split + convertDate[1] + split + convertDate[0]);

            return newDate;
        }

        public static string SendEmail(string strFrom, string strTo, string strCC, string strBCC, string strSubject, string strBody, Object Priority)
        {
            System.Web.Mail.MailMessage objEmail = new System.Web.Mail.MailMessage(); ;
            string result = string.Empty;
            if (string.IsNullOrEmpty(strFrom))
            {
                strFrom = "homecaresystemadmin@sansiri.com";
            }

            try
            {
                objEmail.From = strFrom;
                objEmail.To = strTo;
                objEmail.Cc = strCC;
                objEmail.Bcc = strBCC;
                objEmail.Subject = strSubject;
                objEmail.Body = strBody;
                objEmail.Priority = System.Web.Mail.MailPriority.High;
                objEmail.BodyFormat = MailFormat.Html;
                objEmail.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");

                SmtpMail.SmtpServer = MailServerIP;
                SmtpMail.Send(objEmail);

                result = Cls_HC_Message.SendMail_Success;
            }
            catch (Exception ex)
            {
                result = Cls_HC_Message.SendMail_Filed + "(" + ex.Message.ToString() + ")";
            }
            return result;
        }

        public static string SendNewEmail(string strFrom, string strTo, string strCC, string strBCC, string strSubject, string strBody, Object Priority)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(strFrom))
            {
                strFrom = "homecaresystemadmin@sansiri.com";
            }

            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage(strFrom, strTo, strSubject, strBody)
            {
                Subject = strSubject,
                SubjectEncoding = System.Text.Encoding.UTF8,
                Body = strBody,
                IsBodyHtml = true
            };
            if (!string.IsNullOrEmpty(strCC))
            {
                mailMessage.CC.Add(strCC);
            }
            if (!string.IsNullOrEmpty(strBCC))
            {
                mailMessage.Bcc.Add(strBCC);
            }

            SmtpClient client = new SmtpClient
            {
                Host = MailServerIP,
            };

            try
            {
                client.Send(mailMessage);

                result = Cls_HC_Message.SendMail_Success;
            }
            catch (Exception ex)
            {
                result = Cls_HC_Message.SendMail_Filed + "(" + ex.Message.ToString() + ")";
            }
            return result;
        }

    }
}