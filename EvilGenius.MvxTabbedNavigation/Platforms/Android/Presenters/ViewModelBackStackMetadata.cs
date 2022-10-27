using EvilGenius.MvxTabbedNavigation.Platforms.Android.Presenters.Attributes;
using MvvmCross.ViewModels;

namespace EvilGenius.MvxTabbedNavigation.Platforms.Android.Presenters
{
    public class ViewModelBackStackMetadata
    {
        public bool IsPending { get; private set; }

        public string TabId { get; private set; } = null!;

        private TabPresentationAttribute? _pendingPresentationAttribute;

        private MvxViewModelRequest? _pendingVMRequest;

        private Stack<Type> _viewModelTypesStack { get; } = new Stack<Type>();

        private ViewModelBackStackMetadata(TabPresentationAttribute attribute, MvxViewModelRequest vmRequest)
        {
            if(attribute == null)
                throw new ArgumentNullException(nameof(attribute));

            if (attribute.ViewModelType == null)
                throw new ArgumentException($"{nameof(attribute)}.{nameof(attribute.ViewModelType)} is null");

            if (vmRequest == null)
                throw new ArgumentNullException(nameof(vmRequest));

            IsPending = true;
            _pendingPresentationAttribute = attribute;
            _pendingVMRequest = vmRequest;
            Initialize(attribute.TabId, vmRequest.ViewModelType!);
        }

        private ViewModelBackStackMetadata(string tabId, Type viewModelType)
        {
            if (tabId == null)
                throw new ArgumentNullException(nameof(tabId));

            if (viewModelType == null)
                throw new ArgumentNullException(nameof(viewModelType));

            IsPending = false;
            Initialize(tabId, viewModelType);
        }

        private void Initialize(string tabId, Type viewModelType)
        {
            TabId = tabId;
            _viewModelTypesStack.Push(viewModelType);
        }

        public (TabPresentationAttribute attribute, MvxViewModelRequest vmRequest) ExtractPendingRequestData()
        {
            IsPending = false;
            var result = (_pendingPresentationAttribute!, _pendingVMRequest!);
            _pendingPresentationAttribute = null;
            _pendingVMRequest = null;
            return result;
        }

        public static ViewModelBackStackMetadata CreateFromPeindingRequest(TabPresentationAttribute attribute, MvxViewModelRequest vmRequest) 
            => new ViewModelBackStackMetadata(attribute, vmRequest);

        public static ViewModelBackStackMetadata Create(string tabId, Type viewModelType) 
            => new ViewModelBackStackMetadata(tabId, viewModelType);

        public bool HasViewModelInStack(Type viewModel) => _viewModelTypesStack.Contains(viewModel);

        public void Push(Type viewModelType) => _viewModelTypesStack.Push(viewModelType);

        public void Pop() => _viewModelTypesStack.Pop();

        public void PopIfNeeded(int count)
        {
            if (count == _viewModelTypesStack.Count - 1)
                _viewModelTypesStack.Pop();
        }

        public void Clear() => _viewModelTypesStack.Clear();

        public void PopToRoot()
        {
            if (_viewModelTypesStack.Count > 0)
            {
                var firstVMtype = _viewModelTypesStack.ElementAt(0);
                _viewModelTypesStack.Clear();
                _viewModelTypesStack.Push(firstVMtype); 
            }
        }
    }
}
