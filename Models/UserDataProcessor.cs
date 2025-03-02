using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaviToDoListProjectOfficialV3.Models
{
    public class UserDataProcessor
    {
        public string UserId { get; set; }

        public string UserMail { get; set; }

        public string UserUserName { get; set; }

        public string UserFullName { get; set; }

        public string UserPassword { get; set; }


        public UserDataProcessor(string userId, string userMail, string userUserName, string userFullName, string userPassword)
        {
            UserId = userId;
            UserMail = userMail;
            UserUserName = userUserName;
            UserFullName = userFullName;
            UserPassword = userPassword;
        }

        public UserDataProcessor(string userId, string userUserName, string userMail, string userPassword)
        {
            UserId = userId;
            UserMail = userMail;
            UserUserName = userUserName;
            UserPassword = userPassword;
        }


        public UserDataProcessor()
        {
            UserId = string.Empty;
            UserMail = string.Empty;
            UserUserName = string.Empty;
            UserFullName = string.Empty;
            UserPassword = string.Empty;


        }
    }
}