using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Core.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaviToDoListProjectOfficialV3.Helpers
{
    [BroadcastReceiver]
    public class AlarmReciever : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            string notificationMessage = intent.GetStringExtra("notificationMessage") ?? "זוהי התראה מתוזמנת!";

            // יצירת התראה
            // ID ייחודי לכל התראה (אפשר להשתמש ב-Random או ב-timestamp)
            int notificationId = 1001;

            // יצירת Intent שייפתח כאשר המשתמש ילחץ על ההתראה (אופציונלי)
            Intent resultIntent = new Intent(context, typeof(MainActivity));
            PendingIntent pendingIntent = PendingIntent.GetActivity(context, 0, resultIntent, PendingIntentFlags.Immutable);

            var notificationBuilder = new NotificationCompat.Builder(context, "my_alarm_channel_id")
                 //  .SetSmallIcon(Resource.Drawable.notification_icon) // חובה! צרי אייקון קטן (לדוגמה, ב-Resources/drawable)
                 .SetSmallIcon(Resource.Drawable.notification_icon) // חובה! צרי אייקון קטן (לדוגמה, ב-Resources/drawable)
                .SetContentTitle("תזכורת מהאפליקציה")
                .SetContentText(notificationMessage)
                .SetPriority(NotificationCompat.PriorityHigh) // חשיבות גבוהה כדי שההתראה תבלוט
                .SetAutoCancel(true) // ההתראה תיעלם לאחר לחיצה עליה
                .SetContentIntent(pendingIntent); // הגדרת ה-Intent שיופעל בלחיצה

            var notificationManager = NotificationManagerCompat.From(context);
            notificationManager.Notify(notificationId, notificationBuilder.Build());
        }
    }
}