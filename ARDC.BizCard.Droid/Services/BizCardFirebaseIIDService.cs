
using Android.App;
using Android.Content;
using Android.Util;
using Firebase.Iid;

namespace ARDC.BizCard.Droid.Services
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class BizCardFirebaseIIDService : FirebaseInstanceIdService
    {
        const string TAG = "MyFirebaseIIDService";

        public override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            SendRegistrationToServer(refreshedToken);
        }
        void SendRegistrationToServer(string token)
        {
            Log.Debug(TAG, "Refreshed token: " + token);    // TODO: Futuramente, armazenar em algum lugar
        }
    }
}