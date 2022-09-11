using HomeCare_4_0.Common;
using HomeCare_4_0.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace HomeCare_4_0.DAL
{
    public class cAuthorization
    {

        private HomeCareDBEntities context;


        public cAuthorization()
        {
            this.context = new HomeCareDBEntities();
        }

        public bool checkLogin(string Username, string Password, string Email, string Icontact_userID)
        {
            List<WA_HC_CheckLogin_Result> info;
            if (Email != string.Empty) {
                info = context.WA_HC_CheckLogin(Username, Password, Email).ToList();
            }
            else { 
                info = context.WA_HC_CheckLogin(Username, EncryptPWD(Password), Email).ToList(); }
        
            if (info.Count == 0)
            {
                return false;
            }
            else
            {
                if (Username != "siriCallcenter")
                    HttpContext.Current.Session["userInfo"] = info;
                else
                {
                    HttpContext.Current.Session["userInfo"] = info.Where(x=>x.email.ToUpper()== Email.ToUpper()).ToList();
                    HttpContext.Current.Session["Icontact_userID"] = string.IsNullOrEmpty(Icontact_userID) ? "A1" : Icontact_userID;

                    //string.IsNullOrEmpty(Icontact_userID) ? 'A1' : Icontact_userID ;
                }
                return true;
            }
        }
        public string EncryptPWD(string strSender)
        {
            MemoryStream encryptedPassword = new MemoryStream();
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = Encoding.ASCII.GetBytes("Tracking");

            byte[] iv = { 11, 12, 33, 32, 24, 35, 160, 84 };

            des.IV = iv;

            ICryptoTransform myEncryptor = des.CreateEncryptor();

            CryptoStream myCryptoStream = new CryptoStream(encryptedPassword, myEncryptor, CryptoStreamMode.Write);

            byte[] pwd = Encoding.ASCII.GetBytes(strSender);

            myCryptoStream.Write(pwd, 0, pwd.Length);
            myCryptoStream.Close();

            return Convert.ToBase64String(encryptedPassword.ToArray());

        }
		//    Public Function EncryptPWD(ByVal strSender As String) As String
		//    Dim encryptedPassword As New MemoryStream
		//    Dim des As New DESCryptoServiceProvider
		//    des.Key = Encoding.ASCII.GetBytes("Tracking")
		//    Dim iv() As Byte = { 11, 12, 33, 32, 24, 35, 160, 84 }
		//    des.IV = iv
		//    Dim myEncryptor As ICryptoTransform = des.CreateEncryptor()
		//    Dim myCryptoStream As New CryptoStream(encryptedPassword, myEncryptor, CryptoStreamMode.Write)
		//    Dim pwd() As Byte = Encoding.ASCII.GetBytes(strSender)
		//    myCryptoStream.Write(pwd, 0, pwd.Length)
		//    myCryptoStream.Close()
		//    EncryptPWD = Convert.ToBase64String(encryptedPassword.ToArray())
		//End Function
		//Public Function DecryptPassword(ByVal strSender As String) As String
		//    Dim decryptedPassword As New MemoryStream
		//    Dim des As New DESCryptoServiceProvider
		//    des.Key = Encoding.ASCII.GetBytes("Tracking")
		//    Dim iv() As Byte = { 11, 12, 33, 32, 24, 35, 160, 84 }
		//    des.IV = iv
		//    Dim myDecryptor As ICryptoTransform = des.CreateDecryptor()
		//    Dim myCryptoStream As New CryptoStream(decryptedPassword, myDecryptor, CryptoStreamMode.Write)
		//    Dim encryptedPassword() As Byte = Convert.FromBase64String(strSender)
		//    myCryptoStream.Write(encryptedPassword, 0, encryptedPassword.Length)
		//    myCryptoStream.Close()
		//    DecryptPassword = Encoding.ASCII.GetString(decryptedPassword.ToArray())
		//End Function
		public List<HC_SP_GET_PERMISSION_Result> getPermission(long userId)
		{
			return context.HC_SP_GET_PERMISSION(userId).ToList();
		}
	}
}