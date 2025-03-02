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
    [Activity(Label = "AddTaskActivity")]
    public class AddTaskActivity : Activity
    {
        EditText etTaskTitle, etTaskDescription, etDeadline, etTimeEstimate, etImportanceLevel;
        Button btnAddTask;

        FbData fbd;
        TaskDataProcessor task;

        HashMap ha;
        string tid;
        public static string id;

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddTaskLayout);

            // Create your application here

            InItObject();
            InitViews();
        }

        private void InItObject()
        {
            fbd = new FbData();
            task = new TaskDataProcessor();
        }

        private void InitViews()
        {
            // Sync the variable names with XML IDs for task-related fields
            etTaskTitle = FindViewById<EditText>(Resource.Id.etTaskTitle); // Task title field
            etTaskDescription = FindViewById<EditText>(Resource.Id.etTaskDescription); // Task description field
            etDeadline = FindViewById<EditText>(Resource.Id.etDeadline); // Deadline field
            etTimeEstimate = FindViewById<EditText>(Resource.Id.etTimeEstimate); // Time estimate field
            etImportanceLevel = FindViewById<EditText>(Resource.Id.etImportanceLevel); // Importance level field

            // Sync the button variable with XML ID
            btnAddTask = FindViewById<Button>(Resource.Id.btnAddTask); // Button for adding task
            btnAddTask.Click += BtnAddTask_Click; // Event handler for button click
        }

        private void BtnAddTask_Click(object sender, EventArgs e)
        {
            SaveDocument(); //בדיקות תקינות קלט
        }



        private async void SaveDocument()
        {
            if (await AddTaskData(etTaskTitle.Text, etTaskDescription.Text, etDeadline.Text, etTimeEstimate.Text, etImportanceLevel.Text))
            {
                Toast.MakeText(this, "Task Added Successfully", ToastLength.Short).Show();
                // Clear the input fields after adding the task
                etTaskTitle.Text = "";
                etTaskDescription.Text = "";
                etDeadline.Text = "";
                etTimeEstimate.Text = "";
                etImportanceLevel.Text = "";
            }
            else
            {
                Toast.MakeText(this, "Task Addition Failed", ToastLength.Short).Show();
            }
        }


        private async Task<bool> AddTaskData(string taskTitle, string taskDescription, string deadline, string timeEstimate, string importanceLevel)
        {
            try
            {
                // Generate task ID (you can use a unique identifier or Firestore auto-ID)
                string taskId =fbd.GetNewDocumentId(General.FS_TASK_COLLECTION);

                // Create a map to store the task data
                HashMap taskMap = new HashMap();
                taskMap.Put("TaskTitle", taskTitle);
                taskMap.Put("TaskDescription", string.IsNullOrWhiteSpace(taskDescription) ? "No description" : taskDescription);
                taskMap.Put("Deadline", string.IsNullOrWhiteSpace(deadline) ? "No date set" : deadline);
                taskMap.Put("TimeEstimate", timeEstimate);  // Assuming it's already in a suitable format (e.g., "1", "2", "3")
                taskMap.Put("ImportanceLevel", importanceLevel);  // Similar to timeEstimate
                taskMap.Put("taskId",taskId);
                // Reference to Firestore collection where the task will be stored
                DocumentReference taskRef = fbd.firestore.Collection(General.FS_TASK_COLLECTION).Document(taskId);

                // Set task data in Firestore
                await taskRef.Set(taskMap);

                // Return true if the task is added successfully
                return true;
            }
            catch (Exception e)
            {
                // Show an error message if something goes wrong
                Toast.MakeText(this, "Failed to add task", ToastLength.Short).Show();
                return false;
            }
        }

    }
}