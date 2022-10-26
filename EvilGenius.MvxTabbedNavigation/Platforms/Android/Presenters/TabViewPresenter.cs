using MvvmCross.Base;
using MvvmCross.Exceptions;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Platforms.Android.Presenters;
using MvvmCross.Presenters.Attributes;
using MvvmCross.ViewModels;
using MvvmCross.WeakSubscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EvilGenius.MvxTabbedNavigation.Platforms.Android.Presenters.Attributes;
using EvilGenius.MvxTabbedNavigation.Platforms.Android.Views;
using MvvmCross.Presenters;
using EvilGenius.MvxTabbedNavigation.Presenters.Attributes;
using MvvmCross.Platforms.Android.Views.Fragments;
using MvvmCross.Platforms.Android.Views;
using AndroidX.Fragment.App;
using Fragment = AndroidX.Fragment.App.Fragment;
using FragmentTransaction = AndroidX.Fragment.App.FragmentTransaction;
using ActivityX = AndroidX.AppCompat.App.AppCompatActivity;
using System.Diagnostics;
using Android.Content;
using Android.App;
using FragmentManager = AndroidX.Fragment.App.FragmentManager;
using Android.OS;
using MvvmCross.Presenters.Hints;
using EvilGenius.MvxTabbedNavigation.Platforms.Android.Core;
using EvilGenius.MvxTabbedNavigation.Presenters.Hints;

namespace EvilGenius.MvxTabbedNavigation.Platforms.Android.Presenters
{
    public class TabViewPresenter : MvxAndroidViewPresenter, IDisposable
    {
        protected virtual ISingleHostActivity? SingleHostActivity => CurrentActivity as ISingleHostActivity;

        protected virtual ITabbedFragment? RootFragment { get; set; }

        private IDisposable? _tabSelectedSubscription;

        private IDisposable? _tabbedStackChangedSubscription;

        private IDisposable? _activityLifecycleSubscription;

        private IDisposable? _backPressedSubscription;

        private IDisposable? _backRequestedSubscription;

        private int _currentTabIndex = 0;

        protected IList<ViewModelBackStackMetadata> ViewModelBackStacks { get; } = new List<ViewModelBackStackMetadata>();

        private TabbedFragmentAttachedListener? _tabbedFragAttachedListener;

        private bool _disposed;

        public TabViewPresenter(IEnumerable<Assembly> androidViewAssemblies, IActivityLifecycleListener activityLifecycleListener) 
            : base(androidViewAssemblies) 
        {
            _activityLifecycleSubscription = activityLifecycleListener.WeakSubscribe<IActivityLifecycleListener, ActivityLifecycleEventArgs>(
                nameof(IActivityLifecycleListener.ActivityStateChanged),
                OnActivityLifecycleListenerOnChanged);
        }

        public override void RegisterAttributeTypes()
        {
            //not gonna call base here because this Presenter work differently
            if (AttributeTypesToActionsDictionary == null)
                throw new InvalidOperationException("Cannot register attribute types on null dictionary");

            AttributeTypesToActionsDictionary.Register<RootFragmentPresentationAttribute>(ShowRootFragment, CloseRootFragment);

            AttributeTypesToActionsDictionary.Register<MvxFragmentPresentationAttribute>(ShowFragment, CloseFragment);

            AttributeTypesToActionsDictionary.Register<TabPresentationAttribute>(ShowTabFragment, CloseTabFragment);

            AttributeTypesToActionsDictionary.Register<OverTopPresentationAttribute>(ShowFragmentOverTop, CloseFragmentOverTop);
        }


        private async Task<bool> ShowRootFragment(Type type, RootFragmentPresentationAttribute attr, MvxViewModelRequest vmRequest)
        {
            PendingRequest = vmRequest;

            var showActivityRes = await this.ShowHostActivity(attr.HostActivityType);

            if (!showActivityRes)
            {
                PendingRequest = null;
                return false;
            }

            return true;
        }

