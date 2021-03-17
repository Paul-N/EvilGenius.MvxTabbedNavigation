using MvvmCross.Core;
using MvvmCross.Exceptions;
using MvvmCross.Navigation;
using MvvmCross.Navigation.EventArguments;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using MvxSystem = MvvmCross.Mvx;

namespace EvilGenius.Mvx.TabNavigation.Core.Navigation
{
    public class NavigationService : MvxNavigationService
    {
        public NavigationService(IMvxNavigationCache navigationCache, IMvxViewModelLoader viewModelLoader) : base(navigationCache, viewModelLoader) { }

        protected override async Task<MvxViewModelInstanceRequest> NavigationRouteRequest(string path, IMvxBundle presentationBundle = null)
        {
            KeyValuePair<Regex, Type> entry;

            if (!TryGetRoute(path, out entry))
            {
                throw new MvxException($"Navigation route request could not be obtained for path: {path}");
            }

            var regex = entry.Key;
            var match = regex.Match(path);
            var paramDict = BuildParamDictionary(regex, match);
            var parameterValues = new MvxBundle(paramDict);

            var viewModelType = entry.Value;

            var request = new MvxViewModelInstanceRequest(viewModelType)
            {
                PresentationValues = presentationBundle?.SafeGetData(),
                ParameterValues = parameterValues?.SafeGetData()
            };

            if (viewModelType.GetInterfaces().Contains(typeof(IMvxNavigationFacade)))
            {
                var facade = (IMvxNavigationFacade)MvxSystem.IoCProvider.IoCConstruct(viewModelType);

                try
                {
                    var facadeRequest = await facade.BuildViewModelRequest(path, paramDict).ConfigureAwait(false);
                    if (facadeRequest == null)
                    {
                        throw new MvxException($"{nameof(MvxNavigationService)}: Facade did not return a valid {nameof(MvxViewModelRequest)}.");
                    }

                    request.ViewModelType = facadeRequest.ViewModelType;

                    if (facadeRequest.ParameterValues != null)
                    {
                        request.ParameterValues = facadeRequest.ParameterValues;
                    }
                }
                catch (Exception ex)
                {
                    throw ex.MvxWrap($"{nameof(MvxNavigationService)}: Exception thrown while processing URL: {path} with RoutingFacade: {viewModelType}");
                }
            }

            return request;
        }

        protected new async Task<MvxViewModelInstanceRequest> NavigationRouteRequest<TParameter>(string path, TParameter param, IMvxBundle presentationBundle = null) where TParameter : class
        {
            KeyValuePair<Regex, Type> entry;

            if (!TryGetRoute(path, out entry))
            {
                throw new MvxException($"Navigation route request could not be obtained for path: {path}");
            }

            var regex = entry.Key;
            var match = regex.Match(path);
            var paramDict = BuildParamDictionary(regex, match);
            var parameterValues = new MvxBundle(paramDict);

            var viewModelType = entry.Value;

            var request = new MvxViewModelInstanceRequest(viewModelType)
            {
                PresentationValues = presentationBundle?.SafeGetData(),
                ParameterValues = parameterValues?.SafeGetData()
            };

            if (viewModelType.GetInterfaces().Contains(typeof(IMvxNavigationFacade)))
            {
                var facade = (IMvxNavigationFacade)MvxSystem.IoCProvider.IoCConstruct(viewModelType);

                try
                {
                    var facadeRequest = await facade.BuildViewModelRequest(path, paramDict).ConfigureAwait(false);
                    if (facadeRequest == null)
                    {
                        throw new MvxException($"{nameof(MvxNavigationService)}: Facade did not return a valid {nameof(MvxViewModelRequest)}.");
                    }

                    request.ViewModelType = facadeRequest.ViewModelType;

                    if (facadeRequest.ParameterValues != null)
                    {
                        request.ParameterValues = facadeRequest.ParameterValues;
                    }
                }
                catch (Exception ex)
                {
                    ex.MvxWrap($"{nameof(MvxNavigationService)}: Exception thrown while processing URL: {path} with RoutingFacade: {viewModelType}");
                }
            }

            return request;
        }

        protected override async Task<bool> Navigate(MvxViewModelRequest request, IMvxViewModel viewModel, IMvxBundle presentationBundle = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var hasNavigated = false;

            var args = new MvxNavigateEventArgs(NavigationMode.Show, cancellationToken);
            OnBeforeNavigate(this, args);

            if (args.Cancel)
                return false;

            hasNavigated = await ViewDispatcher.ShowViewModel(request);
            if (!hasNavigated)
                return false;

            OnAfterNavigate(this, args);
            return true;
        }

        //protected override async Task<TResult> Navigate<TResult>(MvxViewModelRequest request, IMvxViewModelResult<TResult> viewModel, IMvxBundle presentationBundle = null, CancellationToken cancellationToken = default)
        //{
        //    var hasNavigated = false;
        //    var tcs = new TaskCompletionSource<object>();

        //    var args = new MvxNavigateEventArgs(NavigationMode.Show, cancellationToken);
        //    OnBeforeNavigate(this, args);

        //    //viewModel.CloseCompletionSource = tcs;
        //    //_tcsResults.Add(viewModel, tcs);

        //    if (cancellationToken.IsCancellationRequested)
        //        return default(TResult);

        //    hasNavigated = await ViewDispatcher.ShowViewModel(request);
        //    if (!hasNavigated)
        //        return default(TResult);

        //    // TODO: Should Initialize task be done on UI or non-UI thread? Now that ShowViewModel is async, we could ConfigureAwait it to return on non-UI thread
        //    if (viewModel.InitializeTask?.Task != null)
        //        await viewModel.InitializeTask.Task.ConfigureAwait(false);

        //    OnAfterNavigate(this, args);

        //    try
        //    {
        //        return (TResult)await tcs.Task;
        //    }
        //    catch (Exception)
        //    {
        //        return default(TResult);
        //    }
        //}
    }
}
