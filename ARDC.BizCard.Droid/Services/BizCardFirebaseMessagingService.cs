
using Android.App;
using Android.Content;
using Android.Support.V4.App;
using Android.Util;
using ARDC.BizCard.Droid.Activities;
using Firebase.Messaging;
using System.Collections.Generic;

namespace ARDC.BizCard.Droid.Services
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class BizCardFirebaseMessagingService : FirebaseMessagingService
    {
        const string TAG = "BizCardFirebaseMessaging";

        public override void OnMessageReceived(RemoteMessage message)
        {
#if DEBUG
            Log.Debug(TAG, "From: " + message.From);
            Log.Debug(TAG, "Notification Message Body: " + message.GetNotification().Body);
#endif

            var body = message.GetNotification().Body;
            PushNotificationToTray(body, message.Data);
        }


        private void PushNotificationToTray(string messageBody, IDictionary<string, string> data)
        {
            var intent = new Intent(this, typeof(MainView));
            intent.AddFlags(ActivityFlags.ClearTop);
            foreach (var key in data.Keys)
            {
                intent.PutExtra(key, data[key]);
            }

            var pendingIntent = PendingIntent.GetActivity(this,
                MainView.NOTIFICATION_ID,
                intent,
                PendingIntentFlags.OneShot);

            var notificationBuilder = new NotificationCompat.Builder(this, MainView.CHANNEL_ID)
                .SetSmallIcon(Resource.Drawable.account)
                .SetContentTitle("Meu Cartão de Visitas")
                .SetContentText(messageBody)
                .SetAutoCancel(true)
                .SetContentIntent(pendingIntent);

            var notificationManager = NotificationManagerCompat.From(this);
            notificationManager.Notify(MainView.NOTIFICATION_ID, notificationBuilder.Build());
        }
    }
}