        private Task<bool> ShowRootFragment(MvxViewModelRequest vmRequest, MvxActivityEventArgs hostActivityEventArgs)
        {
            if (hostActivityEventArgs.Activity is ISingleHostActivity singleHostActivity
                && singleHostActivity.FragmentManager is FragmentManager fm
                && singleHostActivity.ContainerId is int containerId
                && vmRequest != null)
            {
                MvxFragmentPresentationAttribute presentationAttr = new MvxFragmentPresentationAttribute
                {
                    AddFragment = false,
                    AddToBackStack = true,
                    ViewType = ViewsContainer?.GetViewType(vmRequest?.ViewModelType),
                    FragmentContentId = containerId
                };

                PerformShowFragmentTransaction(fm, presentationAttr, vmRequest!);
            }
            else
                return Task.FromResult(false);

            return Task.FromResult(true);
        }

        private void OnTabbedFragmentAttached(object? sender, MvxValueEventArgs<ITabbedFragment> ea)
        {
            if (ea != null)
            {
                var fragment = ea.Value;
                this.RootFragment = fragment;
                _tabSelectedSubscription = fragment.WeakSubscribe<ITabbedFragment, MvxValueEventArgs<int>>(nameof(ITabbedFragment.TabSelected), OnTabSelected);

                if(fragment.FragmentManager is FragmentManager fm)
                    _tabbedStackChangedSubscription = fm.WeakSubscribe(nameof(FragmentManager.BackStackChanged), OnTabbedFragmentBackStackChanged);
            }
        }

        protected virtual Task<bool> ShowHostActivity(Type activityType)
        {
            var intent = new Intent(Application.Context, activityType);

            ShowIntent(intent, null);
            return Task.FromResult(true);
        }

