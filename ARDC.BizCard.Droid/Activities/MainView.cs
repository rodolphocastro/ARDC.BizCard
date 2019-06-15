using Android.App;
using Android.Gms.Common;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using ARDC.BizCard.Core.ViewModels;
using ARDC.BizCard.Droid.Fragments;
using Firebase.Analytics;
using Firebase.Iid;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace ARDC.BizCard.Droid.Activities
{
    /// <summary>
    /// Activity principal do Aplicativo, responsável por hospedar os Fragments de conteúdo.
    /// </summary>
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class MainView : MvxAppCompatActivity<MainViewModel>
    {
        static readonly string TAG = "MainView";

        internal static readonly string CHANNEL_ID = "bizcard_general_notifications";
        internal static readonly int NOTIFICATION_ID = 100;

        /// <summary>
        /// Rotina para criação da Activity.
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            if (savedInstanceState == null)
            {
                LandingView landingFragment = new LandingView();
                var fTrans = SupportFragmentManager.BeginTransaction();
                fTrans.Add(Resource.Id.content_frame, landingFragment);
                fTrans.Commit();
            }

            if (IsGooglePlayAvailable())
            {
                SetupNotifications();
                SetupCrashlytics();
                SetupAnalytics();
            }

            FirebaseAnalytics.GetInstance(this).SetCurrentScreen(this, "Main View", nameof(MainView));
        }

        /// <summary>
        /// Rotina para criação do Menu de Opções.
        /// </summary>
        /// <param name="menu">O menu a ser criado</param>
        /// <returns>TRUE caso o Menu seja aberto com êxito</returns>
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        /// <summary>
        /// Rotina para seleção de itens do Menu de Opções.
        /// </summary>
        /// <param name="item">O item selecionado pelo usuário</param>
        /// <returns>TRUE caso não ocorra erros</returns>
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            switch (id)
            {
                case (Resource.Id.action_my_card):
                    ViewModel.NavigateToMyCardCommand.Execute();
                    break;
                case (Resource.Id.action_qrcode):
                    ViewModel.NavigateToQrCommand.Execute();
                    break;
                case (Resource.Id.action_agenda):
                    ViewModel.NavigateToAgendaCommand.Execute();
                    break;
                default:
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        /// <summary>
        /// Rotina para a requisição de Permissões.
        /// </summary>
        /// <param name="requestCode">Código da requisição</param>
        /// <param name="permissions">Permissão requerida</param>
        /// <param name="grantResults">Resultados das requisições</param>
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private bool IsGooglePlayAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void SetupNotifications()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O) // Para APIs anteriores ao Oreo não é necessário configurar channels
                return;

            var channel = new NotificationChannel(CHANNEL_ID, "Notificações Gerais", NotificationImportance.Default)
            {
                Description = "Notificações gerais do BizCard"
            };

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);

#if DEBUG
            Log.Debug(TAG, FirebaseInstanceId.Instance.Token);
#endif
        }

        private void SetupCrashlytics()
        {
            Fabric.Fabric.With(this, new Crashlytics.Crashlytics());

            Crashlytics.Crashlytics.HandleManagedExceptions();
#if DEBUG
            Log.Debug(TAG, "Crashlytics Started");
#endif
        }

        private void SetupAnalytics()
        {
            FirebaseAnalytics.GetInstance(this).SetAnalyticsCollectionEnabled(true);            
        }
    }
}

