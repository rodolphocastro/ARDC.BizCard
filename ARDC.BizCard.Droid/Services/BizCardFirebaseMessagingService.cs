
using Android.App;
using Android.Content;
using Android.Util;
using Firebase.Messaging;

namespace ARDC.BizCard.Droid.Services
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class BizCardFirebaseMessagingService : FirebaseMessagingService
    {
        const string TAG = "BizCardFirebaseMessaging";

        public override void OnMessageReceived(RemoteMessage message)
        {
            Log.Debug(TAG, "From: " + message.From);
            Log.Debug(TAG, "Notification Message Body: " + message.GetNotification().Body);
        }

    }
}