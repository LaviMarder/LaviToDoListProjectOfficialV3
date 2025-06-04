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

namespace LaviToDoListProjectOfficialV3.Activities
{
    [Activity(Label = "NotificationsActivity")]
    public class NotificationsActivity : Activity
    {

        private Button btnMorningNotifications;
        int hour, minute;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.NotificationsLayout);

            btnMorningNotifications = FindViewById<Button>(Resource.Id.btnMorningNotifications);

            btnMorningNotifications.Click += SetAlarmButton_Click;

            // עבור אנדרואיד 8.0 (Oreo) ומעלה, חייבים ליצור ערוץ התראות
            CreateNotificationChannel();
        }

        private void CreateDialog()
        {
            // Get current time
            TimeSpan time = DateTime.Now.TimeOfDay;

            // Create TimePickerDialog
            TimePickerDialog t = new TimePickerDialog(this,OnTimeSet,time.Hours,time.Minutes,true);

            t.Show();
        }

        private void OnTimeSet(object sender, TimePickerDialog.TimeSetEventArgs e)
        {
            hour = e.HourOfDay;
            minute = e.Minute;

            // Do something with the selected time
            Toast.MakeText(this, $"Selected Time: {hour:D2}:{minute:D2}", ToastLength.Short).Show();
        }

        private void SetAlarmButton_Click(object sender, EventArgs e)
        {
            CreateDialog();

            // קבלת התאריך והשעה מה-UI
            DateTime selectedDate = new DateTime(2025, 6, 4);
            TimeSpan selectedTime = new TimeSpan(hour, minute,0);
            DateTime scheduledDateTime = selectedDate.Add(selectedTime);

            // ודא שהזמן המתוזמן הוא בעתיד
            if (scheduledDateTime <= DateTime.Now)
            {
                Toast.MakeText(this, "אנא בחר תאריך ושעה בעתיד.", ToastLength.Long).Show();
                return;
            }

            string notificationMessage = "התראה נסיון";
            if (string.IsNullOrWhiteSpace(notificationMessage))
            {
                notificationMessage = "זוהי התראה מתוזמנת!";
            }

            // יצירת Intent שיפעיל את ה-AlarmReceiver
            Intent alarmIntent = new Intent(this, typeof(AlarmReciever));
            alarmIntent.PutExtra("notificationMessage", notificationMessage); // העברת הטקסט להתראה

            // PendingIntent הוא Wrapper סביב Intent שמאפשר להעביר אותו לאפליקציה חיצונית (AlarmManager)
            // RequestCode (המספר השני) משמש לזיהוי ייחודי של PendingIntent.
            // חשוב שכל אזעקה תהיה עם RequestCode שונה כדי שה-AlarmManager לא יחליף אזעקות קודמות.
            // PendingIntentFlags.UpdateCurrent מאפשר לעדכן Intent קיים אם קיים עם אותו RequestCode.
            int requestCode = (int)scheduledDateTime.Ticks; // שימוש ב-Ticks כ-RequestCode ייחודי
            PendingIntent pendingIntent = PendingIntent.GetBroadcast(this, requestCode, alarmIntent, PendingIntentFlags.UpdateCurrent | PendingIntentFlags.Immutable);


            // קבלת מופע של AlarmManager
            AlarmManager alarmManager = (AlarmManager)GetSystemService(Context.AlarmService);

            // המרה של DateTime למילישניות מאז Epoch (1 בינואר 1970) - פורמט ש-AlarmManager דורש
            long triggerTimeMillis = (long)scheduledDateTime.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

            // תזמון האזעקה
            // AlarmType.RtcWakeup - גורם למכשיר להתעורר אם הוא במצב שינה
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M) // Android 6.0 Marshmallow ומעלה
            {
                alarmManager.SetExactAndAllowWhileIdle(AlarmType.RtcWakeup, triggerTimeMillis, pendingIntent);
            }
            else if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat) // Android 4.4 KitKat ומעלה
            {
                alarmManager.SetExact(AlarmType.RtcWakeup, triggerTimeMillis, pendingIntent);
            }
            else // גרסאות ישנות יותר
            {
                alarmManager.Set(AlarmType.RtcWakeup, triggerTimeMillis, pendingIntent);
            }

            Toast.MakeText(this, $"התראה נקבעה ל- {scheduledDateTime:g}", ToastLength.Long).Show();
        }

        // שיטה ליצירת ערוץ התראות (חובה לאנדרואיד 8.0 Oreo ומעלה)
        private void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // אין צורך בערוץ התראות בגרסאות ישנות יותר
                return;
            }

            var channelName = "ערוץ התראות האזעקה";
            var channelDescription = "ערוץ עבור התראות מתוזמנות מהאפליקציה";
            var channel = new NotificationChannel("my_alarm_channel_id", channelName, NotificationImportance.High)
            {
                Description = channelDescription
            };

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}