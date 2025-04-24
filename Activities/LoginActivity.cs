using Android.App;
using Android.Content;
using Android.Gms.Extensions;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LaviToDoListProjectOfficialV3.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaviToDoListProjectOfficialV3.Activities
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity
    {
        EditText etMailLog, etPasswordLog;
        Button btnLoginLog;
        FbData fbd;

        string uid;


        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LoginLayout);
            InitObjects();
            InitViews();
        }

        private void InitObjects()
        {
            fbd = new FbData();
        }

        private void InitViews()
        {
            etMailLog = FindViewById<EditText>(Resource.Id.etMailLog);
            etPasswordLog = FindViewById<EditText>(Resource.Id.etPasswordLog);
            btnLoginLog = FindViewById<Button>(Resource.Id.btnLoginLog);
            btnLoginLog.Click += btnLoginLog_Click;
        }
        private async void btnLoginLog_Click(object? sender, EventArgs e)
        {
            if (await LoginUser(etMailLog.Text, etPasswordLog.Text))
            {
                Toast.MakeText(this, "Logged In Successfully", ToastLength.Short).Show();
                etMailLog.Text = " "; 
                etPasswordLog.Text = " ";
                Intent intent = new Intent(this, typeof(HomePageActivity));
                intent.PutExtra("uid", uid);
                StartActivity(intent);
            }
            else
            {
                Toast.MakeText(this, "Login Failed", ToastLength.Short).Show();
            }
        }

        public async Task<bool> LoginUser(string email, string password)
        {
            try
            {
                await fbd.auth.SignInWithEmailAndPassword(email, password);
                uid = fbd.auth.CurrentUser.Uid;
            }
            catch (System.Exception ex)
            {
                string s = ex.Message;
                return false;
            }
            return true;
        }


    }
}