using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using ARDC.BizCard.Core.ViewModels;
using ARDC.BizCard.Core.ViewModels.Agenda;
using Firebase.Analytics;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace ARDC.BizCard.Droid.Fragments
{
    /// <summary>
    /// Fragment para "Agenda de Cartões".
    /// </summary>
    [MvxFragmentPresentation(ActivityHostViewModelType = typeof(MainViewModel), AddToBackStack = true, FragmentContentId = Resource.Id.content_frame)]
    [Register(nameof(AgendaView))]
    public class AgendaView : MvxFragment<AgendaViewModel>
    {
        /// <summary>
        /// Rotina para a criação do Fragment.
        /// </summary>
        /// <param name="inflater"></param>
        /// <param name="container"></param>
        /// <param name="savedInstanceState"></param>
        /// <returns>A View do fragment</returns>
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var _ = base.OnCreateView(inflater, container, savedInstanceState);

            FirebaseAnalytics.GetInstance(Activity).SetCurrentScreen(Activity, "Agenda", nameof(AgendaView));

            return this.BindingInflate(Resource.Layout.agenda, null);
        }

        /// <summary>
        /// Rotina para criação do Fragment.
        /// </summary>
        /// <param name="savedInstanceState"></param>
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            var recyclerView = Activity.FindViewById<MvxRecyclerView>(Resource.Id.list_cards);
            recyclerView.AddItemDecoration(new DividerItemDecoration(Activity, DividerItemDecoration.Vertical));
            recyclerView.HasFixedSize = true;
            recyclerView.SetLayoutManager(new LinearLayoutManager(Activity));
            recyclerView.SetItemAnimator(new DefaultItemAnimator());
        }

        /// <summary>
        /// Rotina para continuação do Fragment.
        /// </summary>
        public override void OnResume()
        {
            base.OnResume();

            Activity?.SetTitle(Resource.String.action_agenda);
        }
    }
}