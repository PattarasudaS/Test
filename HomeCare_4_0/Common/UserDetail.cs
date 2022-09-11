using HomeCare_4_0.ClassLib;
using HomeCare_4_0.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HomeCare_4_0.Common
{
    public class UserDetail
    {
        public static string Fullname
        {
            get
            {
                string session = string.Empty;
                if (HttpContext.Current.Session["Fullname"]==null)
                {
                    var  listuserInfo = HttpContext.Current.Session["userInfo"];
                    List<WA_HC_CheckLogin_Result> userDetail = (List<WA_HC_CheckLogin_Result>)listuserInfo;
                    session = userDetail.FirstOrDefault().firstName + ' ' + userDetail.FirstOrDefault().lastName;
                    HttpContext.Current.Session["Fullname"] = session;
                }
                else if (HttpContext.Current.Session["Fullname"] != null)
                {
                    session = HttpContext.Current.Session["Fullname"].ToString();
                }
                return session;
            }
        }

        public static string FullnameTH
        {
            get
            {
                string session = string.Empty;
                if (HttpContext.Current.Session["FullnameTH"] == null)
                {
                    var listuserInfo = HttpContext.Current.Session["userInfo"];
                    List<WA_HC_CheckLogin_Result> userDetail = (List<WA_HC_CheckLogin_Result>)listuserInfo;
                    session = userDetail.FirstOrDefault().firstNameth + ' ' + userDetail.FirstOrDefault().lastNameth;
                    HttpContext.Current.Session["FullnameTH"] = session;
                }
                else if (HttpContext.Current.Session["FullnameTH"] != null)
                {
                    session = HttpContext.Current.Session["FullnameTH"].ToString();
                }
                return session;
            }
        }

        public static long UserID
        {
            get
            {
                long session=0;
                if (HttpContext.Current.Session["UserID"] == null)
                {
                    var listuserInfo = HttpContext.Current.Session["userInfo"];
                    List<WA_HC_CheckLogin_Result> userDetail = (List<WA_HC_CheckLogin_Result>)listuserInfo;
                    session = userDetail.FirstOrDefault().User_id;
                    HttpContext.Current.Session["UserID"] = session;
                }
                else if (HttpContext.Current.Session["UserID"] != null)
                {
                    session = (long)HttpContext.Current.Session["UserID"];
                }
                return session;
            }
        }
        public static string CodeName
        {
            get
            {
                string session = string.Empty;
                if (HttpContext.Current.Session["CodeName"] == null)
                {
                    var listuserInfo = HttpContext.Current.Session["userInfo"];
                    List<WA_HC_CheckLogin_Result> userDetail = (List<WA_HC_CheckLogin_Result>)listuserInfo;
                    session = userDetail.FirstOrDefault().User_Code;
                    HttpContext.Current.Session["CodeName"] = session;
                }
                else if (HttpContext.Current.Session["CodeName"] != null)
                {
                    session = HttpContext.Current.Session["CodeName"].ToString();
                }
                return session;
            }
        }
        public static string Role
        {
            get
            {
                string session = string.Empty;
                if (HttpContext.Current.Session["Role"] == null)
                {
                    var listuserInfo = HttpContext.Current.Session["userInfo"];
                    List<WA_HC_CheckLogin_Result> userDetail = (List<WA_HC_CheckLogin_Result>)listuserInfo;
                    session =new HomeCare_4_0.Models.HomeCareDBEntities().TK_V_UserAcctRole.Where(x => x.UserCode == CodeName).FirstOrDefault().role;
                    HttpContext.Current.Session["Role"] = session;
                }
                else if (HttpContext.Current.Session["Role"] != null)
                {
                    session = HttpContext.Current.Session["Role"].ToString();
                }
                return session;
            }
        }
        public static string Email
        {
            get
            {
                string session = string.Empty;
                if (HttpContext.Current.Session["Email"] == null)
                {
                    var listuserInfo = HttpContext.Current.Session["userInfo"];
                    List<WA_HC_CheckLogin_Result> userDetail = (List<WA_HC_CheckLogin_Result>)listuserInfo;
                    session = userDetail.FirstOrDefault().email;
                    HttpContext.Current.Session["Email"] = session;
                }
                else if (HttpContext.Current.Session["Email"] != null)
                {
                    session = HttpContext.Current.Session["Email"].ToString();
                }
                return session;
            }
        }

        public static long AllPJ
        {
            get
            {
                long session = 0;
                if (HttpContext.Current.Session["AllPJ"] == null)
                {
                    var listuserInfo = HttpContext.Current.Session["userInfo"];
                    List<WA_HC_CheckLogin_Result> userDetail = (List<WA_HC_CheckLogin_Result>)listuserInfo;
                    session = userDetail.FirstOrDefault().AllProjectFlag?1:0;
                    HttpContext.Current.Session["AllPJ"] = session;
                }
                else if (HttpContext.Current.Session["AllPJ"] != null)
                {
                    session = (long)HttpContext.Current.Session["AllPJ"];
                }
                return session;
            }
        }

        public static string Icontact_userID
        {
            get
            {
                string session = string.Empty;
                if (HttpContext.Current.Session["Icontact_userID"] == null)
                {
                    HttpContext.Current.Session["Icontact_userID"] = 0;
                }
                else if (HttpContext.Current.Session["Icontact_userID"] != null)
                {
                    session = HttpContext.Current.Session["Icontact_userID"].ToString();
                }
                return session;
            }
        }
        public static string VendorCode
        {
            get
            {
                string session = string.Empty;
                if (HttpContext.Current.Session["VendorCode"] == null)
                {
                    var listuserInfo = HttpContext.Current.Session["userInfo"];
                    List<WA_HC_CheckLogin_Result> userDetail = (List<WA_HC_CheckLogin_Result>)listuserInfo;
                    session = userDetail.FirstOrDefault().Vendor_Code;
                    HttpContext.Current.Session["VendorCode"] = session;
                }
                else if (HttpContext.Current.Session["VendorCode"] != null)
                {
                    session = HttpContext.Current.Session["VendorCode"].ToString();
                }
                return session;
            }
        }
        public static string Permisson
        {
            get
            {
                string session = string.Empty;
                if (HttpContext.Current.Session["Permisson"] == null)
                {
                    var listuserInfo = HttpContext.Current.Session["userInfo"];
                    List<WA_HC_CheckLogin_Result> userDetail = (List<WA_HC_CheckLogin_Result>)listuserInfo;
                    session = userDetail.Where(x => x.Profile == EnumConfiguration.Role.AdminHC.ToString()).Count() > 0 
                                                                ? EnumConfiguration.Role.AdminHC.ToString() : string.Empty;
                    HttpContext.Current.Session["Permisson"] = session;
                }
                else if (HttpContext.Current.Session["Permisson"] != null)
                {
                    session = HttpContext.Current.Session["Permisson"].ToString();
                }
                return session;
            }
        }
        public static void clearSession()
        {
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session.Clear();

        }

        public static void clearAllCookie()
        {
            if (HttpContext.Current != null)
            {
                int cookieCount = HttpContext.Current.Request.Cookies.Count;
                for (var i = 0; i < cookieCount; i++)
                {
                    var cookie = HttpContext.Current.Request.Cookies[i];
                    if (cookie != null)
                    {
                        var cookieName = cookie.Name;
                        var expiredCookie = new HttpCookie(cookieName) { Expires = DateTime.Now.AddDays(-1) };
                        HttpContext.Current.Response.Cookies.Add(expiredCookie);
                    }
                }
                HttpContext.Current.Request.Cookies.Clear();
            }

        }
    }
}