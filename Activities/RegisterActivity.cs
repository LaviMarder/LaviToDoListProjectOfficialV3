using Android.App;
using Android.Content;
using Android.Gms.Extensions;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Firestore;
using Java.Util;
using LaviToDoListProjectOfficialV3.Helpers;
using LaviToDoListProjectOfficialV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaviToDoListProjectOfficialV3.Activities
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {
        EditText etMailReg, etUsernameReg, etFullNameReg, etPasswordReg;
        Button btnRegisterReg;
        FbData fbd;
        UserDataProcessor user;

        HashMap ha;
        string uid;
        public static string id;

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RegisterLayout);

            // Create your application here

            InItObject();
            InitViews();
        }

        private void InItObject()
        {
            fbd = new FbData();
            user = new UserDataProcessor();
        }

        private void InitViews()
        {
            // Sync the variable names with XML IDs
            etMailReg = FindViewById<EditText>(Resource.Id.etMailReg); // Updated ID
            etPasswordReg = FindViewById<EditText>(Resource.Id.etPasswordReg); // Updated ID
            etFullNameReg = FindViewById<EditText>(Resource.Id.etFullNameReg);
            etUsernameReg = FindViewById<EditText>(Resource.Id.etUsernameReg);

            btnRegisterReg = FindViewById<Button>(Resource.Id.btnRegisterReg);
            btnRegisterReg.Click += BtnRegisterReg_Click;
        }


        private void BtnRegisterReg_Click(object? sender, EventArgs e)
        {
            SaveDocument(); //בדיקות תקינות קלט
        }

        private async void SaveDocument()
        {//עי
            if (await RegisterData(etMailReg.Text, etPasswordReg.Text, etFullNameReg.Text, etUsernameReg.Text))
            {
                Toast.MakeText(this, "Reggister Successfully", ToastLength.Short).Show();
                etMailReg.Text = "";
                etPasswordReg.Text = "";
                etFullNameReg.Text = "";
                etUsernameReg.Text = "";
                //Intent intent = new Intent(this, typeof(MainActivity));
                //StartActivity(intent);

            }
            else
            {
                Toast.MakeText(this, "Register Failed", ToastLength.Short).Show();
            }
        }//

        private async Task<bool> RegisterData(string Mail, string Password, string FullName, string UserName)
        {
            try
            {
                await fbd.auth.CreateUserWithEmailAndPassword(Mail, Password);
                uid = fbd.auth.CurrentUser.Uid;
                HashMap userMap = new HashMap();
                userMap.Put(General.KEY_FULLNAME, FullName);
                userMap.Put(General.KEY_MAIL, Mail);
                userMap.Put(General.KEY_USERNAME, UserName);
                userMap.Put(General.KEY_PASS, Password);
                userMap.Put(General.KEY_ID, uid);
                DocumentReference userReference = fbd.firestore.Collection(General.FS_COLLECTION).Document(uid);
                await userReference.Set(userMap);
            }
            catch (Exception e)
            {
                Toast.MakeText(this, "Failed", ToastLength.Short).Show();
                return false;
            }
            return true;

        }
    }

}