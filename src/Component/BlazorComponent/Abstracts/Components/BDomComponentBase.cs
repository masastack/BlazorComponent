using BlazorComponent.Abstracts;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace BlazorComponent
{
    public abstract class BDomComponentBase : BComponentBase, IHasProviderComponent
    {
        protected BDomComponentBase()
        {
            _watcher = new PropertyWatcher(GetType());

            CssProvider = new(() => Class, () => Style);
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
        /// Specifies one or more class names for an DOM element.
        /// </summary>
        [Parameter]
        public string? Class { get; set; }

        /// <summary>
        /// Specifies an inline style for an DOM element.
        /// </summary>
        [Parameter]
        public string? Style { get; set; }

        /// <summary>
        /// Custom attributes
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public virtual IDictionary<string, object?> Attributes { get; set; } = new Dictionary<string, object?>();

        protected const int BROWSER_RENDER_INTERVAL = 16;

        private readonly PropertyWatcher _watcher;

        private ElementReference _ref;
        private ElementReference? _prevRef;
        private bool _elementReferenceChanged;

        protected bool HostedInWebAssembly => Js is IJSInProcessRuntime;

        protected ILogger Logger => LoggerFactory.CreateLogger(GetType());

        public ComponentCssProvider CssProvider { get; }

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
            Id ??= ComponentIdGenerator.Generate(this);
            base.OnInitialized();
            SetComponentClass();
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

        protected virtual void SetComponentClass()
        {
        }

        /// <summary>
        /// Register watchers at the first render.
        /// </summary>
        /// <param name="watcher"></param>
        protected virtual void RegisterWatchers(PropertyWatcher watcher)
        {
        }

        protected TValue? GetValue<TValue>(TValue? @default = default, [CallerMemberName] string name = "", bool disableIListAlwaysNotifying = false)
        {
            return _watcher.GetValue(@default, name, disableIListAlwaysNotifying);
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

        protected void SetValue<TValue, TFirstValue>(TValue value, string propertySetFirst, [CallerMemberName] string name = "")
        {
            _watcher.SetValue<TValue, TFirstValue>(value, name, propertySetFirst);
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

        protected RenderFragment? RenderPart(Type keyType, object? arg0, object? arg1, object? arg2,
            [CallerArgumentExpression("arg0")] string arg0Name = "", [CallerArgumentExpression("arg1")] string arg1Name = "",
            [CallerArgumentExpression("arg2")] string arg2Name = "")
        {
            return AbstractProvider.GetPartContent(keyType, this, builder =>
            {
                builder
                    .Add(arg0Name, arg0)
                    .Add(arg1Name, arg1)
                    .Add(arg2Name, arg2);
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
