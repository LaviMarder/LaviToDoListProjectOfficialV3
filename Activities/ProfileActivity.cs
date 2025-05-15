using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Firestore.Auth;
using Firebase.Firestore;
using LaviToDoListProjectOfficialV3.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Gms.Tasks;
using LaviToDoListProjectOfficialV3.Models;
using Android.Gms.Extensions;

namespace LaviToDoListProjectOfficialV3.Activities
{
    [Activity(Label = "ProfileActivity")]
    public class ProfileActivity : Activity, IEventListener, IOnSuccessListener
    {
        TextView tvMailPro, tvUsernamePro, tvFullNamePro;

        List<UserDataProcessor> lstUsers;

        Button btnOk;
        FbData fbd;

        string name = string.Empty;
        UserDataProcessor user;
        string uid;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ProfileLayout);
            uid = Intent.GetStringExtra("uid");
            InitObjects();

            InitViews();
            DoProfileAsync();
        }

        private async void DoProfileAsync()
        {
            await fbd.GetCollection(General.FS_COLLECTION, uid).AddOnSuccessListener(this);
        }

        private void InitViews()
        {
            tvMailPro = FindViewById<TextView>(Resource.Id.tvMailPro);
            tvUsernamePro = FindViewById<TextView>(Resource.Id.tvUsernamePro);
            tvFullNamePro = FindViewById<TextView>(Resource.Id.tvFullNamePro);

        }

        private void InitObjects()
        {
            user = new UserDataProcessor();
            fbd = new FbData();
            fbd.AddCollectionSnapShotListener(this, General.FS_COLLECTION);

        }


        private void PrintUser(UserDataProcessor user)
        {
            tvMailPro.Text = user.UserMail;
            tvUsernamePro.Text = user.UserUserName;
            tvFullNamePro.Text = user.UserFullName;
   
        }

        public void OnEvent(Java.Lang.Object value, FirebaseFirestoreException error)
        {
            Toast.MakeText(this, "event", ToastLength.Long).Show();
        }



        public void OnSuccess(Java.Lang.Object result)
        {
            var snapshot = (DocumentSnapshot)result;
            //     public UserDataProcessor(string userId, string userMail, string userUserName, string userFullName, string userPassword)
            string id, mail, username, fullName, password;
            id = snapshot.Id;
            mail = snapshot.Get("UserMail").ToString();
            username = snapshot.Get("UserUserName").ToString();
            fullName = snapshot.Get("UserUserName").ToString();
            password = snapshot.Get("UserPassword").ToString();
            int a = 5;
            user = new UserDataProcessor(id, mail, username, fullName, password);


            PrintUser(user);
        }
    }
}