using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LaviToDoListProjectOfficialV3.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Android.Views.View;
using static Xamarin.Essentials.Platform;
using Intent = Android.Content.Intent;

namespace LaviToDoListProjectOfficialV3.Activities
{
    [Activity(Label = "HomePageActivity")]
    public class HomePageActivity : Activity, IOnClickListener
    {
        Button btnTaskListHome, btnProfileHome, btnAddTaskHome;
        FbData fbd;

        string uid;



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.HomePageLayout);
            uid = Intent.GetStringExtra("uid");
            InitViews();


        }


        private void InitViews()
        {
            btnTaskListHome = FindViewById<Button>(Resource.Id.btnTaskListHome);
            btnProfileHome = FindViewById<Button>(Resource.Id.btnProfileHome);

            btnAddTaskHome = FindViewById<Button>(Resource.Id.btnAddTaskHome);

            btnTaskListHome.SetOnClickListener(this);
            btnProfileHome.SetOnClickListener(this);

            btnAddTaskHome.SetOnClickListener(this);

        }
        public void OnClick(View v)
        {
            if (v == btnTaskListHome)
            {
                Intent intent = new Intent(this, typeof(TaskListActivity));
                intent.PutExtra("uid", uid);

                StartActivity(intent);
            }
            else if (v == btnProfileHome)
            {
                Intent intent = new Intent(this, typeof(ProfileActivity));
                intent.PutExtra("uid", uid);

                StartActivity(intent);
            }
            else if (v == btnAddTaskHome)
            {
                Intent intent = new Intent(this, typeof(AddTaskActivity));
                intent.PutExtra("uid", uid);

                StartActivity(intent);
            }
            
        }
    }
}