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
    internal class TaskDataProcessor
    {

        public string TaskId { get; set; }
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public string TaskUserId { get; set; }
        public string Deadline { get; set; }  // Deadline is now a string
        public int TimeEstimate { get; set; }  // Time estimate is now an integer (e.g., 1 for short, 2 for medium, 3 for long)
        public int ImportanceLevel { get; set; }  // Importance level is now an integer (e.g., 1 for low, 2 for medium, 3 for high)

        // Constructor with the new integer values for TimeEstimate and ImportanceLevel
        public TaskDataProcessor(string taskId, string taskTitle, string? taskDescription, string? deadline, int timeEstimate, int importanceLevel, string taskUserId)
        {
            TaskId = taskId;
            TaskTitle = taskTitle;
            TaskDescription = string.IsNullOrWhiteSpace(taskDescription) ? "No description" : taskDescription;
            Deadline = string.IsNullOrWhiteSpace(deadline) ? "No date set" : deadline;

            TimeEstimate = timeEstimate;  
            
            ImportanceLevel = importanceLevel;
            TaskUserId = taskUserId;
        }

        public TaskDataProcessor()
        {
            TaskId = string.Empty;  // Generate a unique TaskId
            TaskTitle = string.Empty;  // Default to an untitled task
            TaskDescription = "No description";  // Default description
            Deadline = "No date set";  // Default to no deadline set
            TimeEstimate = 2;  // Default to Medium time estimate
            ImportanceLevel = 2;  // Default to Medium importance level
            TaskUserId = string.Empty;
        }
    }
}
