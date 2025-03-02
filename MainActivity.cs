using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using LaviToDoListProjectOfficialV3.Activities;
using System;

namespace LaviToDoListProjectOfficialV3
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Button btnLoginMain, btnRegisterMain;

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            InitViews();
        }

        private void InitViews()
        {
            // Syncing IDs with XML
            btnLoginMain = FindViewById<Button>(Resource.Id.btnLoginMain);
            btnLoginMain.Click += BtnLoginMain_Click;

            btnRegisterMain = FindViewById<Button>(Resource.Id.btnRegisterMain);
            btnRegisterMain.Click += btnRegisterMain_Click;
        }

        private void BtnLoginMain_Click(object? sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(AddTaskActivity));
            StartActivity(intent);
        }

        private void btnRegisterMain_Click(object? sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(RegisterActivity));
            StartActivity(intent);
        }
    }
}