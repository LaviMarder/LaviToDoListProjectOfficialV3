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
using LaviToDoListProjectOfficialV3.Models;
using System.Threading.Tasks;
using Android.Gms.Tasks;
using Android.Gms.Extensions;

namespace LaviToDoListProjectOfficialV3.Activities
{
    [Activity(Label = "TaskListActivity")]
    public class TaskListActivity : Activity, IOnCompleteListener, IEventListener
    {
        ListView lvTasksList;
        List<TaskDataProcessor> lstTasks;
        TaskAdapter ta;
        FbData fbd;

        string name = string.Empty;
        string uid;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            //  Toast.MakeText(this, "*****"+DocumentActivity.id.ToString(), ToastLength.Long).Show();
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.TaskListLayout);
            uid = Intent.GetStringExtra("uid");
            //Toast.MakeText(this, uid.ToString(), ToastLength.Long).Show();
            InitObjects();

            InitViews();
            GetTaskListAsync();
        }

        private async void GetTaskListAsync()
        {
            await fbd.GetCollection(General.FS_TASK_COLLECTION).AddOnCompleteListener(this);

        }


        //protected override void OnListItemClick(ListView l, View v, int position, long id)
        //{
        //    var t = lvUsers[position];
        //    Android.Widget.Toast.MakeText(this, t, Android.Widget.ToastLength.Short).Show();
        //}
        private void InitObjects()
        {

            //    Toast.MakeText(this, "ok", ToastLength.Long).Show();

            fbd = new FbData();
            fbd.AddCollectionSnapShotListener(this, General.FS_TASK_COLLECTION);

        }

        private void InitViews()
        {

            //  Toast.MakeText(this, uid, ToastLength.Long).Show();
            lvTasksList = FindViewById<ListView>(Resource.Id.lvTasksList);
            lvTasksList.ItemClick += LvTasks_ItemClick;
            lvTasksList.ItemLongClick += LvTasks_ItemLongClick;

        }

        private void LvTasks_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            TaskDataProcessor ts = lstTasks[e.Position];
            Toast.MakeText(this, ts.TaskId + "delete", ToastLength.Long).Show();

        }

        private void LvTasks_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            TaskDataProcessor ts = lstTasks[e.Position];
            Toast.MakeText(this, ts.TaskId + "update", ToastLength.Long).Show();
        }



        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful)
            {

                // Toast.MakeText(this, "ok", ToastLength.Long).Show();
                lstTasks = GetDocuments2((QuerySnapshot)task.Result);
                if (lstTasks.Count != 0)
                {


                    Toast.MakeText(this, "ok", ToastLength.Long).Show();

                }
                else
                    Toast.MakeText(this, "empty", ToastLength.Long).Show();

            }
        }

        private List<TaskDataProcessor> GetDocuments2(QuerySnapshot result)
        {
            lstTasks = new List<TaskDataProcessor>();

            Toast.MakeText(this, "okgetdocument", ToastLength.Long).Show();
            foreach (DocumentSnapshot item in result.Documents)
            {
                TaskDataProcessor myTask = new TaskDataProcessor
                {
                    TaskId = item.Id,
                    TaskTitle = item.Get("TaskTitle").ToString(),
                    //ImportanceLevel = int.Parse(item.Get("ImportanceLevel").ToString()),

                };
                if (item.Get("TaskUserId").ToString() == uid)
                {
                    lstTasks.Add(myTask);
                }


            }
            ta = new TaskAdapter(this, lstTasks, true);
            lvTasksList.Adapter = ta;
            return lstTasks;
        }




        public void OnEvent(Java.Lang.Object value, FirebaseFirestoreException error)
        {
            Toast.MakeText(this, "event", ToastLength.Long).Show();
        }


    }
}