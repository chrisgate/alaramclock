using System;
using Android.Support.V4.App;
using Android.Content;
using Android.App;
using Xamarin.Forms;
using Android.Graphics;
using Android.OS;
using Android.Provider;

[assembly: Xamarin.Forms.Dependency(typeof(PrismUnityApp1.Droid.AndroidReminderService))]

namespace PrismUnityApp1.Droid
{
    public class AndroidReminderService : IReminderService
    {
        #region IReminderService implementation

        public void Remind(DateTime dateTime, string title, string message)
        {

            Intent alarmIntent = new Intent(Forms.Context, typeof(AlarmReceiver));
            alarmIntent.PutExtra("message", message);
            alarmIntent.PutExtra("title", title);
            int guid = (int)SystemClock.CurrentThreadTimeMillis();
            double timeInMilliseconds = (dateTime - DateTime.Now).TotalSeconds;
            PendingIntent pendingIntent = PendingIntent.GetBroadcast(Forms.Context, guid, alarmIntent, PendingIntentFlags.UpdateCurrent);
            AlarmManager alarmManager = (AlarmManager)Forms.Context.GetSystemService(Context.AlarmService);

            //TODO: For demo set after 5 seconds.
            //alarmManager.Set(AlarmType.RtcWakeup, (long)timeInMilliseconds, pendingIntent);
            alarmManager.Set(AlarmType.ElapsedRealtime, SystemClock.ElapsedRealtime() + (long)timeInMilliseconds * 1000, pendingIntent);


        }

        #endregion
    }
}