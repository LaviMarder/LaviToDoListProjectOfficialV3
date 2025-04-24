using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LaviToDoListProjectOfficialV3.Activities;
using LaviToDoListProjectOfficialV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaviToDoListProjectOfficialV3.Helpers
{
    internal class TaskAdapter : BaseAdapter<TaskDataProcessor>
    {

        Context context;
        private List<TaskDataProcessor> lstTasks;
        private bool v;//לא סופי
        public TaskAdapter(Context context)
        {
            this.context = context;
        }
        public TaskAdapter(Context context, List<TaskDataProcessor> lstTasks, bool v)
        {
            this.lstTasks = lstTasks;
            this.context = context;
            this.v = v;


        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater layoutInflater;
            layoutInflater = ((TaskListActivity)context).LayoutInflater;
            View view = layoutInflater.Inflate(Resource.Layout.TaskRowLayout, parent, false);
            TextView TaskName = view.FindViewById<TextView>(Resource.Id.TaskTitleRow);
            TaskDataProcessor task = lstTasks[position];
            int level;
            if (task != null)
            {
                TaskName.Text = task.TaskTitle;
                level = task.ImportanceLevel;
            }

            return view;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return lstTasks.Count;
            }
        }

        public override TaskDataProcessor this[int position] => lstTasks[position];
    }

    //internal class TaskAdapterViewHolder : Java.Lang.Object
    //{
    //    //Your adapter views to re-use
    //    //public TextView Title { get; set; }
    //}
}