using System;
using System.Security;
using Microsoft.Exchange.WebServices.Data;

namespace ExchangeAuthentication
{
    public interface IUserData
    {
        ExchangeVersion Version { get; }
        string EmailAddress { get; }
        string PlainPassword { get; }
        SecureString Password { get; }
        Uri AutodiscoverUrl { get; set; }

    }

    public class UserDataAuthen : IUserData
    {
        public static UserDataAuthen UserData;

        public static IUserData GetUserData()
        {
            if (UserData == null)
            {
                GetUserDataAuthen();
            }

            return UserData;
        }

        private static void GetUserDataAuthen()
        {
            UserData = new UserDataAuthen();

            //Console.Write("Enter email address: ");
            UserData.EmailAddress = Properties.Settings.Default.HCEmail.ToString();
            UserData.PlainPassword = Properties.Settings.Default.Password.ToString();
            //UserData.AutodiscoverUrl = new Uri(Properties.Settings.Default.EWSService.ToString());

            UserData.Password = new SecureString();


            ///string pwd = "Sansiri.com";
            char[] pwdChar = UserData.PlainPassword.ToCharArray();

            foreach (char password in pwdChar)
            {
                UserData.Password.AppendChar(password);
            }
            UserData.Password.MakeReadOnly();
        }

        public ExchangeVersion Version { get { return ExchangeVersion.Exchange2013; } }

        public string EmailAddress
        {
            get;
            private set;
        }

        public SecureString Password
        {
            get;
            private set;
        }

        public Uri AutodiscoverUrl
        {
            get;
            set;
        }
        public string PlainPassword
        {
            get;
            private set;
        }
        
    }
}