        private Task<bool> CloseRootFragment(IMvxViewModel viewModel, RootFragmentPresentationAttribute attribute)
        {
            if (IsTopMostFragmentRoot() && SingleHostActivity?.FragmentManager is FragmentManager fm)
            {
                fm.PopBackStack();

                //ClearTabbedFragmentAttachedListener();

                _tabSelectedSubscription?.Dispose();
                _tabSelectedSubscription = null;

                _tabbedStackChangedSubscription?.Dispose();
                _tabbedStackChangedSubscription = null;

                _currentTabIndex = 0;

                RootFragment = null;

                ViewModelBackStacks.Clear();

                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        protected override Task<bool> ShowFragment(Type view, MvxFragmentPresentationAttribute attribute, MvxViewModelRequest vmRequest)
        {
            //not gonna call base here beacuse this Presenter has different logic
            if(vmRequest?.ViewModelType == null)
                throw new ArgumentNullException($"Either {nameof(vmRequest)} or {nameof(vmRequest)}.{nameof(vmRequest.ViewModelType)} is null");

            var result = false;

            if (IsTopMostFragmentRoot() && GetRootFragmentBackStackId() is string tabId)
            {
                ShowTabFragmentCore(attribute, vmRequest, tabId);

                ViewModelBackStacks.PushToBackStack(tabId, vmRequest.ViewModelType);
                
                result = true;
            }
            else if(SingleHostActivity?.FragmentManager is FragmentManager fm
                && SingleHostActivity?.ContainerId is int containerId
                && vmRequest != null)
            {
                MvxFragmentPresentationAttribute presentationAttr = new MvxFragmentPresentationAttribute
                {
                    AddFragment = false,
                    AddToBackStack = true,
                    ViewType = view,
                    FragmentContentId = containerId
                };

                PerformShowFragmentTransaction(fm, presentationAttr, vmRequest!);
                
                result = true;
            }

            return Task.FromResult(result);
        }

        private Task<bool> ShowTabFragment(Type type, TabPresentationAttribute attribute, MvxViewModelRequest vmRequest)
        {
            if (RootFragment == null)
                throw new InvalidOperationException("Unable to show tab because there no tab fragment");

            if (vmRequest?.ViewModelType == null)
                throw new ArgumentNullException($"Either {nameof(vmRequest)} or {nameof(vmRequest)}.{nameof(vmRequest.ViewModelType)} is null");

            RootFragment.AddTab(attribute);

            var isPending = ViewModelBackStacks.Count > 0;

            ViewModelBackStackMetadata metadata = isPending 
                ? ViewModelBackStackMetadata.CreateFromPeindingRequest(attribute, vmRequest) 
                : ViewModelBackStackMetadata.Create(attribute.TabId, vmRequest.ViewModelType!);

            ViewModelBackStacks.Add(metadata);

            if (!isPending)
                ShowTabFragmentCore(attribute, vmRequest, attribute.TabId);

            return Task.FromResult(true);
        }

        private void ShowTabFragmentCore(MvxBasePresentationAttribute attribute, MvxViewModelRequest vmRequest, string tabId)
        {
            if (RootFragment?.FragmentManager is FragmentManager fm 
                && RootFragment?.ContainerId is int containerId
                && attribute.ViewType is Type viewType)
            {
                var fragmentView = CreateFragment(fm, attribute, viewType);
                var fragment = fragmentView.ToFragment();
                if (fragment == null)
                {
                    var fragmentName = viewType.FragmentJavaName();
                    throw new MvxException($"Fragment {fragmentName} is null. Cannot perform Fragment Transaction.");
                }

                // MvxNavigationService provides an already instantiated ViewModel here
                //if (vmRequest is MvxViewModelInstanceRequest instanceRequest)
                //{
                //    fragmentView!.ViewModel = instanceRequest.ViewModelInstance;
                //}

                // save MvxViewModelRequest in the Fragment's Arguments
#pragma warning disable CA2000 // Dispose objects before losing scope
                var bundle = new Bundle();
#pragma warning restore CA2000 // Dispose objects before losing scope
                var serializedRequest = NavigationSerializer.Serializer.SerializeObject(vmRequest);
                bundle.PutString(ViewModelRequestBundleKey, serializedRequest);

                if (fragment.Arguments == null)
                {
                    fragment.Arguments = bundle;
                }
                else
                {
                    fragment.Arguments.Clear();
                    fragment.Arguments.PutAll(bundle);
                }

                var ft = fm.BeginTransaction();
                ft?.SetReorderingAllowed(true);
                ft?.Replace(containerId, fragment);
                ft?.AddToBackStack(tabId);
                ft?.Commit();
            }
        }

        private Task<bool> CloseTabFragment(IMvxViewModel viewModel, TabPresentationAttribute attribute)
        {
            if (IsTopMostFragmentRoot() && RootFragment?.FragmentManager is FragmentManager fm)
            {
                var tabId = attribute.TabId;

                fm.SaveBackStack(tabId);
                fm.ClearBackStack(tabId);

                if (ViewModelBackStacks.Count != 0)
                {

                    var isNextTabToSelect = _currentTabIndex + 1 < ViewModelBackStacks.Count;

                    var tabToSelect = isNextTabToSelect ? _currentTabIndex + 1 : _currentTabIndex - 1;

                    ChangeCurrentBackStack(tabToSelect);

                    RootFragment.RemoveTab(tabId);

                    if (isNextTabToSelect)
                        _currentTabIndex--;
                }
                else
                    _currentTabIndex = 0;

                ViewModelBackStacks.RemoveBackStack(tabId);

                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        private Task<bool> ShowFragmentOverTop(Type type, OverTopPresentationAttribute attribute, MvxViewModelRequest vmRequest)
        {
            if (SingleHostActivity?.FragmentManager is FragmentManager fm
                && SingleHostActivity?.ContainerId is int containerId
                && vmRequest != null)
            {
                MvxFragmentPresentationAttribute presentationAttr = new MvxFragmentPresentationAttribute
                {
                    AddFragment = false,
                    AddToBackStack = true,
                    ViewType = type,
                    FragmentContentId = containerId
                };

                PerformShowFragmentTransaction(fm, presentationAttr, vmRequest!);
            }

            return Task.FromResult(false);
        }

        private Task<bool> CloseFragmentOverTop(IMvxViewModel viewModel, OverTopPresentationAttribute attribute)
        {
            if (SingleHostActivity?.ContainerId is int containerId)
            {
                MvxFragmentPresentationAttribute presentationAttr = new MvxFragmentPresentationAttribute
                {
                    AddFragment = false,
                    AddToBackStack = false,
                    ViewType = attribute.ViewType,
                    FragmentContentId = containerId
                };

                return CloseFragment(viewModel, presentationAttr);
            }
            else
                return Task.FromResult(false);
        }

        protected override void OnFragmentChanging(FragmentTransaction? fragmentTransaction, Fragment? fragment, 
            MvxFragmentPresentationAttribute? attribute, MvxViewModelRequest? request)
        {
            base.OnFragmentChanging(fragmentTransaction, fragment, attribute, request);

            if (fragment != null && fragment is IFragmentHost)
                fragmentTransaction?.SetPrimaryNavigationFragment(fragment);
        }

        public override MvxBasePresentationAttribute CreatePresentationAttribute(Type? viewModelType, Type? viewType)
        {
            return new MvxFragmentPresentationAttribute(GetCurrentActivityViewModelType(), global::Android.Resource.Id.Content)
            {
                ViewType = viewType,
                ViewModelType = viewModelType
            };
        }

        protected virtual void OnTabSelected(object? sender, MvxValueEventArgs<int> ea) => ChangeCurrentBackStack(ea.Value);

        protected virtual void ChangeCurrentBackStack(int backStackIndexToChange)
        {
            var fm = RootFragment?.FragmentManager;
            fm.SaveBackStack(ViewModelBackStacks[_currentTabIndex].TabId);

            var tabDataToSelect = ViewModelBackStacks[backStackIndexToChange];

            if (tabDataToSelect != null)
            {
                if (tabDataToSelect.IsPending)
                {
                    (TabPresentationAttribute pAttr, MvxViewModelRequest vmReq) = tabDataToSelect.ExtractPendingRequestData();
                    ShowTabFragmentCore(pAttr, vmReq, pAttr.TabId);
                }
                else
                    fm.RestoreBackStack(tabDataToSelect.TabId);
            }

            _currentTabIndex = backStackIndexToChange;
        }

        private void OnTabbedFragmentBackStackChanged(object? sender, EventArgs e)
        {
            if (RootFragment != null
                && RootFragment?.FragmentManager is FragmentManager fm
                && fm.BackStackEntryCount > 0
                && fm.GetBackStackEntryAt(0) is FragmentManager.IBackStackEntry bsEntry
                && bsEntry.Name is string tabId)
            {
                var entryCount = fm.BackStackEntryCount;
                ViewModelBackStacks.PopFromBackStackIfNeeded(tabId, entryCount);
            }
        }

        protected override void ActivityLifetimeListenerOnActivityChanged(object sender, MvxActivityEventArgs ea)
        {
            if (ea.ActivityState == MvxActivityState.OnResume && PendingRequest?.ViewModelType is Type vmType)
            {
                var viewType = ViewsContainer?.GetViewType(vmType);
                if (viewType?.GetInterface(typeof(IFragmentHost).Name) != null)
                {
                    ShowRootFragment(PendingRequest, ea);
                    PendingRequest = null;
                    return;
                }
            }
            base.ActivityLifetimeListenerOnActivityChanged(sender, ea);
        }

        protected virtual void OnActivityLifecycleListenerOnChanged(object? sender, ActivityLifecycleEventArgs ea)
        {
            if (ea.Activity is ISingleHostActivity hostActivity)
            {
                if (ea.ActivityState == MvxActivityState.OnCreate)
                {
                    if (hostActivity.FragmentManager is FragmentManager fm)
                    {
                        _tabbedFragAttachedListener = new TabbedFragmentAttachedListener();

                        _tabbedFragAttachedListener.TabbedFragmentFragmentManagerReady += OnTabbedFragmentAttached;

                        fm.AddFragmentOnAttachListener(_tabbedFragAttachedListener);
                    }

                    var backPressendCallback = new EventSourceOnBackPressedCallback(true);
                    _backPressedSubscription = new OnBackPressedEventSubscription(
                        backPressendCallback, 
                        nameof(EventSourceOnBackPressedCallback.OnBackPressed), 
                        OnHostActivityBackPressed);

                    if (ea.Activity is ActivityX activity)
                        activity.OnBackPressedDispatcher.AddCallback(backPressendCallback);

                    _backRequestedSubscription = hostActivity.WeakSubscribe<IBackPressedAware, BackPressedRequestedEventArgs>(nameof(IBackPressedAware.OnBackRequested), OnHostActivityBackPressed);
                }

                if (ea.ActivityState == MvxActivityState.OnDestroy)
                {
                    ClearHostActivitySubscriptions();
                }
            }
        }

        public override Task<bool> ChangePresentation(MvxPresentationHint hint) => hint switch
        {
            null => throw new ArgumentNullException(nameof(hint)),
            MvxPagePresentationHint pagePresentationHint => Task.FromResult(ChangePagePresentation(pagePresentationHint)),
            ClearStackPresentationHint clearStackPresentationHint => Task.FromResult(ClearBackStack(clearStackPresentationHint)),
            _ => base.ChangePresentation(hint),
        };

        private bool ChangePagePresentation(MvxPagePresentationHint pagePresentationHint)
        {
            for (int i = 0; i<ViewModelBackStacks.Count; i++)
            {
                var metaDataEntry = ViewModelBackStacks[i];
                if (metaDataEntry.HasViewModelInStack(pagePresentationHint.ViewModel))
                {
                    OnTabSelected(null, new MvxValueEventArgs<int>(i));
                    RootFragment?.SelectTabAt(i);
                    return true;
                }
            }
            return false;
        }

        private bool ClearBackStack(ClearStackPresentationHint clearStackPresentationHint)
        {
            if (IsTopMostFragmentRoot() && GetRootFragmentBackStackId() is string tabId)
            {
                var fm = RootFragment?.FragmentManager;
                
                fm?.SaveBackStack(tabId);
                fm?.ClearBackStack(tabId);

                this.ViewModelBackStacks.ClearBackStack(tabId);

                return true;
            }
            else if (SingleHostActivity?.FragmentManager is FragmentManager fm)
            {
                var stackId = GetHostActivityBackStackId();
                fm.SaveBackStack(stackId);
                fm.ClearBackStack(stackId);
                return true;
            }
            return false;
        }

        private void ClearHostActivitySubscriptions()
        {
            if (SingleHostActivity != null && _tabbedFragAttachedListener != null)
            {
                _tabbedFragAttachedListener!.TabbedFragmentFragmentManagerReady -= OnTabbedFragmentAttached;

                SingleHostActivity.FragmentManager?.RemoveFragmentOnAttachListener(_tabbedFragAttachedListener);

                _tabbedFragAttachedListener?.Dispose();

                _tabbedFragAttachedListener = null;
            }

            _backPressedSubscription?.Dispose();

            _backPressedSubscription = null;

            _backRequestedSubscription?.Dispose();

            _backRequestedSubscription = null;
        }

        protected bool IsTopMostFragmentRoot() 
            => SingleHostActivity?.FragmentManager?.Fragments?.Last() == RootFragment && RootFragment != null;

        protected string? GetRootFragmentBackStackId() => GetBackStackId(RootFragment?.FragmentManager);

        protected string? GetHostActivityBackStackId() => GetBackStackId(SingleHostActivity?.FragmentManager);

        protected string? GetBackStackId(FragmentManager? fm) 
            => fm?.BackStackEntryCount > 0
                && fm?.GetBackStackEntryAt(fm.BackStackEntryCount - 1) is FragmentManager.IBackStackEntry bsEntry
                && bsEntry.Name is string backStackId
                ? backStackId
                : null;

        protected bool IsLastFragmentPresentedInRootFragment() => IsLastFragmentPresented(RootFragment?.FragmentManager);

        protected bool IsLastFragmentPresentedInHostActivity() => IsLastFragmentPresented(SingleHostActivity?.FragmentManager);

        protected bool IsLastFragmentPresented(FragmentManager? fm) => !(fm?.BackStackEntryCount > 1);


        protected virtual void OnHostActivityBackPressed(object sender, EventArgs e) { }

        protected virtual void OnHostActivityBackPressed(object sender, BackPressedRequestedEventArgs ea) 
        {
            if (IsTopMostFragmentRoot())
            {
                if (IsLastFragmentPresentedInRootFragment())
                {
                    ea.Result = BackPressedHadlingResult.FinishActivity;
                    return;
                }
                else
                {
                    ea.Result = BackPressedHadlingResult.NotHandled;
                    return;
                }
            }
            else
            {
                ea.Result = BackPressedHadlingResult.NotHandled;
                return;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    ClearHostActivitySubscriptions();
                    _tabSelectedSubscription?.Dispose();
                    _tabSelectedSubscription = null;
                    _tabbedStackChangedSubscription?.Dispose();
                    _tabbedStackChangedSubscription = null;
                    _activityLifecycleSubscription?.Dispose();
                    _activityLifecycleSubscription = null;
                }

                _disposed = true;
            }
        }

        ~TabViewPresenter() => Dispose(disposing: false);

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
