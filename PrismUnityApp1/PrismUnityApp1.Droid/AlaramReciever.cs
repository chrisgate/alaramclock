using Android.Content;
using Android.App;
using Android.Support.V4.App;
using Android.Graphics;
using Android.Media;
using Android.Net;
using Android.Speech.Tts;

namespace PrismUnityApp1.Droid
{
    [BroadcastReceiver]
    public class AlarmReceiver : BroadcastReceiver
    {
        private TextToSpeech_Android mytts;
        public override void OnReceive(Context context, Intent intent)
        {
            var message = intent.GetStringExtra("message");
            var title = intent.GetStringExtra("title");

            var notIntent = new Intent(context, typeof(MainActivity));
            var contentIntent = PendingIntent.GetActivity(context, 0, notIntent, PendingIntentFlags.CancelCurrent);

            Uri ring_uri = RingtoneManager.GetActualDefaultRingtoneUri(context, RingtoneType.Ringtone);
            Ringtone r = RingtoneManager.GetRingtone(context, ring_uri);
            //r.Play();

            mytts = new TextToSpeech_Android();
            Intent checkttsIntent = new Intent();
            checkttsIntent.SetAction(TextToSpeech.Engine.ActionCheckTtsData);
            mytts.Speak(message);


            //var manager = NotificationManagerCompat.From(context);

            //var style = new NotificationCompat.BigTextStyle();
            //style.BigText(message);
                              
        }
    }
}