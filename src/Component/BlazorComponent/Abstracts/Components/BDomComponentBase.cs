using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace BlazorComponent
{
    public abstract class BDomComponentBase : BComponentBase, IHasProviderComponent
    {
        protected BDomComponentBase()
        {
            _watcher = new PropertyWatcher(GetType());

            AbstractProvider = new();
        }

        [Inject]
        private ILoggerFactory LoggerFactory { get; set; } = null!;

        [Inject]
        [NotNull]
        public IComponentIdGenerator? ComponentIdGenerator { get; set; }

        [Parameter]
        public string Id { get; set; } = null!;

        /// <summary>
        /// Custom attributes
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public virtual IDictionary<string, object?> Attributes { get; set; } = new Dictionary<string, object?>();

        private readonly PropertyWatcher _watcher;

        private ElementReference _ref;
        private ElementReference? _prevRef;
        private bool _elementReferenceChanged;

        protected bool HostedInWebAssembly => Js is IJSInProcessRuntime;

        protected ILogger Logger => LoggerFactory.CreateLogger(GetType());

        public ComponentAbstractProvider AbstractProvider { get; }

        /// <summary>
        /// Returned ElementRef reference for DOM element.
        /// </summary>
        public virtual ElementReference Ref
        {
            get => _ref;
            set
            {
                if (_prevRef.HasValue)
                {
                    if (_prevRef.Value.Id != value.Id)
                    {
                        _prevRef = value;
                        _elementReferenceChanged = true;
                    }
                }
                else
                {
                    _prevRef = value;
                }

                _ref = value;
                RefBack?.Set(value);
            }
        }

        protected override void OnInitialized()
        {
            Id ??= ComponentIdGenerator.Generate(this); // TODO: v2 remove this?
            base.OnInitialized();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                RegisterWatchers(_watcher);
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (_elementReferenceChanged)
            {
                _elementReferenceChanged = false;
                await OnElementReferenceChangedAsync();
            }
        }

        protected virtual Task OnElementReferenceChangedAsync()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Register watchers at the first render.
        /// </summary>
        /// <param name="watcher"></param>
        protected virtual void RegisterWatchers(PropertyWatcher watcher)
        {
        }

        protected TValue? GetValue<TValue>(TValue? @default = default, [CallerMemberName] string name = "")
        {
            return _watcher.GetValue(@default, name);
        }

        protected TValue? GetComputedValue<TValue>([CallerMemberName] string name = "")
        {
            return _watcher.GetComputedValue<TValue>(name);
        }

        protected TValue? GetComputedValue<TValue>(Expression<Func<TValue>> valueExpression, [CallerMemberName] string name = "")
        {
            return _watcher.GetComputedValue(valueExpression, name);
        }

        protected TValue? GetComputedValue<TValue>(Func<TValue> valueFactory, string[] dependencyProperties, [CallerMemberName] string name = "")
        {
            return _watcher.GetComputedValue(valueFactory, dependencyProperties, name);
        }

        protected void SetValue<TValue>(TValue value, [CallerMemberName] string name = "", bool disableIListAlwaysNotifying = false)
        {
            _watcher.SetValue(value, name, disableIListAlwaysNotifying);
        }

        protected RenderFragment? RenderPart(Type keyType)
        {
            return AbstractProvider.GetPartContent(keyType, this);
        }

        protected RenderFragment? RenderPart(Type keyType, Action<AttributesBuilder> builderAction)
        {
            return AbstractProvider.GetPartContent(keyType, this, builderAction);
        }

        protected RenderFragment? RenderPart(Type keyType, object? arg0, [CallerArgumentExpression("arg0")] string arg0Name = "")
        {
            return AbstractProvider.GetPartContent(keyType, this, builder =>
            {
                builder
                    .Add(arg0Name, arg0);
            });
        }

        protected RenderFragment? RenderPart(Type keyType, object? arg0, object? arg1, [CallerArgumentExpression("arg0")] string arg0Name = "",
            [CallerArgumentExpression("arg1")] string arg1Name = "")
        {
            return AbstractProvider.GetPartContent(keyType, this, builder =>
            {
                builder
                    .Add(arg0Name, arg0)
                    .Add(arg1Name, arg1);
            });
        }

        protected Dictionary<string, object?> GetAttributes(Type type, object? data = null)
        {
            return AbstractProvider.GetMetadata(type, data).Attributes;
        }

        protected Dictionary<string, object?> GetAttributes(Type type, string name, object? data = null)
        {
            return AbstractProvider.GetMetadata(type, name, data).Attributes;
        }

        public EventCallback<TValue> CreateEventCallback<TValue>(Func<TValue, Task> callback)
        {
            return EventCallback.Factory.Create(this, callback);
        }

        public EventCallback CreateEventCallback(Func<Task> callback)
        {
            return EventCallback.Factory.Create(this, callback);
        }

        public EventCallback<TValue> CreateEventCallback<TValue>(Action<TValue> callback)
        {
            return EventCallback.Factory.Create(this, callback);
        }

        public EventCallback CreateEventCallback(Action callback)
        {
            return EventCallback.Factory.Create(this, callback);
        }
    }
}
